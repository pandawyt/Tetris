using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        private int L       ;                                       // 一个方格宽度
        private static int ROW = 20;                                // 行数
        private static int COLUMN = 15;                             // 列数
        private int[,] CANVAS = new int[ROW, COLUMN];               // 0表示没有block在这个方格，1表示有
        private Bitmap bm;
        private Graphics g;

        private System.Timers.Timer t_fall;
        private double INTERVAL = 500;
        private BaseBlock block;
        private BaseBlock pre_block;                        // 记录上一次画的block，下一次画的时候擦除掉
        private bool completed = false;                     // 一个block是否下落到达最终位置
        private int score = 1;                              // 得分
        private bool has_started = false;                   // 游戏是否已经开始

        private static int BM_HEIGHT = Screen.PrimaryScreen.WorkingArea.Height - 300;
        private static int BM_WIDTH = (BM_HEIGHT / ROW) * COLUMN;

        private delegate void Change();

        public Form1()
        {
            InitializeComponent();
            InitializeUI();
            this.KeyPreview = true;
            this.Text = "我的俄罗斯方块儿";

            button_start.Location = new Point(BM_WIDTH + 60, 8 * BM_HEIGHT / 10);
            label_score.Location = new Point(BM_WIDTH + 120, 1 * BM_HEIGHT / 10 - 5);
            label1.Location = new Point(BM_WIDTH + 50, 1 * BM_HEIGHT / 10);
            label2.Location = new Point(BM_WIDTH + 50, 4 * BM_HEIGHT / 10);
            label3.Location = new Point(BM_WIDTH + 50, 5 * BM_HEIGHT / 10);
            label4.Location = new Point(BM_WIDTH + 50, 6 * BM_HEIGHT / 10);
            label5.Location = new Point(BM_WIDTH + 50, 7 * BM_HEIGHT / 10);
            label6.Location = new Point(BM_WIDTH + 100, 7 * BM_HEIGHT / 10);
            label7.Location = new Point(BM_WIDTH + 100, 6 * BM_HEIGHT / 10);
            label8.Location = new Point(BM_WIDTH + 100, 5 * BM_HEIGHT / 10);
            label9.Location = new Point(BM_WIDTH + 100, 4 * BM_HEIGHT / 10);
        }
        //
        // 初始化界面
        //
        private void InitializeUI()
        {
            L = BM_HEIGHT / ROW;
            this.ClientSize = new Size(BM_WIDTH + 200, BM_HEIGHT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            bm = new Bitmap(BM_WIDTH, BM_HEIGHT);
            g = Graphics.FromImage(bm);
            pictureBox1.Image = bm;
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    CANVAS[i, j] = 0;
                    Brush brush = new SolidBrush(Color.CornflowerBlue);
                    PaintColor(brush, j, i);
                }
            }
            PaintWord();
        }
        //
        // 填充颜色
        //
        public void PaintColor(Brush brush, int x, int y)
        {
            g.FillRectangle(brush, x * L + 1, y * L + 1, L - 2, L - 2);
        }
        //
        //写字
        //
        public void PaintWord()
        {
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    Brush brush = new SolidBrush(Color.Red);
                    Font font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
                    if (CANVAS[i, j] == 1)
                    {
                        g.DrawString("1", font, brush, j * L + 1, i * L + 1);
                    }
                }
            }
        }
        //
        // 画block, paint=false为擦除，paint=true为画
        //
        public void PaintBlock(BaseBlock block, bool paint)
        {
            int x0 = block.move_point.X;            // block移动点相对于整个画布（block的左上角）的坐标x
            int y0 = block.move_point.Y;            // block移动点相对于整个画布（block的左上角）的坐标y
            for (int i = 0; i <block.square.GetLength(0); i++)
            {
                for(int j = 0; j < block.square.GetLength(1); j++)
                {
                    if(i + y0 >= 0 && i + y0 <= ROW - 1 && j + x0 >= 0 && j + x0 <= COLUMN - 1)
                    {
                        if (!paint && CANVAS[i + y0, j + x0] == 0)      // 擦除
                        {
                            Brush brush = new SolidBrush(Color.CornflowerBlue);
                            PaintColor(brush, x0 + j, y0 + i);
                        }
                        if (paint && block.square[i, j] == 1)       // 画
                        {
                            if (completed)
                            {
                                CANVAS[i + y0, j + x0] = 1;
                            }
                            Brush brush = new SolidBrush(Color.SpringGreen);
                            PaintColor(brush, x0 + j, y0 + i);
                            //Brush brush1 = new SolidBrush(Color.Blue);
                            //PaintColor(brush1, x0 + block.spin_point.Y, y0 + block.spin_point.X);       // 画旋转点
                        }
                    }
                }
            }
            //PaintWord();
            pictureBox1.Refresh();
        }
        //
        // 新建block
        //
        public BaseBlock NewBlock()
        {
            Random a = new Random();
            switch (a.Next(1, 8))
            {
                case 1:
                    return new Block1();
                case 2:
                    return new Block2();
                case 3:
                    return new Block3();
                case 4:
                    return new Block4();
                case 5:
                    return new Block5();
                case 6:
                    return new Block6();
                case 7:
                    return new Block7();
            }
            return new Block1();
        }
        //
        // 点击开始
        //
        private void start_Click(object sender, EventArgs e)
        {
            if(!has_started)            // 点击开始 
            {
                has_started = true;
                button_start.Text = "暂停";
                if(block == null)
                {
                    block = NewBlock();
                    pre_block = block;
                    t_fall = new System.Timers.Timer(INTERVAL);
                    t_fall.Elapsed += new System.Timers.ElapsedEventHandler(theout);
                    t_fall.AutoReset = true;
                }
                t_fall.Enabled = true;
            }
            else           // 点击暂停
            {
                has_started = false;
                t_fall.Enabled = false;
                button_start.Text = "开始";
            }
        }
        public void theout(object sender, System.Timers.ElapsedEventArgs e)
        {
            Change change = new Change(AutoDown);
            Invoke(change);
        }
        public void AutoDown()
        {
            if (block.CanMoveDown(CANVAS, ROW, COLUMN))
            {
                t_fall.Enabled = false;
                pre_block = block;
                PaintBlock(pre_block, false);              // 擦除上次一画的block
                block.MoveDown();
                PaintBlock(block, true);                  // 画新的
                t_fall.Enabled = true;
                if(t_fall.Interval == 1)
                {
                    t_fall.Interval = INTERVAL;
                }
            }
            else
            {
                // 不能下移,说明到达最终位置，赋值1；检查能否消除行；新建block
                completed = true;
                PaintBlock(block, true);    // 赋值1
                completed = false;
                EraseLine();                // 消除行
                block = NewBlock();         // 新建block
                pre_block = block;
                t_fall.Interval = 1;
            }
        }
        //
        // 消除行
        //
        public void EraseLine()
        {
            // 从下往上遍历所有行，一行全为1的，消除，上面所有方格下移
            for(int i = ROW - 1; i >= 0; i--)
            {
                for(int j = 0; j < COLUMN; j++)
                {
                    if (CANVAS[i, j] == 0) break;       // 有0，则换下一行
                    if(j == COLUMN - 1)                 // j == COLUMN - 1，说明这一行全是1
                    {
                        Erase(i);
                        label_score.Text = score.ToString();
                        score++;
                        if(INTERVAL >= 100) // 每得一分，下移速度增加，但最小为100
                        {
                            INTERVAL -= 10;
                            t_fall.Interval = INTERVAL;
                        }
                        i++;                            // 上面一行又平移下来，所以得重新检查这一行
                    }
                }
            }
        }
        //
        // 下移
        //
        public void Erase(int row)
        {
            // 下移
            for (int i = row; i >= 1; i--)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    CANVAS[i, j] = CANVAS[i - 1, j];  
                    CANVAS[i - 1, j] = 0;
                }
            }
            // 重绘
            for (int i = 0; i < ROW; i++)
            {
                for (int j = 0; j < COLUMN; j++)
                {
                    if (CANVAS[i, j] == 0)
                    {
                        Brush brush = new SolidBrush(Color.CornflowerBlue);
                        g.FillRectangle(brush, j * L + 1, i * L + 1, L - 2, L - 2);
                    }
                    if (CANVAS[i, j] == 1)
                    {
                        Brush brush = new SolidBrush(Color.SpringGreen);
                        g.FillRectangle(brush, j * L + 1, i * L + 1, L - 2, L - 2);
                    }
                }
            }
        }
        //
        // 方向键控制
        //
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:           // 旋转
                    if(block.CanTransform(CANVAS, ROW, COLUMN))
                    {
                        ExecuteCmd("Transform");
                    }
                    break;
                case Keys.Left:         // 左移
                    if (block.CanMoveLeft(CANVAS, ROW, COLUMN))
                    {
                        ExecuteCmd("Move Left");
                    }
                    break;
                case Keys.Right:        // 右移
                    if (block.CanMoveRight(CANVAS, ROW, COLUMN))
                    {
                        ExecuteCmd("Move Right");
                    }
                    break;
                case Keys.Down:         // 加速下落
                    if (block.CanMoveDown(CANVAS, ROW, COLUMN))
                    {
                        ExecuteCmd("Move Down");
                    }
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }
        //
        // 执行方向键控制
        //
        public void ExecuteCmd(string cmd)
        {
            pre_block = block;
            PaintBlock(pre_block, false);              // 擦除上次一画的block
            switch (cmd)
            {
                case "Transform":
                    block.square = block.Transform();
                    break;
                case "Move Left":
                    block.MoveLeft();
                    break;
                case "Move Right":
                    block.MoveRight();
                    break;
                case "Move Down":
                    block.MoveDown();
                    break;
            }
            PaintBlock(block, true);                   // 画现在的
        }
    }
}
