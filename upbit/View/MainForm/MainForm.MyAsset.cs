using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using upbit.UpbitAPI;
using upbit.Controller;
using upbit.UpbitAPI.Model;
using upbit.ColumnNameBuilder;
using System.Diagnostics;

namespace upbit.View
{
    internal class MyAsset
    {

    }
    public partial class MainForm
    {

        public Dictionary<string, CoinAccount> DictCoinAccount { get; private set; }
        async Task DivideMyAssetGridByUnitCurrency()
        {
            bool bKoreanWonChekced = false;
            Task<List<Account>> taskMyAccountList = mAPI.GetAccount();
            List<Account> allAssetInfo = await taskMyAccountList;
            StringBuilder sbMarketCodeBuilder = new StringBuilder();
            EMarketGridTabIdx eGridKind = new EMarketGridTabIdx();

            foreach (Account acc in allAssetInfo)
            {
                sbMarketCodeBuilder.Clear();
                string curreny = acc.currency;
                if(!bKoreanWonChekced && acc.currency == "KRW")
                {
                    bKoreanWonChekced = true;
                    continue;
                }

                sbMarketCodeBuilder.AppendFormat(acc.unit_currency);
                sbMarketCodeBuilder.AppendFormat("-");
                sbMarketCodeBuilder.AppendFormat(acc.currency);
                if("KRW" == acc.unit_currency)
                {
                    eGridKind = EMarketGridTabIdx.KRW;
                }
                else if("BTC" == acc.unit_currency)
                {
                    eGridKind = EMarketGridTabIdx.BTC;
                }
                else if("USDT" == acc.unit_currency)
                {
                    eGridKind = EMarketGridTabIdx.USDT;
                }
                else
                {
                    Debug.Assert(false);
                }
                string coinMarketCode = sbMarketCodeBuilder.ToString();
                bool bFindFromMarket = DictCoinInfo.ContainsKey(coinMarketCode);
                if(!bFindFromMarket)
                {
                    Debug.Assert(false, "Market Code is Wrong!!");
                    continue;
                }
                Coin coinInfo = DictCoinInfo[coinMarketCode];
                CoinAccount myAssetCoinAccount = new CoinAccount(coinMarketCode, 0, acc.balance, coinInfo.CurPrice, acc.avg_buy_price);
                myAssetCoinAccount.CoinNameEng = coinInfo.CoinNameEng;
                myAssetCoinAccount.CoinNameKor = coinInfo.CoinNameKor;
                bool bCoinAccountAlreadyExist = DictCoinAccount.ContainsKey(coinMarketCode);
                if(!bCoinAccountAlreadyExist)
                {
                    DictCoinAccount.Add(coinMarketCode, myAssetCoinAccount);
                    AddMyAssetInfo(eGridKind, myAssetCoinAccount);
                }
                else
                {
                    Debug.Assert(bCoinAccountAlreadyExist, "Coin Account Already Exist");
                }
                //sbMarketCodeBuilder.AppendFormat(acc.unit_currency);
                //sbMarketCodeBuilder.AppendFormat("-");
                //sbMarketCodeBuilder.AppendFormat(acc.currency);
                //string marketCode = sbMarketCodeBuilder.ToString();
                //bool bCoinAccountAdded = .ContainsKey(marketCode);
                //if(bCoinAccountAdded)
                //{
                //    AddMyAssetInfo(eGridKind, dictCoinAccount[marketCode]);
                //}
                //else
                //{
                //    Debug.Assert(false);
                //}



            }
            //foreach (KeyValuePair<string, CoinAccount> kvp in DictCoinAccount)
            //{
            //    StringBuilder sbCoinDesc = new StringBuilder();
            //    sbCoinDesc.Append(kvp.Value.CoinNameKor);
            //    sbCoinDesc.Append("(");
            //    sbCoinDesc.Append(kvp.Key);
            //    sbCoinDesc.Append(")");
            //    comboBox_selectMarket.Items.Add(sbCoinDesc.ToString());
            //}
        }

        private void AddMyAssetInfo(EMarketGridTabIdx gridType, CoinAccount coinAccount)
        {
            StringBuilder coinMarketNameBuilder = new StringBuilder();
            coinMarketNameBuilder.AppendFormat(coinAccount.CoinNameKor);
            coinMarketNameBuilder.AppendFormat("(");
            coinMarketNameBuilder.AppendFormat(coinAccount.MarketCode);
            coinMarketNameBuilder.AppendFormat(")");
            ColNameBuilder colBuilder = new ColNameBuilder();
            colBuilder.ColItem = ColNameBuilder.EColItem.MarketCode;
            colBuilder.GridType = ColNameBuilder.EGridType.myAsset;

            if (gridType == EMarketGridTabIdx.KRW)
            {
                colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.KRW;
                int rowIdx = dgvMyAssetKRW.Rows.Add();
                coinAccount.GridRowNumber = rowIdx;
                dgvMyAssetKRW[colBuilder.BuildColName(), rowIdx].Value = coinMarketNameBuilder.ToString();
            }
            else if (gridType == EMarketGridTabIdx.BTC)
            {
                //colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.BTC;
                //int rowIdx = dgvMarketBTC.Rows.Add();
                //coin.GridRowNumber = rowIdx;
                //dgvMarketBTC[colBuilder.BuildColName(), rowIdx].Value = coinMarketNameBuilder.ToString();
            }
            else if (gridType == EMarketGridTabIdx.USDT)
            {
                //int rowIdx = dgvMarketUSDT.Rows.Add();
                //coin.GridRowNumber = rowIdx;
                //colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.USDT;
                ////dgvMarketUSDT["USDT_marketInfo", rowIdx].Value = coinMakretNameBuilder.ToString();
                //dgvMarketUSDT[colBuilder.BuildColName(), rowIdx].Value = coinMarketNameBuilder.ToString();
            }
            else
            {
                Debug.Assert(false);
            }

            colBuilder = null;


        }

    }



}




