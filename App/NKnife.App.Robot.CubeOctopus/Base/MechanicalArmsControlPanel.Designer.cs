namespace NKnife.Tools.Robot.CubeOctopus.Base
{
    partial class MechanicalArmsControlPanel
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
            this._InitPositionButton = new System.Windows.Forms.Button();
            this._HandIndexUpDown = new System.Windows.Forms.NumericUpDown();
            this._WristIndexUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this._InitHandExpendPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this._Wrist180PositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this._WristCCW90PositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this._WristCW90PositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._InitHandClosedPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this._InitWristPositionUpDown = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._SavePositionButton = new System.Windows.Forms.Button();
            this._180PositionButton = new System.Windows.Forms.Button();
            this._CCW90PositionButton = new System.Windows.Forms.Button();
            this._CW90PositionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._HandIndexUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._WristIndexUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._InitHandExpendPositionUpDown)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._Wrist180PositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._WristCCW90PositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._WristCW90PositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._InitHandClosedPositionUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._InitWristPositionUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _InitPositionButton
            // 
            this._InitPositionButton.Location = new System.Drawing.Point(443, 323);
            this._InitPositionButton.Name = "_InitPositionButton";
            this._InitPositionButton.Size = new System.Drawing.Size(80, 39);
            this._InitPositionButton.TabIndex = 0;
            this._InitPositionButton.Text = "测试";
            this._InitPositionButton.UseVisualStyleBackColor = true;
            this._InitPositionButton.Click += new System.EventHandler(this.InitPositionButtonClick);
            // 
            // _HandIndexUpDown
            // 
            this._HandIndexUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._HandIndexUpDown.Location = new System.Drawing.Point(141, 24);
            this._HandIndexUpDown.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this._HandIndexUpDown.Name = "_HandIndexUpDown";
            this._HandIndexUpDown.Size = new System.Drawing.Size(67, 25);
            this._HandIndexUpDown.TabIndex = 1;
            this._HandIndexUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._HandIndexUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _WristIndexUpDown
            // 
            this._WristIndexUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._WristIndexUpDown.Location = new System.Drawing.Point(141, 53);
            this._WristIndexUpDown.Name = "_WristIndexUpDown";
            this._WristIndexUpDown.Size = new System.Drawing.Size(67, 25);
            this._WristIndexUpDown.TabIndex = 2;
            this._WristIndexUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._WristIndexUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "机械手舵机编号:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "腕部舵机编号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "腕部初始:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(40, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "手部张开:";
            // 
            // _InitHandExpendPositionUpDown
            // 
            this._InitHandExpendPositionUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._InitHandExpendPositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._InitHandExpendPositionUpDown.Location = new System.Drawing.Point(99, 24);
            this._InitHandExpendPositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._InitHandExpendPositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._InitHandExpendPositionUpDown.Name = "_InitHandExpendPositionUpDown";
            this._InitHandExpendPositionUpDown.Size = new System.Drawing.Size(67, 25);
            this._InitHandExpendPositionUpDown.TabIndex = 5;
            this._InitHandExpendPositionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._InitHandExpendPositionUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this._Wrist180PositionUpDown);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this._WristCCW90PositionUpDown);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this._WristCW90PositionUpDown);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._InitHandClosedPositionUpDown);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this._InitWristPositionUpDown);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this._InitHandExpendPositionUpDown);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(20, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 182);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "位置参数";
            // 
            // _Wrist180PositionUpDown
            // 
            this._Wrist180PositionUpDown.Enabled = false;
            this._Wrist180PositionUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._Wrist180PositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._Wrist180PositionUpDown.Location = new System.Drawing.Point(363, 99);
            this._Wrist180PositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._Wrist180PositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._Wrist180PositionUpDown.Name = "_Wrist180PositionUpDown";
            this._Wrist180PositionUpDown.Size = new System.Drawing.Size(67, 25);
            this._Wrist180PositionUpDown.TabIndex = 19;
            this._Wrist180PositionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._Wrist180PositionUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(323, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "180度:";
            // 
            // _WristCCW90PositionUpDown
            // 
            this._WristCCW90PositionUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._WristCCW90PositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._WristCCW90PositionUpDown.Location = new System.Drawing.Point(250, 99);
            this._WristCCW90PositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._WristCCW90PositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._WristCCW90PositionUpDown.Name = "_WristCCW90PositionUpDown";
            this._WristCCW90PositionUpDown.Size = new System.Drawing.Size(67, 25);
            this._WristCCW90PositionUpDown.TabIndex = 17;
            this._WristCCW90PositionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._WristCCW90PositionUpDown.Value = new decimal(new int[] {
            630,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(181, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "逆时针90度:";
            // 
            // _WristCW90PositionUpDown
            // 
            this._WristCW90PositionUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._WristCW90PositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._WristCW90PositionUpDown.Location = new System.Drawing.Point(108, 99);
            this._WristCW90PositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._WristCW90PositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._WristCW90PositionUpDown.Name = "_WristCW90PositionUpDown";
            this._WristCW90PositionUpDown.Size = new System.Drawing.Size(67, 25);
            this._WristCW90PositionUpDown.TabIndex = 15;
            this._WristCW90PositionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._WristCW90PositionUpDown.Value = new decimal(new int[] {
            2250,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "顺时针90度:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.ControlDark;
            this.label5.Location = new System.Drawing.Point(40, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(420, 1);
            this.label5.TabIndex = 14;
            // 
            // _InitHandClosedPositionUpDown
            // 
            this._InitHandClosedPositionUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._InitHandClosedPositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._InitHandClosedPositionUpDown.Location = new System.Drawing.Point(231, 24);
            this._InitHandClosedPositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._InitHandClosedPositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._InitHandClosedPositionUpDown.Name = "_InitHandClosedPositionUpDown";
            this._InitHandClosedPositionUpDown.Size = new System.Drawing.Size(67, 25);
            this._InitHandClosedPositionUpDown.TabIndex = 12;
            this._InitHandClosedPositionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._InitHandClosedPositionUpDown.Value = new decimal(new int[] {
            1300,
            0,
            0,
            0});
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(172, 30);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "手部合拢:";
            // 
            // _InitWristPositionUpDown
            // 
            this._InitWristPositionUpDown.Font = new System.Drawing.Font("Verdana", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._InitWristPositionUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this._InitWristPositionUpDown.Location = new System.Drawing.Point(99, 52);
            this._InitWristPositionUpDown.Maximum = new decimal(new int[] {
            2500,
            0,
            0,
            0});
            this._InitWristPositionUpDown.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this._InitWristPositionUpDown.Name = "_InitWristPositionUpDown";
            this._InitWristPositionUpDown.Size = new System.Drawing.Size(67, 25);
            this._InitWristPositionUpDown.TabIndex = 9;
            this._InitWristPositionUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this._InitWristPositionUpDown.Value = new decimal(new int[] {
            1450,
            0,
            0,
            0});
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this._HandIndexUpDown);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this._WristIndexUpDown);
            this.groupBox2.Location = new System.Drawing.Point(20, 28);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(503, 101);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "舵机编号";
            // 
            // _SavePositionButton
            // 
            this._SavePositionButton.Location = new System.Drawing.Point(20, 323);
            this._SavePositionButton.Name = "_SavePositionButton";
            this._SavePositionButton.Size = new System.Drawing.Size(80, 39);
            this._SavePositionButton.TabIndex = 11;
            this._SavePositionButton.Text = "保存参数";
            this._SavePositionButton.UseVisualStyleBackColor = true;
            this._SavePositionButton.Click += new System.EventHandler(this.SavePositionButtonClick);
            // 
            // _180PositionButton
            // 
            this._180PositionButton.Location = new System.Drawing.Point(345, 323);
            this._180PositionButton.Name = "_180PositionButton";
            this._180PositionButton.Size = new System.Drawing.Size(80, 39);
            this._180PositionButton.TabIndex = 12;
            this._180PositionButton.Text = "180度";
            this._180PositionButton.UseVisualStyleBackColor = true;
            this._180PositionButton.Click += new System.EventHandler(this.Position180ButtonClick);
            // 
            // _CCW90PositionButton
            // 
            this._CCW90PositionButton.Location = new System.Drawing.Point(259, 323);
            this._CCW90PositionButton.Name = "_CCW90PositionButton";
            this._CCW90PositionButton.Size = new System.Drawing.Size(80, 39);
            this._CCW90PositionButton.TabIndex = 13;
            this._CCW90PositionButton.Text = "逆时针90度";
            this._CCW90PositionButton.UseVisualStyleBackColor = true;
            this._CCW90PositionButton.Click += new System.EventHandler(this.CCW90PositionButtonClick);
            // 
            // _CW90PositionButton
            // 
            this._CW90PositionButton.Location = new System.Drawing.Point(171, 323);
            this._CW90PositionButton.Name = "_CW90PositionButton";
            this._CW90PositionButton.Size = new System.Drawing.Size(80, 39);
            this._CW90PositionButton.TabIndex = 14;
            this._CW90PositionButton.Text = "顺时针90度";
            this._CW90PositionButton.UseVisualStyleBackColor = true;
            this._CW90PositionButton.Click += new System.EventHandler(this.CW90PositionButtonClick);
            // 
            // MechanicalArmsControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._CW90PositionButton);
            this.Controls.Add(this._CCW90PositionButton);
            this.Controls.Add(this._180PositionButton);
            this.Controls.Add(this._SavePositionButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this._InitPositionButton);
            this.Name = "MechanicalArmsControlPanel";
            this.Size = new System.Drawing.Size(545, 419);
            ((System.ComponentModel.ISupportInitialize)(this._HandIndexUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._WristIndexUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._InitHandExpendPositionUpDown)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._Wrist180PositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._WristCCW90PositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._WristCW90PositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._InitHandClosedPositionUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._InitWristPositionUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _InitPositionButton;
        private System.Windows.Forms.NumericUpDown _HandIndexUpDown;
        private System.Windows.Forms.NumericUpDown _WristIndexUpDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown _InitHandExpendPositionUpDown;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown _InitWristPositionUpDown;
        private System.Windows.Forms.NumericUpDown _InitHandClosedPositionUpDown;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown _Wrist180PositionUpDown;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown _WristCCW90PositionUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown _WristCW90PositionUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button _SavePositionButton;
        private System.Windows.Forms.Button _180PositionButton;
        private System.Windows.Forms.Button _CCW90PositionButton;
        private System.Windows.Forms.Button _CW90PositionButton;
    }
}
