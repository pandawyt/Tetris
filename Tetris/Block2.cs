using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Block2 : BaseBlock
    {
        public Block2()
        {
            row = 4;
            column = 4;
            square1 = new int[4, 4] {
                                    {0, 0, 1, 0 },
                                    {0, 0, 1, 0 },
                                    {0, 0, 1, 0 },
                                    {0, 0, 1, 0 }
                                    };
            square2 = new int[4, 4] {
                                    {0, 0, 0, 0 },
                                    {1, 1, 1, 1 },
                                    {0, 0, 0, 0 },
                                    {0, 0, 0, 0 },
                                    };
            square = square1;
            spin_point = new Point(1, 2);
            move_point = new Point(5, 0);
            type = 1;
        }

        public override bool CanTransform(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;
            int col_index = move_point.X + 2;
            switch (type)
            {
                case 1:
                    if (col_index == 0 || col_index == 1) return false;
                    if (col_index == COLUMN - 1) return false;
                    if (CANVAS[row_index, col_index - 2] == 1 || CANVAS[row_index, col_index - 1] == 1 || CANVAS[row_index, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (row_index == ROW - 1 || row_index == ROW - 2) return false;
                    if (CANVAS[row_index - 1, col_index] == 1 || CANVAS[row_index + 1, col_index] == 1 || CANVAS[row_index + 2, col_index] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }
        public override int[,] Transform()
        {
            switch (type)
            {
                case 1:
                    type = 2;
                    return square2;
                case 2:
                    type = 1;
                    return square1;
            }
            return square;
        }
        public override bool CanMoveDown(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;
            int col_index = move_point.X + 2;
            switch (type)
            {
                case 1:
                    // 旋转点所在长条，下面一行1个点为0，则可以下移
                    // 1. 下面没有行了
                    if (row_index + 2 == ROW - 1)
                    {
                        return false;
                    }
                    // 2. 下面还有行
                    if (CANVAS[row_index + 3, col_index] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    // 旋转点所在长条，下面一行四个点都为0，则可以下移
                    // 1. 下面没有行了
                    if (row_index == ROW - 1)
                    {
                        return false;
                    }
                    // 2. 下面还有行
                    if (CANVAS[row_index + 1, col_index - 2] == 1 || CANVAS[row_index + 1, col_index - 1] == 1
                        || CANVAS[row_index + 1, col_index] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }
        public override bool CanMoveLeft(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;           // 旋转点y坐标
            int col_index = move_point.X + 2;           // 旋转点x坐标
            switch (type)
            {
                case 1:
                    // 判断左边是边界，或者有1
                    // 1. 边界
                    if(col_index == 0)
                    {
                        return false;
                    }
                    // 2. 左边不是边界，但是有1
                    if(CANVAS[row_index - 1, col_index - 1] == 1 || CANVAS[row_index, col_index - 1] == 1 || 
                        CANVAS[row_index + 1, col_index - 1] == 1 || CANVAS[row_index + 2, col_index - 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    // 1. 边界
                    if(col_index - 2 == 0)
                    {
                        return false;
                    }
                    // 1. 左边不是边界，但是有1
                    if(CANVAS[row_index, col_index - 3] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }
        public override bool CanMoveRight(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;           // 旋转点y坐标
            int col_index = move_point.X + 2;           // 旋转点x坐标
            switch (type)
            {
                case 1:
                    // 判断右边是边界，或者有1
                    // 1. 边界
                    if (col_index == COLUMN - 1)
                    {
                        return false;
                    }
                    // 2. 右边不是边界，但是有1
                    if (CANVAS[row_index - 1, col_index + 1] == 1 || CANVAS[row_index, col_index + 1] == 1 ||
                        CANVAS[row_index + 1, col_index + 1] == 1 || CANVAS[row_index + 2, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    // 1. 边界
                    if (col_index + 1 == COLUMN - 1)
                    {
                        return false;
                    }
                    // 1. 右边不是边界，但是有1
                    if (CANVAS[row_index, col_index + 2] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }

    }
}
