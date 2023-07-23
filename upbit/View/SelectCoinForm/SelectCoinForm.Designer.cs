namespace upbit.View
{
    partial class SelectCoinForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.button_ToRight = new System.Windows.Forms.Button();
            this.button_ToLeft = new System.Windows.Forms.Button();
            this.dataGridView_Left = new System.Windows.Forms.DataGridView();
            this.LeftCol_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.LeftCol_MarketInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeftCol_KoreanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView_Right = new System.Windows.Forms.DataGridView();
            this.RightCol_Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.RightCol_MarketInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightCol_KoreanName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_Save = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Right)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_Save, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.21277F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.787234F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(774, 470);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView_Left, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dataGridView_Right, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(768, 418);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.button_ToRight, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.button_ToLeft, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(348, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(70, 412);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // button_ToRight
            // 
            this.button_ToRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ToRight.Location = new System.Drawing.Point(3, 291);
            this.button_ToRight.Name = "button_ToRight";
            this.button_ToRight.Size = new System.Drawing.Size(64, 55);
            this.button_ToRight.TabIndex = 0;
            this.button_ToRight.Text = "▶";
            this.button_ToRight.UseVisualStyleBackColor = true;
            this.button_ToRight.Click += new System.EventHandler(this.OnButtonClicked);
            // 
            // button_ToLeft
            // 
            this.button_ToLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ToLeft.Location = new System.Drawing.Point(3, 352);
            this.button_ToLeft.Name = "button_ToLeft";
            this.button_ToLeft.Size = new System.Drawing.Size(64, 57);
            this.button_ToLeft.TabIndex = 1;
            this.button_ToLeft.Text = "◀";
            this.button_ToLeft.UseVisualStyleBackColor = true;
            this.button_ToLeft.Click += new System.EventHandler(this.OnButtonClicked);
            // 
            // dataGridView_Left
            // 
            this.dataGridView_Left.AllowUserToAddRows = false;
            this.dataGridView_Left.AllowUserToDeleteRows = false;
            this.dataGridView_Left.AllowUserToResizeColumns = false;
            this.dataGridView_Left.AllowUserToResizeRows = false;
            this.dataGridView_Left.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Left.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LeftCol_Select,
            this.LeftCol_MarketInfo,
            this.LeftCol_KoreanName});
            this.dataGridView_Left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Left.Location = new System.Drawing.Point(3, 3);
            this.dataGridView_Left.Name = "dataGridView_Left";
            this.dataGridView_Left.RowHeadersVisible = false;
            this.dataGridView_Left.RowTemplate.Height = 23;
            this.dataGridView_Left.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Left.Size = new System.Drawing.Size(339, 412);
            this.dataGridView_Left.TabIndex = 1;
            // 
            // LeftCol_Select
            // 
            this.LeftCol_Select.HeaderText = "선택";
            this.LeftCol_Select.Name = "LeftCol_Select";
            this.LeftCol_Select.Width = 50;
            // 
            // LeftCol_MarketInfo
            // 
            this.LeftCol_MarketInfo.HeaderText = "시장 정보";
            this.LeftCol_MarketInfo.Name = "LeftCol_MarketInfo";
            this.LeftCol_MarketInfo.ReadOnly = true;
            this.LeftCol_MarketInfo.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LeftCol_MarketInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LeftCol_KoreanName
            // 
            this.LeftCol_KoreanName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LeftCol_KoreanName.HeaderText = "이름";
            this.LeftCol_KoreanName.Name = "LeftCol_KoreanName";
            this.LeftCol_KoreanName.ReadOnly = true;
            this.LeftCol_KoreanName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.LeftCol_KoreanName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridView_Right
            // 
            this.dataGridView_Right.AllowUserToAddRows = false;
            this.dataGridView_Right.AllowUserToDeleteRows = false;
            this.dataGridView_Right.AllowUserToResizeColumns = false;
            this.dataGridView_Right.AllowUserToResizeRows = false;
            this.dataGridView_Right.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Right.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RightCol_Select,
            this.RightCol_MarketInfo,
            this.RightCol_KoreanName});
            this.dataGridView_Right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView_Right.Location = new System.Drawing.Point(424, 3);
            this.dataGridView_Right.Name = "dataGridView_Right";
            this.dataGridView_Right.RowHeadersVisible = false;
            this.dataGridView_Right.RowTemplate.Height = 23;
            this.dataGridView_Right.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView_Right.Size = new System.Drawing.Size(341, 412);
            this.dataGridView_Right.TabIndex = 2;
            // 
            // RightCol_Select
            // 
            this.RightCol_Select.HeaderText = "선택";
            this.RightCol_Select.Name = "RightCol_Select";
            this.RightCol_Select.Width = 50;
            // 
            // RightCol_MarketInfo
            // 
            this.RightCol_MarketInfo.HeaderText = "시장 정보";
            this.RightCol_MarketInfo.Name = "RightCol_MarketInfo";
            // 
            // RightCol_KoreanName
            // 
            this.RightCol_KoreanName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.RightCol_KoreanName.HeaderText = "이름";
            this.RightCol_KoreanName.Name = "RightCol_KoreanName";
            this.RightCol_KoreanName.ReadOnly = true;
            // 
            // button_Save
            // 
            this.button_Save.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_Save.Location = new System.Drawing.Point(3, 427);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(768, 40);
            this.button_Save.TabIndex = 1;
            this.button_Save.Text = "저장";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.OnButtonClicked);
            // 
            // SelectCoinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 470);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "SelectCoinForm";
            this.Text = "SelectCoinForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Right)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button button_ToRight;
        private System.Windows.Forms.DataGridView dataGridView_Left;
        private System.Windows.Forms.DataGridView dataGridView_Right;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.DataGridViewCheckBoxColumn LeftCol_Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftCol_MarketInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeftCol_KoreanName;
        private System.Windows.Forms.Button button_ToLeft;
        private System.Windows.Forms.DataGridViewCheckBoxColumn RightCol_Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightCol_MarketInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightCol_KoreanName;
    }
}