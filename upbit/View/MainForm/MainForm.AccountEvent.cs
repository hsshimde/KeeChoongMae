using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using upbit.UpbitAPI.Model;
using upbit.ColumnNameBuilder;

namespace upbit.View
{
    public partial class MainForm
    {
        private void OnEventAccountAdd(object sender, Account account)
        {
            //if(dataGridView_Account.InvokeRequired)
            //{
            //    dataGridView_Account.Invoke((MethodInvoker)delegate () 
            //    {
            //        //DataGridViewRow updateRow = dataGridView_Account.Rows.Cast
            //        //IEnumerable<DataGridViewRow> IERows = dataGridView_Account.Rows.Cast<DataGridViewRow>();
            //        //DataGridViewRow = IERows.Where(x=>x.Cells["account_market"].Value.Equals(account.market)))
            //    });
            //}
        }
        private void OnEventAccountUpdate(object sender, CoinAccount coinAccount)
        {
            EGridKind eGridKind = coinAccount.GridKind;
            ColNameBuilder colBuilder = new ColNameBuilder();
            colBuilder.GridType = ColNameBuilder.EGridType.myAsset;
            switch (eGridKind)
            {
                case EGridKind.KRW:
                    {
                        if (dgvMyAssetKRW.InvokeRequired)
                        {
                            dgvMyAssetKRW.Invoke((MethodInvoker)delegate ()
                            {
                                colBuilder.UnitCurrency = ColNameBuilder.EUnitCurrency.KRW;
                                Color foreColorProfitFromBuyPoint;
                                Color foreColorProfitCompare24H;
                                if(coinAccount.ProfitPercentageBuy > 0.0f)
                                {
                                    foreColorProfitFromBuyPoint = Color.Tomato;
                                }
                                else if(coinAccount.ProfitPercentageBuy ==0.0f)
                                {
                                    foreColorProfitFromBuyPoint = Color.Black;
                                }
                                else
                                {
                                    foreColorProfitFromBuyPoint = Color.DodgerBlue;
                                }
                                if(coinAccount.ProfitPercentageCompare24H > 0.0f)
                                {
                                    foreColorProfitCompare24H = Color.Tomato;
                                }
                                else if (coinAccount.ProfitPercentageCompare24H == 0.0f)
                                {
                                    foreColorProfitCompare24H = Color.Black;
                                }
                                else
                                {
                                    foreColorProfitCompare24H = Color.DodgerBlue;
                                }

                                //colBuilder.ColItem = ColNameBuilder.EColItem.MarketCode;
                                //dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.RowNumber].
                                colBuilder.ColItem = ColNameBuilder.EColItem.OwnCount;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Value = coinAccount.Quantity;
                                colBuilder.ColItem = ColNameBuilder.EColItem.AvgBuyPrice;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Value = coinAccount.AvgBuyPrice;
                                colBuilder.ColItem = ColNameBuilder.EColItem.CurNetValue;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Style.ForeColor = foreColorProfitFromBuyPoint;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Value = Math.Floor(coinAccount.CurNetValue);
                                colBuilder.ColItem = ColNameBuilder.EColItem.BuyVolume;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Value = Math.Floor(coinAccount.AvgBuyPrice * coinAccount.Quantity);
                                colBuilder.ColItem = ColNameBuilder.EColItem.CurProfitPercentage;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Value = Math.Round(coinAccount.ProfitPercentageBuy, 2);
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Style.ForeColor = foreColorProfitFromBuyPoint;
                                colBuilder.ColItem = ColNameBuilder.EColItem.Compare24H;
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Value = Math.Round(coinAccount.ProfitPercentageCompare24H, 2);
                                dgvMyAssetKRW[colBuilder.BuildColName(), coinAccount.GridRowNumber].Style.ForeColor = foreColorProfitCompare24H;

                            });
                        }
                        //if (dgvMarketKRW.InvokeRequired)
                        //{
                        //    dgvMarketKRW.Invoke((MethodInvoker)delegate ()
                        //    {
                        //        //                updateRow.Cells["account_profit"].Style.ForeColor = Color.Tomato;

                        //        //dataGridView_KRW["KRW_marketInfo", coin.GridRowNumber].Value = coin.MarketCode;
                        //        if (coin.Compare24H > 0)
                        //        {
                        //            dgvMarketKRW["KRW_curPrice", coin.GridRowNumber].Style.ForeColor = Color.Tomato;
                        //            dgvMarketKRW["KRW_24H", coin.GridRowNumber].Style.ForeColor = Color.Tomato;
                        //        }
                        //        else if (coin.Compare24H == 0.0f)
                        //        {
                        //            dgvMarketKRW["KRW_curPrice", coin.GridRowNumber].Style.ForeColor = Color.Black;
                        //            dgvMarketKRW["KRW_24H", coin.GridRowNumber].Style.ForeColor = Color.Black;
                        //        }
                        //        else
                        //        {
                        //            dgvMarketKRW["KRW_curPrice", coin.GridRowNumber].Style.ForeColor = Color.DodgerBlue;
                        //            dgvMarketKRW["KRW_24H", coin.GridRowNumber].Style.ForeColor = Color.DodgerBlue;
                        //        }
                        //        dgvMarketKRW["KRW_curPrice", coin.GridRowNumber].Value = roundingForKRW(coin.CurPrice);
                        //        dgvMarketKRW["KRW_24H", coin.GridRowNumber].Value = coin.Compare24H + "%";
                        //        dgvMarketKRW["KRW_transPrice", coin.GridRowNumber].Value = roundingForTransKOR(coin.AccumulateTradePrice);
                        //    }
                        //    );
                        //}
                    }
                    break;


                case EGridKind.BTC:
                    {

                    }
                    break;

                case EGridKind.USDT:
                    {

                    }
                    break;
            }

            //if(dgv)q
            //if (dataGridView_Account.InvokeRequired)
            //{
            //    dataGridView_Account.Invoke((MethodInvoker)delegate ()
            //    {
            //        IEnumerable<DataGridViewRow> IERows = dataGridView_Account.Rows.Cast<DataGridViewRow>();
            //        DataGridViewRow updateRow = IERows.Where(x => x.Cells["account_market"].Value.Equals(account.strMarketCode)).FirstOrDefault();
            //        if (updateRow != null)
            //        {
            //            updateRow.Cells["account_profit"].Value = account.dblProfit.ToString("0.00\\");
            //            updateRow.Cells["account_quant"].Value = account.dblQuantity;
            //            updateRow.Cells["account_avgPrice"].Value = Rounding(account.dblAvgPrice);
            //            updateRow.Cells["account_curPrice"].Value = Rounding(account.dblCurPrice);
            //            if (account.dblProfit > 0)
            //            {
            //                updateRow.Cells["account_profit"].Style.ForeColor = Color.Tomato;
            //            }
            //            else if (account.dblProfit < 0)
            //            {
            //                updateRow.Cells["account_profit"].Style.ForeColor = Color.DodgerBlue;
            //            }
            //            else
            //            {
            //                updateRow.Cells["account_profit"].Style.ForeColor = Color.Black;
            //            }

            //        }
            //        else
            //        {
            //            int nRow = dataGridView_Account.Rows.Add();
            //            dataGridView_Account["account_market", nRow].Value = account.strMarketCode;
            //            dataGridView_Account["account_profit", nRow].Value = account.dblProfit.ToString("0.00\\");
            //            dataGridView_Account["account_quant", nRow].Value = account.dblQuantity;
            //            dataGridView_Account["account_avgPrice", nRow].Value = Rounding(account.dblAvgPrice);
            //            dataGridView_Account["account_curPrice", nRow].Value = Rounding(account.dblCurPrice);
            //        }
            //    });
            //}
        }

        private void OnEventAccountDelete(object sender, string strMarketInfo)
        {

            //if (dataGridView_Account.InvokeRequired)
            //{
            //    dataGridView_Account.Invoke((MethodInvoker)delegate () 
            //    {
            //        IEnumerable<DataGridViewRow> IEDeleteRow = dataGridView_Account.Rows.Cast<DataGridViewRow>();
            //        DataGridViewRow deleteRow = IEDeleteRow.Where(x => x.Cells["account_market"].Value.Equals(strMarketInfo)).FirstOrDefault();
                    
            //        if(deleteRow!=null)
            //        {
            //            dataGridView_Account.Rows.Remove(deleteRow);
            //        }
            //    });

            //}
        }

        private string Rounding(double dblNum)
        {

            if (dblNum >= 100)
            {
                return dblNum.ToString("C0");
            }
            else if (dblNum >= 1)
            {
                return dblNum.ToString("C2");
            }
            else
            {
                return dblNum.ToString("C4");
            }
        }

        private void OnUpdateTotalAsset(object sender, double dblTotal)
        {
            toolStripLabel_totalAsset.Text = dblTotal.ToString("C0");
        }
    }

}
