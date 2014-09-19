using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using NKnife.Utility.Maths;

namespace Gean.Client.EinsteinPuzzle
{
    class MainForm : Form
    {
        public List<IntHouse> ResultHouses { get; set; }

        public MainForm()
        {
            InitializeComponent();
            this.ResultHouses = new List<IntHouse>();

            _totalLabel.Text = "";
            _stepLabel.Text = "";
            _countLabel.Text = "";

            _timer = new System.Timers.Timer(1000);
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.SetStepLabel(_step);
        }

        #region 自动生成的代码

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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._beginButton = new System.Windows.Forms.Button();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this._totalLabel = new System.Windows.Forms.Label();
            this._stepLabel = new System.Windows.Forms.Label();
            this._countLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _beginButton
            // 
            this._beginButton.BackColor = System.Drawing.Color.LightGreen;
            this._beginButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this._beginButton.Location = new System.Drawing.Point(500, 230);
            this._beginButton.Name = "_beginButton";
            this._beginButton.Size = new System.Drawing.Size(101, 34);
            this._beginButton.TabIndex = 0;
            this._beginButton.Text = "开始运算";
            this._beginButton.UseVisualStyleBackColor = false;
            this._beginButton.Click += new System.EventHandler(this.BeginButton_Click);
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 5;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._tableLayoutPanel.Location = new System.Drawing.Point(20, 20);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 1;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(581, 81);
            this._tableLayoutPanel.TabIndex = 1;
            // 
            // _totalLabel
            // 
            this._totalLabel.AutoSize = true;
            this._totalLabel.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this._totalLabel.Location = new System.Drawing.Point(349, 230);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(48, 17);
            this._totalLabel.TabIndex = 2;
            this._totalLabel.Text = "Total";
            // 
            // _stepLabel
            // 
            this._stepLabel.AutoSize = true;
            this._stepLabel.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this._stepLabel.Location = new System.Drawing.Point(349, 247);
            this._stepLabel.Name = "_stepLabel";
            this._stepLabel.Size = new System.Drawing.Size(40, 17);
            this._stepLabel.TabIndex = 3;
            this._stepLabel.Text = "Step";
            // 
            // _countLabel
            // 
            this._countLabel.AutoSize = true;
            this._countLabel.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this._countLabel.Location = new System.Drawing.Point(497, 210);
            this._countLabel.Name = "_countLabel";
            this._countLabel.Size = new System.Drawing.Size(48, 17);
            this._countLabel.TabIndex = 4;
            this._countLabel.Text = "Count";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(621, 287);
            this.Controls.Add(this._countLabel);
            this.Controls.Add(this._stepLabel);
            this.Controls.Add(this._totalLabel);
            this.Controls.Add(this._tableLayoutPanel);
            this.Controls.Add(this._beginButton);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Einstein Puzzle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        #endregion

        private Label _totalLabel;
        private Label _stepLabel;
        private Label _countLabel;
        private Button _beginButton;
        private TableLayoutPanel _tableLayoutPanel;

        private List<int[]> _Colors = new List<int[]>();
        private List<int[]> _Nationals = new List<int[]>();
        private List<int[]> _Pets = new List<int[]>();
        private List<int[]> _Cigarettes = new List<int[]>();
        private List<int[]> _Drinks = new List<int[]>();
        private Dictionary<long, IntHouse> _intHouses = new Dictionary<long, IntHouse>();
        private long _step = 0;
        private System.Timers.Timer _timer;

        void BeginButton_Click(object sender, EventArgs e)
        {
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            Thread t = new Thread(new ThreadStart(DoWorker));
            //t.IsBackground = true;
            t.Start();
            _timer.Start();
        }

