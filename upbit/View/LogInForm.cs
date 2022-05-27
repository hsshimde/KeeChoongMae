using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;
using System.Reflection;

namespace upbit.View
{
    public partial class LogInForm : Form
    {
        private APIClass API;
        public bool BLogInFlag { get; set; }
        private static string SECTION_NAME = "OpenAPIKey";
        private static string SHOULD_SAVE = "SHOULD_SAVE";
        private static string NO_NEED_TO_SAVE = "NO_NEED_TO_SAVE";

        public LogInForm()
        {
            InitializeComponent();
        }


        private void FormClosingSaveKey(Object sender, FormClosingEventArgs e)
        {
            string strAccessKey = textBox_AccessKey.Text;
            string strSecretKey = textBox_SecretKey.Text;

            if (!checkBox_SaveKey.Checked)
            {
                ClearINIFile();
                return;
            }
            string root = Application.StartupPath;
            int nLastIdx = root.LastIndexOf('\\');
            int nFolderIdx = root.LastIndexOf('\\', nLastIdx - 1);
            //string strINIFilePath = root;

            StringBuilder sbINIFilePath = new StringBuilder(root);
            sbINIFilePath.Remove(nFolderIdx, root.Length - nFolderIdx);
            sbINIFilePath.AppendFormat("\\Setting\\UpbitKey");

            if (!Directory.Exists(sbINIFilePath.ToString()))
            {
                Directory.CreateDirectory(sbINIFilePath.ToString());
            }
            sbINIFilePath.AppendFormat("\\FuckedUpKey.ini");
            INIFile iniKeyFile = new INIFile(sbINIFilePath.ToString());
            if (iniKeyFile.KeyExists(INIFile.ACCESS_KEY))
            {
                string strReadAccessKey = iniKeyFile.Read(INIFile.ACCESS_KEY, SECTION_NAME);
                iniKeyFile.DeleteKey(INIFile.ACCESS_KEY, SECTION_NAME);
            }
            iniKeyFile.Write(INIFile.ACCESS_KEY, strAccessKey, "OpenAPIKey");

            if (iniKeyFile.KeyExists(INIFile.SECRET_KEY))
            {
                string strReadSecretKey = iniKeyFile.Read(INIFile.SECRET_KEY, SECTION_NAME);
                iniKeyFile.DeleteKey(INIFile.SECRET_KEY, SECTION_NAME);
            }
            iniKeyFile.Write(INIFile.SECRET_KEY, strSecretKey, "OpenAPIKey");

            if(iniKeyFile.KeyExists(INIFile.SHOULD_SAVE_KEY))
            {
                string strReadSecretKey = iniKeyFile.Read(INIFile.SHOULD_SAVE_KEY, SECTION_NAME);
                if (strReadSecretKey.CompareTo(SHOULD_SAVE) != 0)
                {
                    iniKeyFile.DeleteKey(INIFile.SHOULD_SAVE_KEY, SECTION_NAME);
                    iniKeyFile.Write(INIFile.SHOULD_SAVE_KEY, SHOULD_SAVE, "OpenAPIKey");
                }
            }

        }

        private void ClearINIFile()
        {
            string root = Application.StartupPath;
            int nLastIdx = root.LastIndexOf('\\');
            int nFolderIdx = root.LastIndexOf('\\', nLastIdx - 1);
            //string strINIFilePath = root;

            StringBuilder sbINIFilePath = new StringBuilder(root);
            sbINIFilePath.Remove(nFolderIdx, root.Length - nFolderIdx);
            sbINIFilePath.AppendFormat("\\Setting\\UpbitKey");

            if (!Directory.Exists(sbINIFilePath.ToString()))
            {
                Directory.CreateDirectory(sbINIFilePath.ToString());
            }
            sbINIFilePath.AppendFormat("\\FuckedUpKey.ini");
            INIFile iniKeyFile = new INIFile(sbINIFilePath.ToString());

            if(iniKeyFile.KeyExists(INIFile.ACCESS_KEY, SECTION_NAME))
            {
                iniKeyFile.DeleteKey(INIFile.ACCESS_KEY, SECTION_NAME);
            }

            if (iniKeyFile.KeyExists(INIFile.SECRET_KEY, SECTION_NAME))
            {
                iniKeyFile.DeleteKey(INIFile.SECRET_KEY, SECTION_NAME);
            }

            if(iniKeyFile.KeyExists(INIFile.SHOULD_SAVE_KEY, SECTION_NAME))
            {
                iniKeyFile.DeleteKey(INIFile.SHOULD_SAVE_KEY, SECTION_NAME);
                iniKeyFile.Write(INIFile.SHOULD_SAVE_KEY, NO_NEED_TO_SAVE, SECTION_NAME);
            }


        }

