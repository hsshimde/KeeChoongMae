using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;
using upbit.View;
using upbit.Model;
using upbit.Enum;
using Newtonsoft.Json;

namespace upbit.Controller
{
    public partial class Running
    {
        private System.Timers.Timer mMainTimer;
        private System.Timers.Timer mOrderTimer;
        private APIClass mAPI;
        public string m_strMyAssetCode { get; set; }
        public string m_SelectMarketInfo { get; set; }
        public string allMarketCode { get; set; }
        public string SearchBoxMatchMarketCode { get; private set; }
        private MainForm mMainForm;

        private ConcurrentQueue<Model.MyOrder> mInsertOrderQueue;
        private TimeSpanCheck mTmSpanCheck;

        public double RunningValue { get; set; }



        private const double BUY_FEE = 0.05 * 0.01;
        private const double SELL_FEE = 0.05 * 0.01;

        public enum BuyOrSellStatus
        {
            First,
            IncompleteAndRetry
        }


        public Running(APIClass api, MainForm mainForm)
        {
            Debug.Assert(api != null);
            Debug.Assert(mainForm != null);
            InitializeRunning(api);
            mMainForm = mainForm;
            RunningMarketList = null;
            RunningStatus = EnumClass.EIsRunning.Paused;
            mTmSpanCheck = new TimeSpanCheck(DateTime.Now);
        }

        public void OrderEnqForBuy(string market, double tradeValue, double waitOrderSecond)
        {
            //마켓, 매수 가치, 매수 주문대기 시간
            //Coin marketCoin = DictAllMarketInfo[market];
            if (DictAllMarketInfo.ContainsKey(market))
            {
                Coin marketCoin = DictAllMarketInfo[market];
                if(marketCoin.IsBuyingRequested == false)
                {
                    marketCoin.IsBuyingRequested = true;
                    using
                        (MyOrder insertOrder = new MyOrder(market, EnumClass.EOrderState.WaitBuy, tradeValue, waitOrderSecond))
                    {
                        mInsertOrderQueue.Enqueue(insertOrder);
                    }

                }
            }
        }

        public void OrderEnqForSell(string market, double tradeValue, double avgPrice, double ownQuantity, double waitOrderSecond)
        {
            //마켓, 매도가치, 평단가, 보유수량, 매도주문대기시간
            using (MyOrder insertOrder = new MyOrder(market, EnumClass.EOrderState.WaitSell, tradeValue, waitOrderSecond))
            {
                insertOrder.OwnQuantity = ownQuantity;
                insertOrder.AvgPrice = avgPrice;
                mInsertOrderQueue.Enqueue(insertOrder);
            }
        }


        //private async Task OrderDeque(DateTime dateTimeNow)
        //{
        //    if (mInsertOrderQueue.Count > 0)
        //    {
        //        MyOrder orderData = mInsertOrderQueue.Dequeue();
        //        switch (orderData.OrderState)
        //        {
        //            case EnumClass.EOrderState.WaitBuy:
        //                {
        //                    await Buy(orderData, dateTimeNow, BuyOrSellStatus.First); //매수 주문
        //                }
        //                break;

        //            case EnumClass.EOrderState.Buying:
        //                {
        //                    await BeBuying(orderData, dateTimeNow); //매수 주문 중
        //                }
        //                break;

        //            case EnumClass.EOrderState.IncompleteBuyOrder:
        //                {
        //                    await IncompleteBuy(orderData, dateTimeNow); //미체결 매수 주문 처리
        //                }
        //                break;

        //            case EnumClass.EOrderState.WaitSell:
        //                {
        //                    await Sell(orderData, dateTimeNow, BuyOrSellStatus.First); //매도 주문
        //                }
        //                break;

        //            case EnumClass.EOrderState.Selling:
        //                {
        //                    await BeSelling(orderData, dateTimeNow);  //매도 주문 중 
        //                }
        //                break;

        //            case EnumClass.EOrderState.IncompleteSellOrder:
        //                {
        //                    await IncompleteSell(orderData, dateTimeNow);
        //                }
        //                break;
        //        }
        //    }
        //}

