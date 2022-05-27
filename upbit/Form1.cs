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
using System.Reflection;

namespace upbit
{
    public partial class Base : Form
    {

        private APIClass API;

        private static string strSectionName = "OpenAPIKey";

        public Base()
        {
            InitializeComponent();
            button_LogIn.Click += ReceiveButtonEvent;
            button_CheckFee.Click += ReceiveButtonEvent;
            button_CheckBalance.Click += ReceiveButtonEvent;

        }

        private void FormLoadEvent(object sender, EventArgs e)
        {
            string root = Application.StartupPath;
            int nLastIdx = root.LastIndexOf('\\');
            int nFolderIdx = root.LastIndexOf('\\', nLastIdx - 1);

            StringBuilder sbINIFilePath = new StringBuilder(root);
            sbINIFilePath.Remove(nFolderIdx, root.Length - nFolderIdx);
            sbINIFilePath.AppendFormat("\\Setting\\UpbitKey\\FuckedUpKey.ini");

            if(!File.Exists(sbINIFilePath.ToString()))
            {
                return;
            }
            INIFile iniKeyFile = new INIFile(sbINIFilePath.ToString());
            StringBuilder sbReadKey = new StringBuilder();
            if(iniKeyFile.KeyExists(INIFile.ACCESS_KEY, strSectionName))
            {
                sbReadKey.AppendFormat(iniKeyFile.Read(INIFile.ACCESS_KEY, strSectionName));
            }

            textBox_AccessKey.AppendText(sbReadKey.ToString());
            sbReadKey.Clear();
            if(iniKeyFile.KeyExists(INIFile.SECRET_KEY, strSectionName))
            {
                sbReadKey.AppendFormat(iniKeyFile.Read(INIFile.SECRET_KEY, strSectionName));
            }
            textBox_SecretKey.AppendText(sbReadKey.ToString());
            sbReadKey.Clear();

            sbReadKey = null;
            iniKeyFile = null;

        }

        public void ReceiveButtonEvent(object sender, EventArgs e)
        {
            if(sender.Equals(button_LogIn))
            {
                API = new APIClass(textBox_AccessKey.Text, textBox_SecretKey.Text);
            }
            else if(sender.Equals(button_CheckFee))
            {
                //Console.WriteLine(API.GetAccount());
                Console.WriteLine(API.GetOrderChance("KRW-BTC"));

            }
            else if(sender.Equals(button_CheckBalance))
            {
                //비트코인 수수료
                //Console.WriteLine(API.GetOrderChance("KRW-BTC"));
                Console.WriteLine(API.GetAccount());
            }
        }

        private void FormClosingSaveKey(Object sender, FormClosingEventArgs e)
        {
            string strAccessKey = textBox_AccessKey.Text;
            string strSecretKey = textBox_SecretKey.Text;

            if (!checkBox_SaveKey.Checked)
            {
                ClearINIFile();
            }
            string root = Application.StartupPath;
            int nLastIdx = root.LastIndexOf('\\');
            int nFolderIdx = root.LastIndexOf('\\', nLastIdx - 1);
            //string strINIFilePath = root;

            StringBuilder sbINIFilePath = new StringBuilder(root);
            sbINIFilePath.Remove(nFolderIdx, root.Length - nFolderIdx);
            sbINIFilePath.AppendFormat("\\Setting\\UpbitKey");

            if(!Directory.Exists(sbINIFilePath.ToString()))
            {
                Directory.CreateDirectory(sbINIFilePath.ToString());
            }
            sbINIFilePath.AppendFormat("\\FuckedUpKey.ini");
            INIFile iniKeyFile = new INIFile(sbINIFilePath.ToString());
            if(iniKeyFile.KeyExists(INIFile.ACCESS_KEY))
            {
                string strReadAccessKey = iniKeyFile.Read(INIFile.ACCESS_KEY, strSectionName);
                iniKeyFile.DeleteKey(INIFile.ACCESS_KEY, strSectionName);
            }
            iniKeyFile.Write(INIFile.ACCESS_KEY, strAccessKey, "OpenAPIKey");

            if(iniKeyFile.KeyExists(INIFile.SECRET_KEY))
            {
                string strReadSecretKey = iniKeyFile.Read(INIFile.SECRET_KEY, strSectionName);
                iniKeyFile.DeleteKey(INIFile.SECRET_KEY, strSectionName);
            }
            iniKeyFile.Write(INIFile.SECRET_KEY, strSecretKey, "OpenAPIKey");
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
        }

    }
}
