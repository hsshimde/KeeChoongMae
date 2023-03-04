using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.Model;
using upbit.UpbitAPI.Model;
using upbit.Enum;
using static upbit.Controller.Running;

namespace upbit.View
{
    internal class SettingTransaction
    {
    }

    public partial class MainForm
    {
        public enum ETransactionSetting
        {
            Sell,
            Buy
        }

        public static ETransactionSetting eTransactionSetting = ETransactionSetting.Buy;

        private void OnBnClickedSettingTransactionSellOrBuy(object sender, EventArgs e)
        {
            if (eTransactionSetting == ETransactionSetting.Buy)
            {
                eTransactionSetting = ETransactionSetting.Sell;
                this.button_curTransChange.Text = "매도";
                
            }
            else
            {
                eTransactionSetting = ETransactionSetting.Buy;
                this.button_curTransChange.Text = "매수";
            }
            ResetComboBox();
        }

        private void ResetComboBox()
        {
            comboBox_selectMarket.Items.Clear();
            if(eTransactionSetting == ETransactionSetting.Buy)
            {
                foreach (KeyValuePair<string, Coin> kvp in DictCoinInfo)
                {
                    Coin coin = kvp.Value;
                    if(coin.MarketGridTabIdx == EMarketGridTabIdx.KRW)
                    {
                        StringBuilder sbCoinDesc = new StringBuilder();
                        sbCoinDesc.Append(kvp.Value.CoinNameKor);
                        sbCoinDesc.Append("(");
                        sbCoinDesc.Append(kvp.Key);
                        sbCoinDesc.Append(")");
                        comboBox_selectMarket.Items.Add(sbCoinDesc.ToString());
                    }
                }
            }
            else
            {
                foreach (KeyValuePair<string, CoinAccount> kvp in DictCoinAccount)
                {
                    StringBuilder sbCoinDesc = new StringBuilder();
                    sbCoinDesc.Append(kvp.Value.CoinNameKor);
                    sbCoinDesc.Append("(");
                    sbCoinDesc.Append(kvp.Key);
                    sbCoinDesc.Append(")");
                    comboBox_selectMarket.Items.Add(sbCoinDesc.ToString());
                }
            }
        }

        private async void OnBnClickedMakeOrder(object sender, EventArgs e)
        {
            //OrderEnqForBuy(string market, double tradeValue, double waitOrderSecond)
            StringBuilder sbCoinMarketInfo = new StringBuilder();
            string selectCoin = comboBox_selectMarket.Text;
            if(selectCoin == "선택")
            {
                return;
            }
            int nMarketInfoStartIdx = selectCoin.IndexOf('(');
            int nMarketInfoEndIdx = selectCoin.IndexOf(')');
            int nMarketInfoLength = nMarketInfoEndIdx - nMarketInfoStartIdx - 1;
            string coinMarket = selectCoin.Substring(nMarketInfoStartIdx + 1, nMarketInfoLength);
            string transactionVolume = textBox_TransactionAmount.Text;
            if(transactionVolume.Length < 1)
            {
                return;
            }
            double dblTransactionVolume = Convert.ToDouble(transactionVolume);
            if (eTransactionSetting == ETransactionSetting.Buy)
            {
                Task<OrderChance> taskOrderChance = mAPI.GetOrderChance(coinMarket);
                OrderChance thisMarketOrderChance = await taskOrderChance;

                MyOrder newBuyOrder = new MyOrder(coinMarket, EnumClass.EOrderState.WaitBuy, dblTransactionVolume, 5);
                await running.Buy(newBuyOrder, DateTime.Now, BuyOrSellStatus.First);
            }
            else
            {
                Task<OrderChance> taskOrderChance = mAPI.GetOrderChance(coinMarket);
                OrderChance thisMarketOrderChance = await taskOrderChance;
                //Task<List<OrderBook>> taskOrderBook = mAPI.GetOrderBook(coinMarket);
                //List<OrderBook> listOrderBook = await taskOrderBook;

                MyOrder newBuyOrder = new MyOrder(coinMarket, EnumClass.EOrderState.WaitSell, dblTransactionVolume, 5);
                await running.Sell(newBuyOrder, DateTime.Now, BuyOrSellStatus.First);


            }
        }

       

        
    }


}
