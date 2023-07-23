using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using upbit.UpbitAPI;
using upbit.UpbitAPI.Model;

namespace upbit.View.KeySaveForm
{
    public partial class KeySaveForm : Form
    {
        private const string KEY_SECTION_NAME = "OpenAPIKey";
        private const string COUNT_SECTION_NAME = "COUNT_OF_KEYS";
        private const string SHOULD_SAVE = "SHOULD_SAVE";
        private const string NO_NEED_TO_SAVE = "NO_NEED_TO_SAVE";
        private const string COUNT = "COUNT";
        private const string NICKNAME = "NICKNAME";
        private const string LAST_LOGIN_LOCATION_SECTION_NAME = "LAST LOG IN LOCATION SECTION";
        private const string LAST_LOGIN_INFO = "LAST LOG IN LOCATION";
        private HashSet<string> mNicknameTable;
        public APIClass mAPI { get; private set; }

        public struct SKey
        {
            public string AccessKey { get; set; }
            public string SecretKey { get; set; }
            public string KeyNickName { get; set; }
        }

        private List<SKey> mKeysList;

        private INIFile mINIFile;

        public bool IsLoggedIn { get; private set; }

        public KeySaveForm()
        {
            InitializeComponent();
            mKeysList = new List<SKey>();
            mNicknameTable = new HashSet<string>();
            AddEventHandler();
            setUpButtonClrs();
            Initalize();
            bool bLoadINIFileResult = LoadINIFileInfo();
            if (!bLoadINIFileResult)
            {
                Console.WriteLine("INI File 로드에 실패하였습니다");
            }
        }

        private void setUpButtonClrs()
        {
            button_DoLogIn.BackColor = Color.Red;
            button_SaveKeys.BackColor = Color.Lime;
        }


        private bool Initalize()
        {
            string root = Application.StartupPath;
            int nLastIdx = root.LastIndexOf('\\');
            int nFolderIdx = root.LastIndexOf('\\', nLastIdx - 1);
            StringBuilder sbINIFilePath = new StringBuilder(root);
            sbINIFilePath.Remove(nFolderIdx, root.Length - nFolderIdx);
            sbINIFilePath.AppendFormat("\\Setting\\UpbitKey\\MyUpbitKeys.ini");

            if (!File.Exists(sbINIFilePath.ToString()))
            {
                return false;
            }
            mINIFile = new INIFile(sbINIFilePath.ToString());

            return true;
        }

        private bool LoadINIFileInfo()
        {
            int keysCount;

            if (mINIFile.KeyExists(COUNT, COUNT_SECTION_NAME))
            {
                string strReadKey = mINIFile.Read(COUNT, COUNT_SECTION_NAME);
                keysCount = int.Parse(strReadKey);
            }
            else
            {
                Debug.Assert(false, "There is no Count of keys in the file");
                return false;
            }

            bool bReadKeyResult = readEachKey(keysCount);
            int selectedIdx = readLastLocationIdx();
            comboBox_KeyList.TabIndex = selectedIdx;
            Debug.Assert(selectedIdx < mKeysList.Count, "Tab Index Wrong");
            SKey lastUsedKey = mKeysList.ElementAt(selectedIdx);
            textBox_AccessKey.Text = lastUsedKey.AccessKey;
            textBox_SecretKey.Text = lastUsedKey.SecretKey;
            textBox_KeysNickname.Text = lastUsedKey.KeyNickName;
            comboBox_KeyList.Text = lastUsedKey.KeyNickName;
            return bReadKeyResult;
        }

        private int readLastLocationIdx()
        {
            int returnIdx;
            if(mINIFile.KeyExists(LAST_LOGIN_INFO, LAST_LOGIN_LOCATION_SECTION_NAME))
            {
                string lastLocationIdxInString = mINIFile.Read(LAST_LOGIN_INFO, LAST_LOGIN_LOCATION_SECTION_NAME);
                returnIdx = int.Parse(lastLocationIdxInString);
            }
            else
            {
                returnIdx = 0;
            }
            return returnIdx;
        }

