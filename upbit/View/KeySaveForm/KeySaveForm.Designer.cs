namespace upbit.View.KeySaveForm
{
    partial class KeySaveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox_KeyList = new System.Windows.Forms.ComboBox();
            this.label_AccessKey = new System.Windows.Forms.Label();
            this.label_Secretkey = new System.Windows.Forms.Label();
            this.textBox_AccessKey = new System.Windows.Forms.TextBox();
            this.textBox_SecretKey = new System.Windows.Forms.TextBox();
            this.label_KeysNickname = new System.Windows.Forms.Label();
            this.textBox_KeysNickname = new System.Windows.Forms.TextBox();
            this.button_SaveKeys = new System.Windows.Forms.Button();
            this.button_DoLogIn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBox_KeyList
            // 
            this.comboBox_KeyList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_KeyList.Font = new System.Drawing.Font("굴림", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_KeyList.FormattingEnabled = true;
            this.comboBox_KeyList.Location = new System.Drawing.Point(630, 26);
            this.comboBox_KeyList.Name = "comboBox_KeyList";
            this.comboBox_KeyList.Size = new System.Drawing.Size(91, 27);
            this.comboBox_KeyList.TabIndex = 0;
            this.comboBox_KeyList.SelectedIndexChanged += new System.EventHandler(this.comboBox_KeyList_SelectedIndexChanged);
            // 
            // label_AccessKey
            // 
            this.label_AccessKey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_AccessKey.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_AccessKey.Location = new System.Drawing.Point(34, 26);
            this.label_AccessKey.Name = "label_AccessKey";
            this.label_AccessKey.Size = new System.Drawing.Size(107, 23);
            this.label_AccessKey.TabIndex = 1;
            this.label_AccessKey.Text = "Access Key";
            this.label_AccessKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Secretkey
            // 
            this.label_Secretkey.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Secretkey.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_Secretkey.Location = new System.Drawing.Point(34, 125);
            this.label_Secretkey.Name = "label_Secretkey";
            this.label_Secretkey.Size = new System.Drawing.Size(107, 23);
            this.label_Secretkey.TabIndex = 2;
            this.label_Secretkey.Text = "Secret Key";
            this.label_Secretkey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_AccessKey
            // 
            this.textBox_AccessKey.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_AccessKey.Location = new System.Drawing.Point(169, 25);
            this.textBox_AccessKey.Name = "textBox_AccessKey";
            this.textBox_AccessKey.Size = new System.Drawing.Size(432, 32);
            this.textBox_AccessKey.TabIndex = 3;
            // 
            // textBox_SecretKey
            // 
            this.textBox_SecretKey.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_SecretKey.Location = new System.Drawing.Point(169, 121);
            this.textBox_SecretKey.Name = "textBox_SecretKey";
            this.textBox_SecretKey.Size = new System.Drawing.Size(432, 32);
            this.textBox_SecretKey.TabIndex = 4;
            // 
            // label_KeysNickname
            // 
            this.label_KeysNickname.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_KeysNickname.Font = new System.Drawing.Font("굴림", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_KeysNickname.Location = new System.Drawing.Point(34, 205);
            this.label_KeysNickname.Name = "label_KeysNickname";
            this.label_KeysNickname.Size = new System.Drawing.Size(107, 23);
            this.label_KeysNickname.TabIndex = 5;
            this.label_KeysNickname.Text = "닉네임";
            this.label_KeysNickname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_KeysNickname
            // 
            this.textBox_KeysNickname.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_KeysNickname.Location = new System.Drawing.Point(169, 201);
            this.textBox_KeysNickname.Name = "textBox_KeysNickname";
            this.textBox_KeysNickname.Size = new System.Drawing.Size(432, 32);
            this.textBox_KeysNickname.TabIndex = 6;
            // 
            // button_SaveKeys
            // 
            this.button_SaveKeys.Location = new System.Drawing.Point(543, 254);
            this.button_SaveKeys.Name = "button_SaveKeys";
            this.button_SaveKeys.Size = new System.Drawing.Size(75, 37);
            this.button_SaveKeys.TabIndex = 7;
            this.button_SaveKeys.Text = "저장";
            this.button_SaveKeys.UseVisualStyleBackColor = true;
            this.button_SaveKeys.Click += new System.EventHandler(this.button_SaveKeys_Click);
            // 
            // button_DoLogIn
            // 
            this.button_DoLogIn.Location = new System.Drawing.Point(648, 254);
            this.button_DoLogIn.Name = "button_DoLogIn";
            this.button_DoLogIn.Size = new System.Drawing.Size(73, 37);
            this.button_DoLogIn.TabIndex = 8;
            this.button_DoLogIn.Text = "로그인";
            this.button_DoLogIn.UseVisualStyleBackColor = true;
            this.button_DoLogIn.Click += new System.EventHandler(this.button_DoLogIn_Click);
            // 
            // KeySaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 303);
            this.Controls.Add(this.button_DoLogIn);
            this.Controls.Add(this.button_SaveKeys);
            this.Controls.Add(this.textBox_KeysNickname);
            this.Controls.Add(this.label_KeysNickname);
            this.Controls.Add(this.textBox_SecretKey);
            this.Controls.Add(this.textBox_AccessKey);
            this.Controls.Add(this.label_Secretkey);
            this.Controls.Add(this.label_AccessKey);
            this.Controls.Add(this.comboBox_KeyList);
            this.Name = "KeySaveForm";
            this.Text = "키 저장";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_KeyList;
        private System.Windows.Forms.Label label_AccessKey;
        private System.Windows.Forms.Label label_Secretkey;
        private System.Windows.Forms.TextBox textBox_AccessKey;
        private System.Windows.Forms.TextBox textBox_SecretKey;
        private System.Windows.Forms.Label label_KeysNickname;
        private System.Windows.Forms.TextBox textBox_KeysNickname;
        private System.Windows.Forms.Button button_SaveKeys;
        private System.Windows.Forms.Button button_DoLogIn;
    }
}