        void SetTotalLabel(long value)
        {
            if (_totalLabel.InvokeRequired)
            {
                ControlInvoke _invoke = new ControlInvoke(SetTotalLabel);
                this.BeginInvoke(_invoke, new object[] { value });
            }
            else
            {
                _totalLabel.Text = value.ToString();
            }
        }
        void SetStepLabel(long value)
        {
            if (_stepLabel.InvokeRequired)
            {
                ControlInvoke _invoke = new ControlInvoke(SetStepLabel);
                this.BeginInvoke(_invoke, new object[] { value });
            }
            else
            {
                _stepLabel.Text = value.ToString();
            }

        }
        void SetCountLabel(long value)
        {
            if (_countLabel.InvokeRequired)
            {
                ControlInvoke _invoke = new ControlInvoke(SetCountLabel);
                this.BeginInvoke(_invoke, new object[] { value });
            }
            else
            {
                _countLabel.Text = value.ToString();
            }

        }
        delegate void ControlInvoke(long value);

        void DoWorker()
        {
            DoArrayBuilder();
            DoIntHouseBuilder();
        }

        void DoArrayBuilder()
        {
            Permutations<int> intPer = new Permutations<int>(new int[] { 0, 1, 2, 3, 4 }, 10);
            //生成颜色数组
            foreach (IList<int> item in intPer)
            {
                if (ConditionService.Verify14(item))
                {
                    int[] ts = new int[5];
                    item.CopyTo(ts, 0);
                    _Colors.Add(ts);
                }
            }
            //生成国家数组
            foreach (IList<int> item in intPer)
            {
                if (ConditionService.Verify08(item))
                {
                    int[] ts = new int[5];
                    item.CopyTo(ts, 0);
                    _Nationals.Add(ts);
                }
            }
            //生成饮料数组
            foreach (IList<int> item in intPer)
            {
                if (ConditionService.Verify09(item))
                {
                    int[] ts = new int[5];
                    item.CopyTo(ts, 0);
                    _Drinks.Add(ts);
                }
            }
            //生成宠物数组
            foreach (IList<int> item in intPer)
            {
                int[] ts = new int[5];
                item.CopyTo(ts, 0);
                _Pets.Add(ts);
            }
            //生成香烟数组
            foreach (IList<int> item in intPer)
            {
                int[] ts = new int[5];
                item.CopyTo(ts, 0);
                _Cigarettes.Add(ts);
            }
            this.SetTotalLabel(_Colors.Count * _Nationals.Count * _Drinks.Count * _Pets.Count * _Cigarettes.Count);
            //_count = _Colors.Count + _Nationals.Count + _Drinks.Count + _Pets.Count + _Cigarettes.Count;
        }

        void DoIntHouseBuilder()
        {
            _step = 0;
            foreach (int[] color in _Colors)
            {
                foreach (int[] national in _Nationals)
                {
                    foreach (int[] pet in _Pets)
                    {
                        foreach (int[] cigarette in _Cigarettes)
                        {
                            foreach (int[] drink in _Drinks)
                            {
                                IntHouse house = new IntHouse(color, national, pet, cigarette, drink);
                                if (house.Verify())
                                {
                                    this.ResultHouses.Add(house);
                                    this.SetCountLabel(this.ResultHouses.Count);
                                }
                                _step++;
                            }
                        }
                    }
                }
            }
            StringBuilder sb = new StringBuilder();
            if (this.ResultHouses.Count > 0)
            {
                foreach (var item in this.ResultHouses)
                {
                    sb.AppendLine(item.ToString());
                    sb.AppendLine();
                }
            }
            else
                sb.AppendLine("Nothing......");
            MessageBox.Show(sb.ToString());
        }
    }

    struct IntHouse
    {
        private List<int[]> _innerIntArrays;

        public IntHouse(int[] color, int[] national, int[] pet, int[] cigarette, int[] drink)
        {
            _innerIntArrays = new List<int[]>(5);
            _innerIntArrays.Add(color);
            _innerIntArrays.Add(national);
            _innerIntArrays.Add(pet);
            _innerIntArrays.Add(cigarette);
            _innerIntArrays.Add(drink);
        }

        public int[] this[Enums.HouseType type]
        {
            get
            {
                return _innerIntArrays[(int)type];
            }
        }

        public int this[int x, Enums.HouseType y] 
        {
            get
            {
                return _innerIntArrays[(int)y][x];
            }
        }