        private bool readEachKey(int keysCount)
        {
            StringBuilder sbFindKeys = new StringBuilder();
            for (int idx = 0; idx < keysCount; idx++)
            {
                sbFindKeys.AppendFormat(INIFile.ACCESS_KEY + "_{0}", idx);
                SKey key = new SKey();

                if (mINIFile.KeyExists(sbFindKeys.ToString(), KEY_SECTION_NAME))
                {
                    key.AccessKey = mINIFile.Read(sbFindKeys.ToString(), KEY_SECTION_NAME);
                }
                else
                {
                    return false;
                }
                sbFindKeys.Clear();
                sbFindKeys.AppendFormat(INIFile.SECRET_KEY + "_{0}", idx);

                if (mINIFile.KeyExists(sbFindKeys.ToString(), KEY_SECTION_NAME))
                {
                    key.SecretKey = mINIFile.Read(sbFindKeys.ToString(), KEY_SECTION_NAME);
                }
                else
                {
                    return false;
                }

                sbFindKeys.Clear();
                sbFindKeys.AppendFormat(NICKNAME + "_{0}", idx);

                if (mINIFile.KeyExists(sbFindKeys.ToString(), KEY_SECTION_NAME))
                {
                    key.KeyNickName= mINIFile.Read(sbFindKeys.ToString(), KEY_SECTION_NAME);
                }
                else
                {
                    return false;
                }

                mKeysList.Add(key);
                mNicknameTable.Add(key.KeyNickName);
                comboBox_KeyList.Items.Add(key.KeyNickName);
                sbFindKeys.Clear();
            }
            return true;
        }

        private void AddEventHandler()
        {
            //comboBox_KeyList.TabIndexChanged += OnComboBoxIndexChanged;
            this.Closed += new EventHandler(this.OnSaveKeyFormClosed);
        }

        private void OnSaveKeyFormClosed(object sender, EventArgs e)
        {
            saveLastLocation();
        }

      
       

        private void button_SaveKeys_Click(object sender, EventArgs e)
        {
            string accessKey = textBox_AccessKey.Text;
            string secretKey = textBox_SecretKey.Text;
            string keysNickName = textBox_KeysNickname.Text;
            if (accessKey.Length < 1)
            {
                MessageBox.Show("엑세스 키가 적합하지 않습니다.");
                return;
            }

            if (secretKey.Length < 1)
            {
                MessageBox.Show("시크릿 키가 적합하지 않습니다.");
                return;
            }

            if (keysNickName.Length < 1)
            {
                const string message = "키 별명이 매우 짧습니다.";
                DialogResult result = MessageBox.Show(message);
            }

            SKey newKey = new SKey();
            newKey.AccessKey = textBox_AccessKey.Text;
            newKey.SecretKey = textBox_SecretKey.Text;
            newKey.KeyNickName = textBox_KeysNickname.Text;
            if (mNicknameTable.Contains(newKey.KeyNickName))
            {
                const string message = "이미 같은 별명의 키 값이 존재합니다!";
                MessageBox.Show(message);
                return;
            }

            mKeysList.Add(newKey);
            mNicknameTable.Add(newKey.KeyNickName);
            saveToINIFile();
        }

        private void saveToINIFile()
        {
            saveCountOfKeys();
            saveEachKey();
            saveLastLocation();
        }

       
        private void saveLastLocation()
        {
            int selIdx = comboBox_KeyList.SelectedIndex;
            if(mINIFile.KeyExists(LAST_LOGIN_INFO, LAST_LOGIN_LOCATION_SECTION_NAME))
            {
                mINIFile.DeleteKey(LAST_LOGIN_INFO, LAST_LOGIN_INFO);
            }
            string lastLogIdx = selIdx.ToString();
            mINIFile.Write(LAST_LOGIN_INFO, lastLogIdx, LAST_LOGIN_LOCATION_SECTION_NAME);
        }

