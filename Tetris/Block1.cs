using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    class Block1 : BaseBlock
    {
        public Block1()
        {
            row = 2;
            column = 2;
            square1 = new int[2,2]{ 
                                    {1, 1}, 
                                    {1, 1}
                                  };
            square2 = square1;
            square3 = square1;
            square4 = square1;
            square = square1;
            spin_point = new Point(0, 0);
            move_point = new Point(6, 0);
        }

        public override bool CanTransform(int[,] CANVAS, int ROW, int COLUMN)
        {
            return false;
        }

        public override int[,] Transform()
        {
            return square;
        }

        public override bool CanMoveDown(int[,] CANVAS, int ROW, int COLUMN)
        {
            // 判断square值有1的最后一行
            // 1. 下边没有block，直接判断边界
            int row_index = move_point.Y + 1;
            if (row_index == ROW - 1)
            {
                return false;      // 下移的边界,到达底部了
            }
            // 2. 下边有block
            for(int i = 0; i < column; i++)             // 找下一行的1，找到则不能下移
            {
                if (CANVAS[row_index + 1, move_point.X + i] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool CanMoveLeft(int[,] CANVAS, int ROW, int COLUMN)
        {
            // 判断square值有1最左边一列
            // 1. 左边没有其他block，直接判断边界
            int col = move_point.X;
            if(col == 0)
            {
                return false;
            }
            // 2. 左边有block
            for(int i = 0; i < row; i++)
            {
                if (CANVAS[move_point.Y + i, col - 1] == 1)
                {
                    return false;
                }
            }
            return true;
        }

        public override bool CanMoveRight(int[,] CANVAS, int ROW, int COLUMN)
        {
            // 判断square值有1最右边一列
            // 1. 右边没有其他block，直接判断边界
            int col = move_point.X + 1;
            if (col == COLUMN - 1)
            {
                return false;
            }
            // 2. 左边有block
            for (int i = 0; i < row; i++)
            {
                if (CANVAS[move_point.Y + i, col + 1] == 1)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
