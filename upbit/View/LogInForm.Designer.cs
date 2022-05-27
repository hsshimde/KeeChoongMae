namespace upbit.View
{
    partial class LogInForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBox_SaveKey = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label_AccessKey = new System.Windows.Forms.Label();
            this.label_SecretKey = new System.Windows.Forms.Label();
            this.textBox_AccessKey = new System.Windows.Forms.TextBox();
            this.textBox_SecretKey = new System.Windows.Forms.TextBox();
            this.button_LogIn = new System.Windows.Forms.Button();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.checkBox_SaveKey, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_LogIn, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 43.82022F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 56.17978F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(341, 218);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // checkBox_SaveKey
            // 
            this.checkBox_SaveKey.AutoSize = true;
            this.checkBox_SaveKey.Dock = System.Windows.Forms.DockStyle.Left;
            this.checkBox_SaveKey.Location = new System.Drawing.Point(3, 170);
            this.checkBox_SaveKey.Name = "checkBox_SaveKey";
            this.checkBox_SaveKey.Size = new System.Drawing.Size(71, 45);
            this.checkBox_SaveKey.TabIndex = 2;
            this.checkBox_SaveKey.Text = "키 저장";
            this.checkBox_SaveKey.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.24521F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.75479F));
            this.tableLayoutPanel1.Controls.Add(this.label_AccessKey, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label_SecretKey, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_AccessKey, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox_SecretKey, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(335, 67);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // label_AccessKey
            // 
            this.label_AccessKey.AutoSize = true;
            this.label_AccessKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_AccessKey.Location = new System.Drawing.Point(4, 1);
            this.label_AccessKey.Name = "label_AccessKey";
            this.label_AccessKey.Size = new System.Drawing.Size(81, 32);
            this.label_AccessKey.TabIndex = 0;
            this.label_AccessKey.Text = "Access Key";
            this.label_AccessKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_AccessKey.Resize += new System.EventHandler(this.OnResize);
            // 
            // label_SecretKey
            // 
            this.label_SecretKey.AutoSize = true;
            this.label_SecretKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_SecretKey.Location = new System.Drawing.Point(4, 34);
            this.label_SecretKey.Name = "label_SecretKey";
            this.label_SecretKey.Size = new System.Drawing.Size(81, 32);
            this.label_SecretKey.TabIndex = 1;
            this.label_SecretKey.Text = "Secret Key";
            this.label_SecretKey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_AccessKey
            // 
            this.textBox_AccessKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_AccessKey.Location = new System.Drawing.Point(92, 4);
            this.textBox_AccessKey.Name = "textBox_AccessKey";
            this.textBox_AccessKey.PasswordChar = '*';
            this.textBox_AccessKey.Size = new System.Drawing.Size(239, 25);
            this.textBox_AccessKey.TabIndex = 2;
            // 
            // textBox_SecretKey
            // 
            this.textBox_SecretKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox_SecretKey.Location = new System.Drawing.Point(92, 37);
            this.textBox_SecretKey.Name = "textBox_SecretKey";
            this.textBox_SecretKey.PasswordChar = '*';
            this.textBox_SecretKey.Size = new System.Drawing.Size(239, 25);
            this.textBox_SecretKey.TabIndex = 3;
            // 
            // button_LogIn
            // 
            this.button_LogIn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_LogIn.Location = new System.Drawing.Point(3, 76);
            this.button_LogIn.Name = "button_LogIn";
            this.button_LogIn.Size = new System.Drawing.Size(335, 88);
            this.button_LogIn.TabIndex = 3;
            this.button_LogIn.Text = "로그인";
            this.button_LogIn.UseVisualStyleBackColor = true;
            this.button_LogIn.Click += new System.EventHandler(this.OnLogInButtonEvent);
            // 
            // LogInForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(341, 218);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "LogInForm";
            this.Text = "FuckedUp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClosingSaveKey);
            this.Load += new System.EventHandler(this.FormLoadEvent);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label_AccessKey;
        private System.Windows.Forms.Label label_SecretKey;
        private System.Windows.Forms.TextBox textBox_AccessKey;
        private System.Windows.Forms.TextBox textBox_SecretKey;
        private System.Windows.Forms.Button button_LogIn;
        private System.Windows.Forms.CheckBox checkBox_SaveKey;
    }
}