        private async Task DoOrderByStatus(MyOrder orderData, DateTime dateTimeNow)
        {
            if (mInsertOrderQueue.Count > 0)
            {
                //MyOrder orderData = mInsertOrderQueue.Dequeue();
                switch (orderData.OrderState)
                {
                    case EnumClass.EOrderState.WaitBuy:
                        {
                            await Buy(orderData, dateTimeNow, BuyOrSellStatus.First); //매수 주문
                        }
                        break;

                    case EnumClass.EOrderState.Buying:
                        {
                            await BeBuying(orderData, dateTimeNow); //매수 주문 중
                        }
                        break;

                    case EnumClass.EOrderState.IncompleteBuyOrder:
                        {
                            await IncompleteBuy(orderData, dateTimeNow); //미체결 매수 주문 처리
                        }
                        break;

                    case EnumClass.EOrderState.WaitSell:
                        {
                            await Sell(orderData, dateTimeNow, BuyOrSellStatus.First); //매도 주문
                        }
                        break;

                    case EnumClass.EOrderState.Selling:
                        {
                            await BeSelling(orderData, dateTimeNow);  //매도 주문 중 
                        }
                        break;

                    case EnumClass.EOrderState.IncompleteSellOrder:
                        {
                            await IncompleteSell(orderData, dateTimeNow);
                        }
                        break;
                }
            }
        }

        public async Task Go()
        {
            await BeforeGoRunning();
            if (false == this.mMainTimer.Enabled)
            {
                this.mMainTimer.Start();
            }
        }

        public void SetSearchBoxMatchMarketCode(string matchMarketCode)
        {
            SearchBoxMatchMarketCode = matchMarketCode;
        }
        public void SetAllMarketInfo(Dictionary<string, Coin> dictMarketInfo)
        {
            Debug.Assert(dictMarketInfo != null);
            Debug.Assert(DictAllMarketInfo == null);
            DictAllMarketInfo = dictMarketInfo;
        }

        public void SetMyAssetInfo(Dictionary<string, CoinAccount> dictMyAssetInfo)
        {
            Debug.Assert(dictMyAssetInfo != null);
            Debug.Assert(DictMyAssetInfo == null);
            DictMyAssetInfo = dictMyAssetInfo;
        }

        //private void main()
        //{
        //    List<Ticker> tickerList = API.GetTicker(strSelectCoin);
        //    if(tickerList!=null)
        //    {
        //        foreach(Ticker tickerItem in tickerList)
        //        {
        //            Console.WriteLine($"{tickerItem.market} {tickerItem.trade_date_kst} {tickerItem.trade_price}");
        //        }
        //    }
        //}

        public void Stop()
        {
            this.mMainTimer.Stop();
            if (mOrderTimer.Enabled)
            {
                mOrderTimer.Stop();
            }
        }


        private async void ElapsedEventReceiver(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (sender.Equals(this.mMainTimer))
            {
                if(mMainForm.IsDisposed)
                {
                    return;
                }
                await DoUpdate();

                CheckAndStartOrderTimer();
                CheckAndStopOrderTimer();
            } 
            else if (sender.Equals(this.mOrderTimer))
            {
                //await DoOrderByStatus(e.SignalTime);
                MyOrder orderData = null;
                bool bPopResult = mInsertOrderQueue.TryDequeue(out orderData);
                if (orderData != null)
                {
                    if (bPopResult)
                    {
                        await DoOrderByStatus(orderData, e.SignalTime);

                    }
                }
                else
                {
                    CheckAndStopOrderTimer();
                }
            }
        }

        #region BUY

