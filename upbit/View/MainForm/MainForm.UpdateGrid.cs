using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;

using upbit.UpbitAPI.Model;
using upbit.ColumnNameBuilder;


namespace upbit.View
{
    internal class MonitoringEvent
    {
    }

    public partial class MainForm
    {
        private void AddMonitoringGridCol(StringBuilder marketInfoBuilder, StringBuilder coinNameBuilder)
        {
            string marketInfos = marketInfoBuilder.ToString();
            string coinNames = coinNameBuilder.ToString();
            string[] marketArray = marketInfos.Split(',');
            string[] coinNamesArray = coinNames.Split(',');
            int nArraySize = marketArray.Count();
            Debug.Assert(coinNamesArray.Count() == nArraySize);
            for (int nIdx = 0; nIdx < nArraySize; nIdx++)
            {
                int row = dgvMarketInterestCoin.Rows.Add();
                dgvMarketInterestCoin["Monitoring_marketInfo", row].Value = marketArray[nIdx];
                dgvMarketInterestCoin["Monitoring_koreanName", row].Value = coinNamesArray[nIdx];
            }
        }

        private void OnMonitoringEvent(object sender, Monitoring e)
        {
            if (dgvMarketInterestCoin.InvokeRequired)
            {
                dgvMarketInterestCoin.Invoke((MethodInvoker)delegate ()
                {
                    IEnumerable<DataGridViewRow> updateRowEnum = dgvMarketInterestCoin.Rows.Cast<DataGridViewRow>();
                    DataGridViewRow updateRow = updateRowEnum.Where(x => x.Cells["Monitoring_marketInfo"].Value.Equals(e.MarketInfo)).FirstOrDefault();
                    if (updateRow != null)
                    {
                        updateRow.Cells["Monitoring_curPrice"].Value = e.CurPrice;
                        updateRow.Cells["Monitoring_24H"].Value = e._24Hour.ToString("N2");
                    }

                });

            }
        }

        private void OnUpdateTickerEvent(object sender, Coin coin)
        {
            EMarketGridTabIdx eGridKind = coin.MarketGridTabIdx;
            ColNameBuilder colBuilder = new ColNameBuilder();
            colBuilder.GridType = ColNameBuilder.EGridType.market;
            switch (eGridKind)
            {
                case EMarketGridTabIdx.BTC:
                    {
                        if (dgvMarketBTC.InvokeRequired)
                        {
                            dgvMarketBTC.Invoke((MethodInvoker)delegate ()
                            {
                                //                updateRow.Cells["account_profit"].Style.ForeColor = Color.Tomato;
                                colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.BTC;
                                lock (dgvMarketBTC)
                                {

                                    Color foreColor;
                                    if (coin.Compare24H > 0)
                                    {
                                        foreColor = Color.Tomato;

                                    }
                                    else if (coin.Compare24H == 0.0f)
                                    {
                                        foreColor = Color.Black;
                                    }
                                    else
                                    {
                                        foreColor = Color.DodgerBlue;
                                    }
                                    //colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                    //dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Black;
                                    //colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                    //dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Black;
                                    colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                    dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Value = roundingForBTC(coin.CurPrice);
                                    dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = foreColor;
                                    colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                    dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Value = coin.Compare24H + "%";
                                    dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = foreColor;
                                    colBuilder.ColItem = ColNameBuilder.EColItem.TransVolume;
                                    dgvMarketBTC[colBuilder.BuildColName(), coin.GridRowNumber].Value = roundingForTransBTC(coin.AccumulateTradePrice);
                                }

                            }
                            );
                        }
                    }
                    break;

                case EMarketGridTabIdx.KRW:
                    {
                        if (dgvMarketKRW.InvokeRequired)
                        {
                            dgvMarketKRW.Invoke((MethodInvoker)delegate ()
                            {
                                //                updateRow.Cells["account_profit"].Style.ForeColor = Color.Tomato;
                                colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.KRW;
                                lock (dgvMarketKRW)
                                {

                                    //dataGridView_KRW["KRW_marketInfo", coin.GridRowNumber].Value = coin.MarketCode;
                                    if (coin.Compare24H > 0)
                                    {
                                        colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                        dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Tomato;
                                        colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                        dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Tomato;
                                    }
                                    else if (coin.Compare24H == 0.0f)
                                    {
                                        colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                        dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Black;
                                        colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                        dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                        colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                        dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.DodgerBlue;
                                        colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                        dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.DodgerBlue;
                                    }
                                    colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                    dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Value = roundingForKRW(coin.CurPrice);
                                    colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                    dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Value = coin.Compare24H + "%";
                                    colBuilder.ColItem = ColNameBuilder.EColItem.TransVolume;
                                    dgvMarketKRW[colBuilder.BuildColName(), coin.GridRowNumber].Value = roundingForTransKOR(coin.AccumulateTradePrice);
                                }
                            }
                            );
                        }
                    }
                    break;

                case EMarketGridTabIdx.USDT:
                    {
                        if (dgvMarketUSDT.InvokeRequired)
                        {
                            colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.USDT;
                            dgvMarketUSDT.Invoke((MethodInvoker)delegate ()
                            {
                                lock (dgvMarketUSDT)
                                {

                                    if (coin.Compare24H > 0)
                                    {
                                        colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                        dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Tomato;
                                        colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                        dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Tomato;
                                    }
                                    else if (coin.Compare24H == 0.0f)
                                    {
                                        colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                        dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Black;
                                        colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                        dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.Black;
                                    }
                                    else
                                    {
                                        colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                        dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.DodgerBlue;
                                        colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                        dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Style.ForeColor = Color.DodgerBlue;
                                    }
                                    colBuilder.ColItem = ColNameBuilder.EColItem.CurPrice;
                                    dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Value = roundingForUSDT(coin.CurPrice);
                                    colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                    dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Value = coin.Compare24H + "%";
                                    colBuilder.ColItem = ColNameBuilder.EColItem.TransVolume;
                                    dgvMarketUSDT[colBuilder.BuildColName(), coin.GridRowNumber].Value = roundingForUSDT(coin.AccumulateTradePrice);
                                }
                            }
                            );
                        }
                    }
                    break;

                default:
                    {
                        Debug.Assert(false);
                    }
                    break;
            }

        }
        private string roundingForKRW(double dblNum)
        {
            if (dblNum >= 100)
            {
                //return dblNum.ToString();
                string number = string.Format("{0:#,###}", dblNum);
                return number;
            }
            else if (dblNum >= 1)
            {
                return dblNum.ToString("F2");
            }
            else
            {
                return dblNum.ToString("F4");
            }
        }

        private string roundingForBTC(double price)
        {
            return price.ToString("F8");
        }

        private string roundingForUSDT(double price)
        {

            if (price >= 100)
            {
                string number = string.Format("{0:#,###}", price);
                return number;
            }
            else if (price >= 1)
            {
                return price.ToString("F2");
            }
            else
            {
                return price.ToString("F4");
            }
        }

        private string roundingForTransKOR(double price)
        {
            StringBuilder transPriceBuilder = new StringBuilder();
            decimal roundedPrice = (decimal)(price / 1000000);
            string roundedPriceString = string.Format("{0:#,###}", roundedPrice);
            transPriceBuilder.AppendFormat(roundedPriceString);
            transPriceBuilder.AppendFormat("백만");
            return transPriceBuilder.ToString();
        }

        private string roundingForTransBTC(double price)
        {
            StringBuilder sbTransPriceBuilder = new StringBuilder();
            string roundedPriceString = string.Format("{0:0.000}", Math.Round(price, 4));

            return roundedPriceString;
        }
    }

}
