using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using RestSharp;
using Newtonsoft.Json;

namespace upbit.UpbitAPI
{
    using upbit.UpbitAPI.Model;
    public class APIClass
    {
        private Param mWithParam;
        private NoParam mNoParam;

        public APIClass(string upbitAccesKey, string upbitSecretKey)
        {
            mWithParam= new Param(upbitAccesKey, upbitSecretKey);
            mNoParam = new NoParam(upbitAccesKey, upbitSecretKey);
        }
       
        public async Task<List<Account>> GetAccount()
        {
            // 자산 - 전체 계좌 조회
            //var data = mNoParam.Get("/v1/accounts", RestSharp.Method.GET);
            Task<string> dataTask = mNoParam.GetTask("/v1/accounts", RestSharp.Method.GET);
            string data = await dataTask;
            if (data != null)
            {
                return JsonConvert.DeserializeObject<List<Account>>(data);
            }
            else
            {
                return null;
            }
        }

        public async Task<OrderChance> GetOrderChance(string market)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            StringBuilder sbReturn = new StringBuilder();
            Task<string> dataTask =  mWithParam.Get("/v1/orders/chance", parameters, RestSharp.Method.GET);
            string data = await dataTask;

            return JsonConvert.DeserializeObject<OrderChance>(data);
        }

        public async Task<Order> GetOrder(string uuid)
        {
            // 주문 - 개별 주문 조회
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("uuid", uuid);
            Task<string> dataTask =  mWithParam.Get("/v1/order", parameters, RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<Order>(data);
        }

        public async Task<CancelOrder> CancelOrder(string uuid)
        {
            // 주문 - 주문 취소 접수
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("uuid", uuid);
            Task<string> dataTask = mWithParam.Get("/v1/order", parameters, RestSharp.Method.DELETE);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<CancelOrder>(data);
        }
        public async Task<MakeOrder> MakeOrderLimit(string market, OrderSide orderSide, double volume, double price)
        {
            // 주문 - 주문하기 - 지정가 매수&매도
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("side", orderSide.ToString());
            parameters.Add("volume", volume.ToString());
            parameters.Add("price", price.ToString());
            parameters.Add("ord_type", "limit");

            Task<string> dataTask = mWithParam.Get("/v1/orders", parameters, RestSharp.Method.POST);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<MakeOrder>(data);
        }
        public async Task<MakeOrderMarketBuy> MakeOrderMarketBuy(string market, double price)
        {
            // 주문 - 주문하기 - 시장가매수

            /* 주문 가격. (지정가, 시장가 매수 시 필수)
            ex) KRW-BTC 마켓에서 1BTC당 1,000 KRW로 거래할 경우, 값은 1000 이 된다.
            ex) KRW-BTC 마켓에서 1BTC당 매도 1호가가 500 KRW 인 경우,
            시장가 매수 시 값을 1000으로 세팅하면 2BTC가 매수된다.
            (수수료가 존재하거나 매도 1호가의 수량에 따라 상이할 수 있음)  
            --> 결론 : price는 원화가치인듯 */

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("side", OrderSide.bid.ToString());
            parameters.Add("price", price.ToString());
            parameters.Add("ord_type", "price");
            Task<string> dataTask = mWithParam.Get("/v1/orders", parameters, RestSharp.Method.POST);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<MakeOrderMarketBuy>(data);
        }
        public async Task<MakeOrderMarketSell> MakeOrderMarketSell(string market, double volume)
        {
            // 주문 - 주문하기 - 시장가매도
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("side", OrderSide.ask.ToString());
            parameters.Add("volume", volume.ToString());
            parameters.Add("ord_type", "market");
            Task<string> dataTask = mWithParam.Get("/v1/orders", parameters, RestSharp.Method.POST);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<MakeOrderMarketSell>(data);
        }


        /*--------------------- QUOTATION API ---------------------*/

        public async Task<List<MarketAll>> GetMarketAll()
        {
            // 시세 종목 조회 - 마켓 코드 조회
            Task<string> dataTask = mNoParam.Get("/v1/market/all", RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<List<MarketAll>>(data);

        }

        public async Task<List<CandleMinute>> GetCandleMinutes(string market, MinuteUnit unit, DateTime to = default(DateTime), int count = 1)
        {
            // 시세 캔들 조회 - 분(Minute) 캔들
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("to", to.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("count", count.ToString());
            Task<string> dataTask = mWithParam.Get(String.Join("", "/v1/candles/minutes/", (int)unit), parameters, RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<List<CandleMinute>>(data);

        }

        public async Task<List<CandleDay>> GetCandleDays(string market, DateTime to = default(DateTime), int count = 1)
        {
            // 시세 캔들 조회 - 일(Day) 캔들
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("to", to.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("count", count.ToString());
            Task<string> dataTask = mWithParam.Get("/v1/candles/days", parameters, RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<List<CandleDay>>(data);
        }
        public async Task<List<CandleWeek>> GetCandleWeeks(string market, DateTime to = default(DateTime), int count = 1)
        {
            // 시세 캔들 조회 - 주(Week) 캔들
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("to", to.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("count", count.ToString());
            Task<string> dataTask = mWithParam.Get("/v1/candles/weeks", parameters, RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<List<CandleWeek>>(data);
        }
        public async Task<List<CandleMonth>> GetCandleMonths(string market, DateTime to = default(DateTime), int count = 1)
        {
            // 시세 캔들 조회 - 월(Month) 캔들
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("market", market);
            parameters.Add("to", to.ToString("yyyy-MM-dd HH:mm:ss"));
            parameters.Add("count", count.ToString());
            Task<string> dataTask = mWithParam.Get("/v1/candles/months", parameters, RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<List<CandleMonth>>(data);
        }

        public async Task<List<Ticker>> GetTicker(string markets)
        {
            // 시세 Ticker조회 - 현재가정보
            // market을 콤마로 구분하여 입력한다. 
            // ex) "KRW-BTC, KRW-ETH, ....."
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("markets", markets);
            Task<string> dataTask = mWithParam.Get("/v1/ticker", parameters, RestSharp.Method.GET);
            string data = await dataTask;
            if (data != null)
            {
                return JsonConvert.DeserializeObject<List<Ticker>>(data);
            }
            else
            {
                return null;
            }
        }

        public async Task<List<OrderBook>> GetOrderBook(string markets)
        {
            // 시세 호가 정보 조회 - 호가 정보 조회
            // market을 콤마로 구분하여 입력한다. 
            // ex) "KRW-BTC, KRW-ETH, ....."
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("markets", markets);
            Task<string> dataTask = mWithParam.Get("/v1/orderbook", parameters, RestSharp.Method.GET);
            string data = await dataTask;
            return JsonConvert.DeserializeObject<List<OrderBook>>(data);
        }

        public enum OrderSide
        {
            bid,    // 매수
            ask     // 매도
        }
        public enum MinuteUnit
        {
            _1 = 1,
            _3 = 3,
            _5 = 5,
            _10 = 10,
            _15 = 15,
            _30 = 30,
            _60 = 60,
            _240 = 240

        }
    }
}