        public async Task Buy(MyOrder orderData, DateTime dateTimeNow, BuyOrSellStatus buyStatus)
        {
            double curPrice = GetCurPrice(orderData.market);
            if (curPrice <= 0)
            {
                return;
            }

            double buyQuantity = 0;
            if (buyStatus == BuyOrSellStatus.IncompleteAndRetry)
            {
                buyQuantity = orderData.OrderQuantity;
            }
            else
            {
                buyQuantity = orderData.GetOrderQuantity(curPrice);
            }
            Task<MakeOrder> taskBuyOrder = mAPI.MakeOrderLimit(orderData.market, UpbitAPI.APIClass.OrderSide.bid, buyQuantity, curPrice);
            //Task<List<Account>> taskMyAssetAccount = mAPI.GetAccount();
            //List<Account> myAssetAccount = await taskMyAssetAccount;
            MakeOrder buyOrder = await taskBuyOrder;
            if (buyOrder != null) // Null이 아니라면 
            {
                Error thisError = buyOrder.error;
                if (thisError == null) //에러가 없다면
                {
                    orderData.OrderDateTime = dateTimeNow;
                    orderData.uuid = buyOrder.uuid;
                    orderData.OrderQuantity = buyOrder.volume;
                    orderData.OrderState = EnumClass.EOrderState.Buying;
                    mInsertOrderQueue.Enqueue(orderData);
                }
                else
                {
                    Console.WriteLine(thisError.message);
                    //if(thisError.name == "Invalid_")
                    //if (thisError.name.Equals("under_min_total_bid"))
                    //{
                    //    Console.WriteLine(thisError.message);
                    //    //Console.WriteLine("주문 요청 금액이 최소주문금액 미만입니다");
                    //}
                    //else if (thisError.name.Equals("insufficient_funds_bid"))
                    //{
                    //    Console.WriteLine(thisError.message);

                    //    //Console.WriteLine("매수 가능 잔고가 부족합니다.");
                    //}
                    //else if (thisError.name.Equals("create_bid_error"))
                    //{
                    //    Console.WriteLine(thisError.message);

                    //    //mInsertOrderQueue.Enqueue(orderData);
                    //    //Console.WriteLine("주문 요청 정보가 올바르지 않습니다.");
                    //}
                    //else if (thisError.name.Equals("validation_error"))
                    //{
                    //    Console.WriteLine(thisError.message);

                    //    //Console.WriteLine("잘못된 요청입니다.");
                    //}

                }
            }
        }

