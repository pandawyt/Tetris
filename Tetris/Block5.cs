using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    class Block5 : BaseBlock
    {
        public Block5()
        {
            row = 3;
            column = 3;
            square1 = new int[3, 3] {
                                    {1, 0, 0 },
                                    {1, 1, 0,},
                                    {0, 1, 0,},
                                    };
            square2 = new int[3, 3] {
                                    {0, 0, 0 },
                                    {0, 1, 1,},
                                    {1, 1, 0,},
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
                    if (col_index == COLUMN - 1) return false;
                    if (CANVAS[row_index + 1, col_index - 1] == 1 || CANVAS[row_index, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (CANVAS[row_index, col_index - 1] == 1 || CANVAS[row_index - 1, col_index - 1] == 1)
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
            int row_index = move_point.Y + 1;           // 旋转点坐标
            int col_index = move_point.X + 1;           // 旋转点坐标
            switch (type)
            {
                case 1:
                    if (row_index + 1 == ROW - 1) return false;
                    if (CANVAS[row_index + 1, col_index - 1] == 1 || CANVAS[row_index + 2, col_index] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (row_index + 1 == ROW - 1) return false;
                    if (CANVAS[row_index + 2, col_index - 1] == 1 || CANVAS[row_index + 2, col_index] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
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
                    if (col_index - 1 == 0) return false;
                    if (CANVAS[row_index - 1, col_index - 2] == 1 || CANVAS[row_index, col_index - 2] == 1 || CANVAS[row_index + 1, col_index - 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (col_index - 1 == 0) return false;
                    if (CANVAS[row_index, col_index - 1] == 1 || CANVAS[row_index + 1, col_index - 2] == 1)
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
                    if (col_index == COLUMN - 1) return false;
                    if (CANVAS[row_index - 1, col_index] == 1 || CANVAS[row_index, col_index + 1] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
                case 2:
                    if (col_index + 1 == COLUMN - 1) return false;
                    if (CANVAS[row_index, col_index + 2] == 1 || CANVAS[row_index + 1, col_index + 1] == 1)
                    {
                        return false;
                    }
                    return true;
            }
            return true;
        }

    }
}
