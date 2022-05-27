namespace upbit
{
    partial class Base
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_LogIn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_AccessKey = new System.Windows.Forms.TextBox();
            this.textBox_SecretKey = new System.Windows.Forms.TextBox();
            this.button_CheckBalance = new System.Windows.Forms.Button();
            this.button_CheckFee = new System.Windows.Forms.Button();
            this.checkBox_SaveKey = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.72115F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.27885F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.button_LogIn, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_AccessKey, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_SecretKey, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.95062F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 32.71605F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(416, 122);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // button_LogIn
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.button_LogIn, 2);
            this.button_LogIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LogIn.Location = new System.Drawing.Point(4, 84);
            this.button_LogIn.Name = "button_LogIn";
            this.button_LogIn.Size = new System.Drawing.Size(408, 34);
            this.button_LogIn.TabIndex = 1;
            this.button_LogIn.Text = "로그인";
            this.button_LogIn.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "Access Key";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(4, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Secret Key";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_AccessKey
            // 
            this.textBox_AccessKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_AccessKey.Location = new System.Drawing.Point(111, 4);
            this.textBox_AccessKey.Name = "textBox_AccessKey";
            this.textBox_AccessKey.Size = new System.Drawing.Size(301, 25);
            this.textBox_AccessKey.TabIndex = 2;
            // 
            // textBox_SecretKey
            // 
            this.textBox_SecretKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SecretKey.Location = new System.Drawing.Point(111, 45);
            this.textBox_SecretKey.Name = "textBox_SecretKey";
            this.textBox_SecretKey.Size = new System.Drawing.Size(301, 25);
            this.textBox_SecretKey.TabIndex = 3;
            // 
            // button_CheckBalance
            // 
            this.button_CheckBalance.Location = new System.Drawing.Point(19, 169);
            this.button_CheckBalance.Name = "button_CheckBalance";
            this.button_CheckBalance.Size = new System.Drawing.Size(97, 74);
            this.button_CheckBalance.TabIndex = 1;
            this.button_CheckBalance.Text = "잔고확인";
            this.button_CheckBalance.UseVisualStyleBackColor = true;
            // 
            // button_CheckFee
            // 
            this.button_CheckFee.Location = new System.Drawing.Point(123, 169);
            this.button_CheckFee.Name = "button_CheckFee";
            this.button_CheckFee.Size = new System.Drawing.Size(97, 74);
            this.button_CheckFee.TabIndex = 2;
            this.button_CheckFee.Text = "수수료 확인";
            this.button_CheckFee.UseVisualStyleBackColor = true;
            // 
            // checkBox_SaveKey
            // 
            this.checkBox_SaveKey.AutoSize = true;
            this.checkBox_SaveKey.Location = new System.Drawing.Point(336, 140);
            this.checkBox_SaveKey.Name = "checkBox_SaveKey";
            this.checkBox_SaveKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.checkBox_SaveKey.Size = new System.Drawing.Size(71, 21);
            this.checkBox_SaveKey.TabIndex = 3;
            this.checkBox_SaveKey.Text = "키 저장";
            this.checkBox_SaveKey.UseVisualStyleBackColor = true;
            // 
            // Base
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(535, 330);
            this.Controls.Add(this.checkBox_SaveKey);
            this.Controls.Add(this.button_CheckFee);
            this.Controls.Add(this.button_CheckBalance);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Base";
            this.Text = "기충매";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingSaveKey);
            this.Load += new System.EventHandler(this.FormLoadEvent);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_AccessKey;
        private System.Windows.Forms.TextBox textBox_SecretKey;
        private System.Windows.Forms.Button button_LogIn;
        private System.Windows.Forms.Button button_CheckBalance;
        private System.Windows.Forms.Button button_CheckFee;
        private System.Windows.Forms.CheckBox checkBox_SaveKey;
    }
}

