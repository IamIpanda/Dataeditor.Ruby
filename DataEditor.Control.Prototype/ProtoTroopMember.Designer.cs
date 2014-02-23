namespace DataEditor.Control.Prototype
{
    partial class ProtoTroopMember
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtoTroopMember));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btAuto = new System.Windows.Forms.Button();
            this.btBackground = new System.Windows.Forms.Button();
            this.btBattleTest = new System.Windows.Forms.Button();
            this.troopMain = new DataEditor.Control.Prototype.ProtoTroopBitmap();
            this.lbList = new DataEditor.Control.Prototype.ProtoListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.btAdd = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btClarify = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbFlash = new System.Windows.Forms.CheckBox();
            this.cbUndead = new System.Windows.Forms.CheckBox();
            this.cbType = new DataEditor.Control.Prototype.ProtoComboBox();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.794683F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.08715F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.btAuto, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.btBackground, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.btBattleTest, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.troopMain, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbList, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textBox1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(783, 324);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // btAuto
            // 
            this.btAuto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btAuto.Location = new System.Drawing.Point(315, 3);
            this.btAuto.Name = "btAuto";
            this.btAuto.Size = new System.Drawing.Size(150, 20);
            this.btAuto.TabIndex = 0;
            this.btAuto.Text = "自动命名";
            this.btAuto.UseVisualStyleBackColor = true;
            this.btAuto.Click += new System.EventHandler(this.btAuto_Click);
            // 
            // btBackground
            // 
            this.btBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btBackground.Location = new System.Drawing.Point(471, 3);
            this.btBackground.Name = "btBackground";
            this.btBackground.Size = new System.Drawing.Size(150, 20);
            this.btBackground.TabIndex = 1;
            this.btBackground.Text = "更换背景";
            this.btBackground.UseVisualStyleBackColor = true;
            this.btBackground.Click += new System.EventHandler(this.btBackground_Click);
            // 
            // btBattleTest
            // 
            this.btBattleTest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btBattleTest.Enabled = false;
            this.btBattleTest.Location = new System.Drawing.Point(627, 3);
            this.btBattleTest.Name = "btBattleTest";
            this.btBattleTest.Size = new System.Drawing.Size(153, 20);
            this.btBattleTest.TabIndex = 2;
            this.btBattleTest.Text = "战斗测试";
            this.btBattleTest.UseVisualStyleBackColor = true;
            this.btBattleTest.Click += new System.EventHandler(this.btBattleTest_Click);
            // 
            // troopMain
            // 
            this.troopMain.Background = null;
            this.troopMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tableLayoutPanel1.SetColumnSpan(this.troopMain, 3);
            this.troopMain.Components = ((System.Collections.Generic.List<System.Drawing.Bitmap>)(resources.GetObject("troopMain.Components")));
            this.troopMain.Coodinates = ((System.Collections.Generic.List<System.Drawing.Point>)(resources.GetObject("troopMain.Coodinates")));
            this.troopMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.troopMain.Frontground = null;
            this.troopMain.Location = new System.Drawing.Point(3, 29);
            this.troopMain.Name = "troopMain";
            this.tableLayoutPanel1.SetRowSpan(this.troopMain, 2);
            this.troopMain.SelectedIndex = -1;
            this.troopMain.Size = new System.Drawing.Size(462, 292);
            this.troopMain.TabIndex = 3;
            this.troopMain.SelectedIndexChanged += new System.EventHandler(this.troopMain_SelectedIndexChanged);
            this.troopMain.SelectedBitmapMoved += new System.EventHandler(this.troopMain_SelectedBitmapMoved);
            // 
            // lbList
            // 
            this.lbList.DisappearRectLosingFocus = false;
            this.lbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbList.FormattingEnabled = true;
            this.lbList.Location = new System.Drawing.Point(627, 106);
            this.lbList.Name = "lbList";
            this.lbList.Size = new System.Drawing.Size(153, 215);
            this.lbList.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 26);
            this.label2.TabIndex = 6;
            this.label2.Text = "名称";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox1.Location = new System.Drawing.Point(56, 3);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(253, 21);
            this.textBox1.TabIndex = 7;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.btAdd, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.btDelete, 0, 3);
            this.tableLayoutPanel3.Controls.Add(this.btClear, 0, 5);
            this.tableLayoutPanel3.Controls.Add(this.btClarify, 0, 7);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(471, 106);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 9;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 27F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(150, 215);
            this.tableLayoutPanel3.TabIndex = 9;
            // 
            // btAdd
            // 
            this.btAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btAdd.Location = new System.Drawing.Point(3, 24);
            this.btAdd.Name = "btAdd";
            this.btAdd.Size = new System.Drawing.Size(144, 21);
            this.btAdd.TabIndex = 0;
            this.btAdd.Text = "< 添加";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // btDelete
            // 
            this.btDelete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btDelete.Location = new System.Drawing.Point(3, 72);
            this.btDelete.Name = "btDelete";
            this.btDelete.Size = new System.Drawing.Size(144, 21);
            this.btDelete.TabIndex = 1;
            this.btDelete.Text = "删除 >";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // btClear
            // 
            this.btClear.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btClear.Location = new System.Drawing.Point(3, 120);
            this.btClear.Name = "btClear";
            this.btClear.Size = new System.Drawing.Size(144, 21);
            this.btClear.TabIndex = 2;
            this.btClear.Text = "全部删除 >>";
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btClarify
            // 
            this.btClarify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btClarify.Location = new System.Drawing.Point(3, 168);
            this.btClarify.Name = "btClarify";
            this.btClarify.Size = new System.Drawing.Size(144, 21);
            this.btClarify.TabIndex = 3;
            this.btClarify.Text = "> 自动整列 <";
            this.btClarify.UseVisualStyleBackColor = true;
            this.btClarify.Click += new System.EventHandler(this.btClarify_Click);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 2);
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(471, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 71);
            this.panel1.TabIndex = 10;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.55639F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.69173F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.55639F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.94737F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel2.Controls.Add(this.cbFlash, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbUndead, 2, 2);
            this.tableLayoutPanel2.Controls.Add(this.cbType, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.nudX, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.nudY, 3, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(309, 71);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "种类";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(3, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "X";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(153, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Y";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cbFlash
            // 
            this.cbFlash.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.cbFlash, 2);
            this.cbFlash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbFlash.Location = new System.Drawing.Point(3, 49);
            this.cbFlash.Name = "cbFlash";
            this.cbFlash.Size = new System.Drawing.Size(144, 19);
            this.cbFlash.TabIndex = 6;
            this.cbFlash.Text = "中途出现";
            this.cbFlash.UseVisualStyleBackColor = true;
            this.cbFlash.CheckedChanged += new System.EventHandler(this.cbFlash_CheckedChanged);
            // 
            // cbUndead
            // 
            this.cbUndead.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.cbUndead, 2);
            this.cbUndead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbUndead.Location = new System.Drawing.Point(153, 49);
            this.cbUndead.Name = "cbUndead";
            this.cbUndead.Size = new System.Drawing.Size(153, 19);
            this.cbUndead.TabIndex = 7;
            this.cbUndead.Text = "不死之身";
            this.cbUndead.UseVisualStyleBackColor = true;
            this.cbUndead.CheckedChanged += new System.EventHandler(this.cbUndead_CheckedChanged);
            // 
            // cbType
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.cbType, 3);
            this.cbType.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbType.FormattingEnabled = true;
            this.cbType.ItemHeight = 12;
            this.cbType.Location = new System.Drawing.Point(72, 3);
            this.cbType.Name = "cbType";
            this.cbType.Size = new System.Drawing.Size(234, 18);
            this.cbType.TabIndex = 8;
            this.cbType.SelectedIndexChanged += new System.EventHandler(this.cbType_SelectedIndexChanged);
            this.cbType.Enter += new System.EventHandler(this.cbType_Enter);
            // 
            // nudX
            // 
            this.nudX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudX.Location = new System.Drawing.Point(72, 26);
            this.nudX.Maximum = new decimal(new int[] {
            640,
            0,
            0,
            0});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(75, 21);
            this.nudX.TabIndex = 9;
            // 
            // nudY
            // 
            this.nudY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nudY.Location = new System.Drawing.Point(222, 26);
            this.nudY.Maximum = new decimal(new int[] {
            480,
            0,
            0,
            0});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(84, 21);
            this.nudY.TabIndex = 10;
            // 
            // ProtoTroopMember
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ProtoTroopMember";
            this.Size = new System.Drawing.Size(783, 324);
            this.Leave += new System.EventHandler(this.ProtoTroopMember_Leave);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btAuto;
        private System.Windows.Forms.Button btBackground;
        private System.Windows.Forms.Button btBattleTest;
        private ProtoTroopBitmap troopMain;
        private ProtoListBox lbList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Button btClear;
        private System.Windows.Forms.Button btClarify;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbFlash;
        private System.Windows.Forms.CheckBox cbUndead;
        private ProtoComboBox cbType;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
    }
}