        public bool Verify()
        {
            if (!ConditionService.VerifySmoker(this)) return false;
            if (!ConditionService.VerifyNational(this)) return false;
            if (!ConditionService.VerifyColor(this)) return false;

            return true;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in _innerIntArrays)
            {
                foreach (var i in item)
                {
                    sb.Append(i.ToString()).Append(',');
                }
                sb.Append("\r\n");
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// !1.	英国人住红色房子里。The Englishman lives in the red house.
    /// !2.	瑞典人养狗。The Swede keeps dogs.
    /// !3.	丹麦人喝茶。The Dane drinks tea.
    /// @4.	绿色房子坐落在白色房子的左面。The green house is just to the left of the white one.
    /// @5.	绿色房子的主人喝咖啡。The owner of the green house drinks coffee.
    /// #6.	抽Pall Mall香烟的人养鸟。The Pall Mall smoker keeps birds.
    /// #7.	黄色房子的主人抽Dunhill香烟。The owner of the yellow house smokes Dunhills.
    /// !8.	挪威人住第一间房子。The man in the center house drinks milk.
    /// !9.	五座房子中间的那座的主人喝牛奶。The Norwegian lives in the first house.
    /// #10.	抽Blends香烟的住在养猫人的隔壁。The Blend smoker has a neighbor who keeps cats.
    /// #11.	养马的人住在抽Dunhill香烟者的隔壁。The man who smokes Blue Masters drinks bier.
    /// #12.	抽Blue Master香烟的喝啤酒。The man who keeps horses lives next to the Dunhill smoker.
    /// !13.	德国人抽Prince香烟。The German smokes Prince.
    /// !14.	挪威人住的房子在蓝色房子的隔壁。The Norwegian lives next to the blue house.
    /// #15.	抽Blends香烟的人有一个喝水的邻居。The Blend smoker has a neighbor who drinks water.
    /// </summary>
    class ConditionService
    {
        /// <summary>
        /// Verifies the smoker.
        /// 6.	抽Pall Mall香烟的人养鸟。The Pall Mall smoker keeps birds.
        /// 10.	抽Blends香烟的住在养猫人的隔壁。The Blend smoker has a neighbor who keeps cats.
        /// 15.	抽Blends香烟的人有一个喝水的邻居。The Blend smoker has a neighbor who drinks water.
        /// 12.	抽Blue Master香烟的喝啤酒。The man who keeps horses lives next to the Dunhill smoker.
        /// 11.	抽Dunhill香烟者的隔壁养马。The man who smokes Blue Masters drinks bier.
        /// 7.	抽Dunhill香烟住黄色房子。The owner of the yellow house smokes Dunhills.
        /// 13.	德国人抽Prince香烟。The German smokes Prince.
        /// </summary>
        public static bool VerifySmoker(IntHouse house)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (house[i, Enums.HouseType.Cigarette])
                {
                    case 1://抽Blends香烟的住在养猫人的隔壁。
                        if (!((i - 1 >= 0 && house[i - 1, Enums.HouseType.Pet] == 2) ||
                            ((i + 1 < 5) && (house[i + 1, Enums.HouseType.Pet] == 2))))
                        {
                            return false;
                        } //抽Blends香烟的人有一个喝水的邻居。
                        if (!((i - 1 >= 0 && house[i - 1, Enums.HouseType.Drink] == 4) ||
                            ((i + 1 < 5) && (house[i + 1, Enums.HouseType.Drink] == 4))))
                        {
                            return false;
                        }
                        break;
                    case 2://BlueMaster 抽Blue Master香烟的喝啤酒
                        if (house[i, Enums.HouseType.Drink] != 3)
                        {
                            return false;
                        }
                        break;
                    case 3://Dunhill 隔壁养马 住黄色房子
                        if (house[i, Enums.HouseType.Color] != 3)
                        {
                            return false;
                        }
                        if (!((i - 1 >= 0 && house[i - 1, Enums.HouseType.Pet] == 3) ||
                            ((i + 1 < 5) && (house[i + 1, Enums.HouseType.Pet] == 3))))
                        {
                            return false;
                        }
                        break;
                    case 4://PallMall 养鸟
                        if (house[i, Enums.HouseType.Pet] != 1)
                        {
                            return false;
                        }
                        break;
                    case 5://Prince 德国人
                        if (house[i, Enums.HouseType.National] != 4)
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifies the national.
        /// 1.	英国人住红色房子里。The Englishman lives in the red house.
        /// 2.	瑞典人养狗。The Swede keeps dogs.
        /// 3.	丹麦人喝茶。The Dane drinks tea.
        /// </summary>
        /// <param name="house">The house.</param>
        /// <returns></returns>
        public static bool VerifyNational(IntHouse house)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (house[i, Enums.HouseType.National])
                {
                    case 1://Dane 喝茶
                        if (house[i, Enums.HouseType.Drink] != 0)
                        {
                            return false;
                        }
                        break;
                    case 2://English 红色房子
                        if (house[i, Enums.HouseType.Color] != 0)
                        {
                            return false;
                        }
                        break;
                    case 3://German
                    case 4://Norwegian
                        break;
                    case 5://Swede 养狗
                        if (house[i, Enums.HouseType.Drink] != 0)
                        {
                            return false;
                        }
                        break;
                }
            }
            return true;
        }

        /// <summary>
        /// Verifies the color.
        /// 4.	绿色房子坐落在白色房子的左面。The green house is just to the left of the white one.
        /// 5.	绿色房子的主人喝咖啡。The owner of the green house drinks coffee.
        /// </summary>
        /// <param name="house">The house.</param>
        /// <returns></returns>
        public static bool VerifyColor(IntHouse house)
        {
            for (int i = 0; i < 5; i++)
            {
                switch (house[i, Enums.HouseType.National])
                {
                    case 1://Blue 
                        break;
                    case 2://Green 绿色房子坐落在白色房子的左面
                        if (house[i, Enums.HouseType.Drink] != 1) //喝咖啡
                        {
                            return false;
                        }
                        for (int j = i; j >= 0; j--)
                        {
                            if (house[j, Enums.HouseType.Color] == 1)
                            {
                                break;
                            }
                        }
                        return false;
                    case 3://Red
                    case 4://White
                    case 5://Yellow
                        break;
                }
            }
            return true;
        }

        #region 单条件8，9，14, 在数组生成前过滤
        /// <summary>
        /// 8.	挪威人住第一间房子。
        /// </summary>
        /// <param name="ints">The ints.</param>
        /// <returns></returns>
        public static bool Verify08(IList<int> ints)
        {
            if (ints[0] == 3 || ints[4] == 3)//挪威是3
                return true;
            return false;
        }

        /// <summary>
        /// 9.	五座房子中间的那座的主人喝牛奶。
        /// </summary>
        /// <param name="ints">The ints.</param>
        /// <returns></returns>
        public static bool Verify09(IList<int> ints)
        {
            if (ints[2] == 2)//2是牛奶
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 14.	挪威人住的房子在蓝色房子的隔壁。
        /// 因为第8条，挪威人住在第1间，故蓝色房子一定是两侧数起的第2间
        /// </summary>
        /// <param name="ints">The ints.</param>
        /// <returns></returns>
        public static bool Verify14(IList<int> ints)
        {
            if (ints[1] == 4 || ints[3] == 4)//4是蓝色
            {
                return true;
            }
            return false;
        }
        #endregion

    }

    class Enums
    {
        public enum HouseType : int
        {
            Color = 0, National = 1, Pet = 2, Cigarette = 3, Drink = 4, None = 9
        }
        public enum Color : int
        {
            Red = 0, White = 1, Green = 2, Yellow = 3, Blue = 4, None = 9
        }
        public enum National : int
        {
            English = 0, Swede = 1, Dane = 2, Norwegian = 3, German = 4, None = 9
        }
        public enum Pet : int
        {
            Dog = 0, Bird = 1, Cat = 2, Horse = 3, Fish = 4, None = 9
        }
        public enum Cigarette : int
        {
            PallMall = 0, Dunhill = 1, Blends = 2, BlueMaster = 3, Prince = 4, None = 9
        }
        public enum Drink : int
        {
            Tea = 0, Coffee = 1, Milk = 2, Bier = 3, Water = 4, None = 9
        }
    }

}