        private void FormLoadEvent(object sender, EventArgs e)
        {
            string root = Application.StartupPath;
            int nLastIdx = root.LastIndexOf('\\');
            int nFolderIdx = root.LastIndexOf('\\', nLastIdx - 1);
            StringBuilder sbINIFilePath = new StringBuilder(root);
            sbINIFilePath.Remove(nFolderIdx, root.Length - nFolderIdx);
            sbINIFilePath.AppendFormat("\\Setting\\UpbitKey\\FuckedUpKey.ini");

            if (!File.Exists(sbINIFilePath.ToString()))
            {
                return;
            }
            SetSaveKeyCheckBox(sbINIFilePath);


            INIFile iniKeyFile = new INIFile(sbINIFilePath.ToString());


            StringBuilder sbReadKey = new StringBuilder();
            if (iniKeyFile.KeyExists(INIFile.ACCESS_KEY, SECTION_NAME))
            {
                sbReadKey.AppendFormat(iniKeyFile.Read(INIFile.ACCESS_KEY, SECTION_NAME));
            }

            textBox_AccessKey.AppendText(sbReadKey.ToString());
            sbReadKey.Clear();
            if (iniKeyFile.KeyExists(INIFile.SECRET_KEY, SECTION_NAME))
            {
                sbReadKey.AppendFormat(iniKeyFile.Read(INIFile.SECRET_KEY, SECTION_NAME));
            }
            textBox_SecretKey.AppendText(sbReadKey.ToString());
            sbReadKey.Clear();

            sbReadKey = null;
            iniKeyFile = null;

        }

        private async Task<bool> LogIn()
        {
            string strAccessKey = textBox_AccessKey.Text;
            string strSecretKey = textBox_SecretKey.Text;

            if(strAccessKey.Length < 15 || strSecretKey.Length < 15)
            {
                return false;
            }

            APIClass api = new APIClass(strAccessKey, strSecretKey);
            Task<List<Account>> taskAccountInfo = api.GetAccount();
            List<Account> accountInfo = await taskAccountInfo;

            if(accountInfo!=null)
            {
                this.API = api;
                return true;
            }
            else
            {
                return false;
            }
        }

        public APIClass GetUpbitAPI()
        {
            return this.API;
        }

        //public bool IsLogedIn()
        //{
        //    return bLogInFlag;
        //}
        


        private async void OnLogInButtonEvent(object sender, EventArgs e)
        {
            if (sender.Equals(button_LogIn))
            {
                Task<bool> loginTask = LogIn();
                BLogInFlag = await loginTask;
                if(BLogInFlag)
                {
                    Close();
                }

                else
                {
                    MessageBox.Show("로그인에 실패하였습니다");
                    //if(m_dlgNoConnection == null)
                    //{
                    //    m_dlgNoConnection = new ConnectionNoWorkDialogue();
                    //    m_dlgNoConnection.Owner = this;
                    //    m_dlgNoConnection.Show();
                    //}
                    //else
                    //{
                    //    m_dlgNoConnection.Focus();
                    //}

                }

            }
        }

        private void OnResize(object sender, EventArgs e)
        {
            //Control nextControl = this.GetNextControl(this, false);
            
            //while (nextControl != null)
            //{
            //    nextControl = nextControl.GetNextControl(nextControl, false);
            //}
        }

        private void SetSaveKeyCheckBox(StringBuilder sbPath)
        {
            INIFile iniKeyFile = new INIFile(sbPath.ToString());
            if (iniKeyFile.KeyExists(INIFile.SHOULD_SAVE_KEY, SECTION_NAME))
            {
                string strReadKey = iniKeyFile.Read(INIFile.SHOULD_SAVE_KEY, SECTION_NAME);
                if(strReadKey.CompareTo(SHOULD_SAVE) == 0)
                {
                    checkBox_SaveKey.CheckState = CheckState.Checked; 
                }
                else
                {
                    checkBox_SaveKey.CheckState = CheckState.Unchecked;
                }
            }
            iniKeyFile = null;
         }

    }


}
