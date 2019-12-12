using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public abstract class BaseBlock
    {
        public int row;                             // block占有的正方形区域大小
        public int column;
        public int[,] square;                        // block占有的正方形区域
        public int[,] square1;                       // block占有的正方形区域 类型1
        public int[,] square2;                       // block占有的正方形区域 类型2
        public int[,] square3;                       // block占有的正方形区域 类型3
        public int[,] square4;                       // block占有的正方形区域 类型4
        public Point spin_point;                    // 旋转点，相对于正方形区域
        public Point move_point;                    // 移动点，相对于画布，默认是正方形区域的左上角坐标
        public int type = 1;

        public void infor(string s)
        {
            MessageBox.Show(s, "asdsa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public abstract bool CanTransform(int[,] CANVAS, int ROW, int COLUMN);          // 能否变形
        public abstract int[,] Transform();                                           // 变形
        public abstract bool CanMoveDown(int[,] CANVAS, int ROW, int COLUMN);          // 能否下移
        public void MoveDown()
        {
            move_point.Y += 1;
        }
        public abstract bool CanMoveLeft(int[,] CANVAS, int ROW, int COLUMN);          // 能否左移
        public void MoveLeft()
        {
            move_point.X -= 1;
        }
        public abstract bool CanMoveRight(int[,] CANVAS, int ROW, int COLUMN);         // 能否右移
        public void MoveRight()
        {
            move_point.X += 1;
        }
}
}