        private void saveCountOfKeys()
        {
            StringBuilder sbKey = new StringBuilder();
            sbKey.AppendFormat(COUNT);
            if (mINIFile.KeyExists(sbKey.ToString(), COUNT_SECTION_NAME))
            {
                string keyCountInString = mINIFile.Read(COUNT, COUNT_SECTION_NAME);
                int keysCount = int.Parse(keyCountInString);
                if (keysCount != mKeysList.Count)
                {
                    mINIFile.DeleteKey(COUNT, COUNT_SECTION_NAME);
                    int keysCountWithNewKey = mKeysList.Count;
                    string addedKeysCount = keysCountWithNewKey.ToString();
                    mINIFile.Write(COUNT, addedKeysCount, COUNT_SECTION_NAME);
                }
            }
            else
            {
                int keysCountWithNewKey = mKeysList.Count;
                string addedKeysCount = keysCountWithNewKey.ToString();
                mINIFile.Write(COUNT, addedKeysCount, COUNT_SECTION_NAME);
            }

        }

        private void saveEachKey()
        {
            StringBuilder sbKey = new StringBuilder();
            for (int idx = 0; idx < mKeysList.Count; idx++)
            {
                sbKey.Clear();
                sbKey.AppendFormat(INIFile.ACCESS_KEY + "_{0}", idx);
                if (!mINIFile.KeyExists(sbKey.ToString(), KEY_SECTION_NAME))
                {
                    Debug.Assert(idx < mKeysList.Count, "Index Exceeded");
                    SKey key = mKeysList.ElementAt(idx);
                    mINIFile.Write(sbKey.ToString(), key.AccessKey, KEY_SECTION_NAME);
                }

                sbKey.Clear();
                sbKey.AppendFormat(INIFile.SECRET_KEY + "_{0}", idx);
                if (!mINIFile.KeyExists(sbKey.ToString(), KEY_SECTION_NAME))
                {
                    Debug.Assert(idx < mKeysList.Count, "Index Exceeded");
                    SKey key = mKeysList.ElementAt(idx);
                    mINIFile.Write(sbKey.ToString(), key.SecretKey, KEY_SECTION_NAME);
                }


                sbKey.Clear();
                sbKey.AppendFormat(NICKNAME + "_{0}", idx);
                if (!mINIFile.KeyExists(sbKey.ToString(), KEY_SECTION_NAME))
                {
                    Debug.Assert(idx < mKeysList.Count, "Index Exceeded");
                    SKey key = mKeysList.ElementAt(idx);
                    mINIFile.Write(sbKey.ToString(), key.KeyNickName, KEY_SECTION_NAME);
                }
            }

        }

        private async void button_DoLogIn_Click(object sender, EventArgs e)
        {
            Task<bool> taskLogIn = DoLogIn();
            IsLoggedIn = await taskLogIn;

            if (IsLoggedIn)
            {
                Close();
            }

            else
            {
                MessageBox.Show("로그인에 실패 하였습니다");
            }
        }
        private async Task<bool> DoLogIn()
        {
            string accessKey = textBox_AccessKey.Text;
            string secretKey = textBox_SecretKey.Text;

            if (accessKey.Length < 15 || secretKey.Length < 15)
            {
                return false;
            }

            mAPI = new APIClass(accessKey, secretKey);
            Task<List<Account>> taskAccountInfo = mAPI.GetAccount();
            List<Account> accountInfo = await taskAccountInfo;

            if (accountInfo != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void comboBox_KeyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int curTabIdx = comboBox_KeyList.SelectedIndex;
            SKey selKeys = mKeysList.ElementAt(curTabIdx);
            textBox_AccessKey.Text = selKeys.AccessKey;
            textBox_SecretKey.Text = selKeys.SecretKey;
            textBox_KeysNickname.Text = selKeys.KeyNickName;
        }
    }
}
