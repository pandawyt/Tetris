using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Block3 : BaseBlock
    {
        public Block3()
        {
            row = 3;
            column = 3;
            square1 = new int[3, 3] {
                                    {0, 1, 0 },
                                    {1, 1, 1,},
                                    {0, 0, 0,},
                                    };
            square2 = new int[3, 3] {
                                    {0, 1, 0 },
                                    {0, 1, 1,},
                                    {0, 1, 0,},
                                    };
            square3 = new int[3, 3] {
                                    {0, 0, 0 },
                                    {1, 1, 1,},
                                    {0, 1, 0,},
                                    };
            square4 = new int[3, 3] {
                                    {0, 1, 0 },
                                    {1, 1, 0,},
                                    {0, 1, 0,},
                                    };
            square = square1;
            spin_point = new Point(1, 1);
            move_point = new Point(5, 0);
            type = 1;
        }
        public override bool CanTransform(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;           // 旋转点坐标
            int col_index = move_point.X + 1;           // 旋转点坐标
            switch (type)
            {
                case 1:
                    // 旋转点下面一个点为0，则可以
                    if (row_index == ROW - 1) return false;
                    if (CANVAS[row_index + 1, col_index] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    // 旋转点左边一个点为0，则可以
                    if (col_index == 0) return false;
                    if (CANVAS[row_index, col_index - 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 3:
                    // 旋转点上面一个点为0，则可以
                    if (CANVAS[row_index - 1, col_index] == 1)
                    {
                        return false;
                    }
                    return true;
                case 4:
                    // 旋转点右边一个点为0，则可以
                    if (col_index == COLUMN - 1) return false;
                    if (CANVAS[row_index, col_index + 1] == 1)
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
                    type = 3;
                    return square3;
                case 3:
                    type = 4;
                    return square4;
                case 4:
                    type = 1;
                    return square1;
            }
            return square;
        }
        public override bool CanMoveDown(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;           // 旋转点坐标
            int col_index = move_point.X + 1;           // 旋转点坐标
            switch (type)
            {
                case 1:
                    // 1. 到达边界
                    if (row_index == ROW - 1) return false;
                    // 1. 下面有1
                    if (CANVAS[row_index + 1, col_index - 1] == 1 || CANVAS[row_index + 1, col_index] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    // 1. 到达边界
                    if (row_index + 1 == ROW - 1) return false;
                    // 1. 下面有1
                    if (CANVAS[row_index + 2, col_index] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 3:
                    // 1. 到达边界
                    if (row_index + 1 == ROW - 1) return false;
                    // 1. 下面有1
                    if (CANVAS[row_index + 1, col_index - 1] == 1 || CANVAS[row_index + 2, col_index] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 4:
                    // 1. 到达边界
                    if (row_index + 1 == ROW - 1) return false;
                    // 1. 下面有1
                    if (CANVAS[row_index + 1, col_index - 1] == 1 || CANVAS[row_index + 2, col_index] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }
        public override bool CanMoveLeft(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;           // 旋转点坐标
            int col_index = move_point.X + 1;           // 旋转点坐标
            switch (type)
            {
                case 1:
                    // 1. 边界
                    if (col_index - 1 == 0) return false;
                    // 2. 左边有1
                    if (CANVAS[row_index, col_index - 2] == 1 || CANVAS[row_index - 1, col_index - 1] == 1) return false;
                    return true;
                case 2:
                    if (col_index == 0) return false;
                    if (CANVAS[row_index - 1, col_index - 1] == 1 || CANVAS[row_index, col_index - 1] == 1 || CANVAS[row_index + 1, col_index - 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 3:
                    if (col_index - 1 == 0) return false;
                    if (CANVAS[row_index, col_index - 2] == 1 || CANVAS[row_index + 1, col_index - 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 4:
                    if (col_index - 1 == 0) return false;
                    if (CANVAS[row_index - 1, col_index - 1] == 1 || CANVAS[row_index, col_index - 2] == 1 || CANVAS[row_index + 1, col_index - 1] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }
        public override bool CanMoveRight(int[,] CANVAS, int ROW, int COLUMN)
        {
            int row_index = move_point.Y + 1;           // 旋转点坐标
            int col_index = move_point.X + 1;           // 旋转点坐标
            switch (type)
            {
                case 1:
                    // 1. 边界
                    if (col_index + 1 == COLUMN - 1) return false;
                    // 2. 右边有1
                    if (CANVAS[row_index - 1, col_index + 1] == 1 || CANVAS[row_index, col_index + 2] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (col_index + 1 == COLUMN - 1) return false;
                    if (CANVAS[row_index - 1, col_index + 1] == 1 || CANVAS[row_index, col_index + 2] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 3:
                    if (col_index + 1 == COLUMN - 1) return false;
                    if (CANVAS[row_index, col_index + 2] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 4:
                    if (col_index == COLUMN - 1) return false;
                    if (CANVAS[row_index - 1, col_index + 1] == 1 || CANVAS[row_index, col_index + 1] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }
    }
}