        public async Task BeBuying(MyOrder orderData, DateTime dateTimeNow)
        {
            //var orderInfo = mAPI.GetOrder(orderData.uuid, )
            Console.WriteLine("Be Buying Function Call");
            Task<Order> taskOrder = mAPI.GetOrder(orderData.uuid);
            //using (var orderInfo = mAPI.GetOrder(orderData.uuid))
            Order orderInfo = await taskOrder;
            if (orderInfo != null)
            {
                if (orderInfo.Error == null)
                {
                    if (orderInfo.remaining_volume <= 0)
                    {
                        //체결 완료됨
                        Console.WriteLine("Bidding Complete~~~");
                        if(DictAllMarketInfo.ContainsKey(orderData.market))
                        {
                            Coin buyingCoin = DictAllMarketInfo[orderData.market];
                            buyingCoin.IsBuyingRequested = false;
                            buyingCoin.UpdateBuyPricePos();
                        }
                        else
                        {
                            Debug.Assert(false, "NO Market COinData");
                        }
                    }
                    else
                    {
                        Console.WriteLine("--------Bidding(Buying) InComplete--------");
                        Console.WriteLine($"-----Current State :  {orderInfo.state}----");
                        Console.WriteLine($"-----Trades Price :  {orderInfo.price}-----");
                        Console.WriteLine($"------Remaining Volune :  {orderInfo.remaining_volume}------");
                        //Console.WriteLine("${}");

                        //미체결 
                        // 사용자가 설정한 시간이 지나도 매수 완료가 되지 않는다면
                        //주문 취소 후에 주문 큐에 다시 매수 주문을 넣음
                        TimeSpan diffTime = dateTimeNow - orderData.OrderDateTime;
                        if (diffTime.TotalSeconds >= orderData.WaitOrderSeconds)
                        {
                            Console.WriteLine("-----Time Over--------");
                            Console.WriteLine("-----Making Cancel Order--------");
                            Task<CancelOrder> taskCancelOrder = mAPI.CancelOrder(orderData.uuid);
                            CancelOrder cancelOrder = await taskCancelOrder;
                            Console.WriteLine("-------Cancelling Orders Start ----------");
                            if (cancelOrder != null)
                            {
                                if (cancelOrder.error == null)
                                {
                                    Console.WriteLine("Incomplete Buy -- Inserting ordering queue again");

                                    orderData.OrderQuantity = cancelOrder.remaining_volume;
                                    orderData.OrderDateTime = dateTimeNow;
                                    orderData.OrderState = EnumClass.EOrderState.IncompleteBuyOrder;
                                    mInsertOrderQueue.Enqueue(orderData);

                                    //Need to Put Cancelling Ordering here
                                    //
                                }
                                else
                                {
                                    string errorMsg = cancelOrder.error.name + " - " + cancelOrder.error.message;
                                    Console.WriteLine($"[BUY]Cancelling Order Error Occured ! [{errorMsg}] Inserting Queue Again");
                                    mInsertOrderQueue.Enqueue(orderData); //에러로 인한 주문 실패는 다시 실행 +9
                                }
                            }
                            else
                            {
                                //에러로 인한 주문실패 
                                string errorMsg = cancelOrder.error.name + " - " + cancelOrder.error.message;
                                Console.WriteLine($"[BUY]Cancelling Order Error Occured ! [{errorMsg}] Inserting Queue Again");
                                mInsertOrderQueue.Enqueue(orderData); //에러로 인한 주문 실패는 다시 실행 +9
                            }
                        }
                        else
                        {
                            Console.WriteLine("[BUY]Still Have Time Left to Wait - Inserting Queue Again");
                            mInsertOrderQueue.Enqueue(orderData);//Incomplete Buy Because of Error
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("--------Get Buying Order Info NULL Inserting Queue Again---------");
                mInsertOrderQueue.Enqueue(orderData);
            }

        }

        private async Task IncompleteBuy(MyOrder orderData, DateTime dateTimeNow)
        {
            await Buy(orderData, dateTimeNow, BuyOrSellStatus.IncompleteAndRetry);
        }
        #endregion

        #region SELL
        public async Task Sell(MyOrder orderData, DateTime dateTimeNow, BuyOrSellStatus eStatus)
        {
            double curPrice = GetCurPrice(orderData.market);
            if (curPrice <= 0)
            {
                return;
            }

            else
            {
                double sellQuantity = 0;
                if (eStatus == BuyOrSellStatus.IncompleteAndRetry)
                {
                    sellQuantity = orderData.OrderQuantity;
                    if (sellQuantity > orderData.OwnQuantity)
                    {
                        sellQuantity = orderData.OwnQuantity;
                    }
                }
                else
                {
                    sellQuantity = orderData.GetOrderQuantity(curPrice);
                    //if (sellQuantity > orderData.OwnQuantity)
                    //{
                    //    sellQuantity = orderData.OwnQuantity;
                    //}
                }

                Task<MakeOrder> taskMakeOrder = mAPI.MakeOrderLimit(orderData.market, APIClass.OrderSide.ask, sellQuantity, curPrice);
                MakeOrder makeOrder = await taskMakeOrder;
                if (makeOrder != null)
                {
                    Error thisError = makeOrder.error;
                    if (thisError == null)
                    {
                        orderData.OrderDateTime = dateTimeNow;
                        orderData.uuid = makeOrder.uuid;
                        orderData.OrderQuantity = makeOrder.volume;
                        orderData.OrderState = EnumClass.EOrderState.Selling;
                        mInsertOrderQueue.Enqueue(orderData);
                    }
                    else
                    {
                        Console.WriteLine(thisError.name + thisError.message);

                        //if (makeOrder.Error.Name.Equals("under_min_total_bid"))
                        //{
                        //    //requested amount is smaller than minimun
                        //}
                        //else if (makeOrder.Error.Name.Equals("insufficient_funds_bid"))
                        {
                            //There isnt enough money for buying

                        }
                        //else
                        {
                            //mInsertOrderQueue.Enqueue(orderData);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("SELL Order NULL -- Inserting ordering queue again");
                    mInsertOrderQueue.Enqueue(orderData);
                }
            }
        }
        private async Task BeSelling(MyOrder orderData, DateTime dateTimeNow)
        {
            Console.WriteLine("Be Selling Function Call");
            Task<Order> taskSellInfo = mAPI.GetOrder(orderData.uuid);
            Order orderInfo = await taskSellInfo;
            if (orderInfo != null)
            {
                if (orderInfo.Error == null)
                {
                    if (orderInfo.remaining_volume <= 0)
                    {
                        //매도 주문이 체결됨 로그 기록
                        Console.WriteLine("Asking Complete~~~");
                    }
                    else
                    {
                        //미체결
                        //사용자가 설정한 시간이 지나도 체결이 되지 않는다면 
                        //주문 취소 후 다음 큐에 매도 주문을 넣음

                        Console.WriteLine("--------Asking(Selling) InComplete--------");
                        Console.WriteLine($"-----Current State :  {orderInfo.state}----");
                        Console.WriteLine($"-----Trades Price :  {orderInfo.price}-----");
                        Console.WriteLine($"------Remaining Volune :  {orderInfo.remaining_volume}------");
                        TimeSpan diffTime = dateTimeNow - orderData.OrderDateTime;
                        if (diffTime.TotalSeconds >= orderData.WaitOrderSeconds)
                        {
                            Console.WriteLine("-----Time Over--------");
                            Console.WriteLine("-----Making Cancel Order--------");
                            Task<CancelOrder> taskCancelOrder = mAPI.CancelOrder(orderData.uuid);
                            CancelOrder cancelOrder = await taskCancelOrder;
                            Console.WriteLine("-------Cancelling Orders Start ----------");
                            if (cancelOrder != null)
                            {
                                if (cancelOrder.error == null)
                                {
                                    Console.WriteLine("Incomplete Selling -- Inserting ordering queue again");
                                    orderData.OrderDateTime = dateTimeNow;
                                    orderData.OrderQuantity = cancelOrder.remaining_volume;
                                    orderData.OrderState = EnumClass.EOrderState.IncompleteSellOrder;
                                    mInsertOrderQueue.Enqueue(orderData);
                                    //취소 주문 투입
                                }
                                else
                                {
                                    string errorMsg = cancelOrder.error.name + " - " + cancelOrder.error.message;
                                    Console.WriteLine($"[SELL]Error Occured ! [{errorMsg}] Inserting Queue Again");
                                    mInsertOrderQueue.Enqueue(orderData);
                                }
                            }
                            else
                            {
                                // 에러로 인한 주문실패 
                                string errorMsg = cancelOrder.error.name + " - " + cancelOrder.error.message;
                                Console.WriteLine($"[SELL]Error Occured ! [{errorMsg}] Inserting Queue Again");
                                mInsertOrderQueue.Enqueue(orderData);
                            }
                        }
                        else
                        {
                            Console.WriteLine("[SELL]Still Have Time Left to Wait - Inserting Queue Again");
                            mInsertOrderQueue.Enqueue(orderData);//Incomplete Buy Because of Error
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("--------Get Selling Order Info NULL Inserting Queue Again---------");
                mInsertOrderQueue.Enqueue(orderData);
            }
        }

        private async Task IncompleteSell(MyOrder orderData, DateTime dateTimeNow)
        {
            await Sell(orderData, dateTimeNow, BuyOrSellStatus.IncompleteAndRetry);
        }
        #endregion


        private double GetCurPrice(string marketInfo)
        {

            if (DictAllMarketInfo.ContainsKey(marketInfo))
            {
                Coin lookedUpCoin = DictAllMarketInfo[marketInfo];
                return lookedUpCoin.MarketTicker.trade_price;
            }
            else
            {
                return -1;
            }
        }

    }


}
