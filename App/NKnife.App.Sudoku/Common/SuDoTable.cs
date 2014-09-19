using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.App.Sudoku.Common
{
    /// <summary>
    /// Representation of a Sudoku board
    /// </summary>
    public class SuDoTable
    {

        /// <summary>Sudoku board.</summary>
        private SuDoCell[,] _DoTable;

        /// <summary>Indexer to retrieve a Sudoku cell.</summary>
        /// <param name="row">Row for the cell (zero-based).</param>
        /// <param name="col">Col for the cell (zero-based).</param>
        public SuDoCell this[int row, int col]
        {
            get { return this._DoTable[row, col]; }
            set { this._DoTable[row, col] = value; }
        }

        /// <summary>
        /// Constructor for this class.
        /// </summary>
        public SuDoTable()
        {
            this._DoTable = new SuDoCell[9, 9];
        }

        /// <summary>
        /// Sets a value for a cell.
        /// </summary>
        /// <param name="row">Row for the cell (zero-based).</param>
        /// <param name="col">Col for the cell (zero-based).</param>
        /// <param name="value">Value for the cell.</param>
        public void SetValue(int row, int col, int value)
        {
            this[row, col].SetValue(value);

            for (int i = 0; i < 9; i++)
            {
                this[row, i].RemovePossibility(value); // remove possible value for the row
                this[i, col].RemovePossibility(value); // remove possible value for the col

                int rowS = (row / 3) * 3 + (i / 3);
                int colS = (col / 3) * 3 + (i % 3);
                this[rowS, colS].RemovePossibility(value); // remove possible value for the square
            }
        }

        /// <summary>
        /// 解答题目
        /// </summary>
        public void Solve()
        {
            // Prepare cells, rows, cols and squares pending to solve...
            List<SuDoCell> sudokuCellsToSolve = new List<SuDoCell>();
            List<int> sudokuRowsToSolve = new List<int>();
            List<int> sudokuColsToSolve = new List<int>();
            List<int> sudokuSquaresToSolve = new List<int>();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (!this[row, col].IsValid)
                    {
                        sudokuCellsToSolve.Add(this[row, col]);

                        if (!sudokuRowsToSolve.Contains(row))
                        {
                            sudokuRowsToSolve.Add(row);
                        }

                        if (!sudokuColsToSolve.Contains(col))
                        {
                            sudokuColsToSolve.Add(col);
                        }

                        int square = (row / 3) * 3 + (col / 3);
                        if (!sudokuSquaresToSolve.Contains(square))
                        {
                            sudokuSquaresToSolve.Add(square);
                        }
                    }
                }
            }

            sudokuRowsToSolve.Sort();
            sudokuColsToSolve.Sort();
            sudokuSquaresToSolve.Sort();


            // While any cell to solve...
            while (sudokuCellsToSolve.Count > 0)
            {

                while (true)
                {
                    if (this.SolveCellsWithOnePossibility(sudokuCellsToSolve))
                        continue;

                    if (this.SolveUniqueCellValueInRows(sudokuRowsToSolve))
                        continue;

                    if (this.SolveUniqueCellValueInCols(sudokuColsToSolve))
                        continue;

                    if (this.SolveUniqueCellValueInSquares(sudokuSquaresToSolve))
                        continue;

                    break;
                }

                if (sudokuCellsToSolve.Count == 0)
                    break;


                // Cannot solve any cell...now we depend on luck!! 
                // Find the cell with the minimum possibilities to apply random...
                int index = -1, minPossib = 10;

                for (int i = 0; i < sudokuCellsToSolve.Count; i++)
                {
                    if (minPossib > sudokuCellsToSolve[i].NumPossibilities)
                    {
                        index = i;
                        minPossib = sudokuCellsToSolve[i].NumPossibilities;
                    }
                }

                // We choose a solution randomly
                SuDoCell cell = sudokuCellsToSolve[index];
                int solution = cell.PickRandomPossibility();

                if (solution < 0)
                {
                    throw new ApplicationException(
                        string.Format("Cannot found a solution for cell at [{0}, {1}].",
                        cell.Row + 1, cell.Column + 1));
                }

                sudokuCellsToSolve.RemoveAt(index);
                this.SetValue(cell.Row, cell.Column, solution);

                this.OnSudokuStrategyUsed(
                    new SuDoStrategyUsedEventArgs(SuDoStrategy.RandomPick, cell));

            }


        }

        /// <summary>
        /// 解答题目
        /// </summary>
        /// <param name="doString">数独字符串</param>
        /// <returns></returns>
        public static int[] Solve(string doString)
        {
            int r = 0;
            int c = 0;
            SuDoTable dt = new SuDoTable();
            for (int i = 0; i < 81; i++)
            {
                r = i / 9;
                c = i % 9;
                dt[r, c] = new SuDoCell(r, c);
            }
            int[] doArray = SuDoHelper.GetDoIntArrary(doString);
            for (int i = 0; i < doArray.Length; i++)
            {
                r = i / 9;
                c = i % 9;
                int value = doArray[i];
                dt.SetValue(r, c, value);//值先进行校验后，保存入DoTabel
            }
            dt.Solve();
            List<int> intList = new List<int>();
            for (int i = 0; i < dt._DoTable.Length; i++)
            {
                intList.Add(dt._DoTable[i / 9, i % 9].Value);
            }
            return intList.ToArray();
        }

        /// <summary>
        /// Search for cells with an unique possible value.
        /// </summary>
        /// <param name="sudokuCellsToSolve">List of cells pending to solve.</param>
        private bool SolveCellsWithOnePossibility(List<SuDoCell> sudokuCellsToSolve)
        {
            bool result = false;
            int i = 0;

            while (i < sudokuCellsToSolve.Count)
            {
                if (sudokuCellsToSolve[i].IsValid)
                {
                    sudokuCellsToSolve.RemoveAt(i);
                }
                else if (sudokuCellsToSolve[i].NumPossibilities == 1)
                {
                    SuDoCell cell = sudokuCellsToSolve[i];
                    int solution = cell.PickRandomPossibility();
                    if (solution < 0)
                    {
                        throw new ApplicationException("Sudoku cannot be solved!");
                    }

                    this.SetValue(cell.Row, cell.Column, solution);
                    this.OnSudokuStrategyUsed(
                        new SuDoStrategyUsedEventArgs(SuDoStrategy.UniqueValueForCell, cell));
                    result = true;

                    sudokuCellsToSolve.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            return result;
        }

        /// <summary>
        /// Search for rows with an unique possible value.
        /// </summary>
        /// <param name="sudokuCellsToSolve">List of index for rows pending to solve.</param>
        private bool SolveUniqueCellValueInRows(List<int> sudokuRowsToSolve)
        {
            int i = 0, numSolutions = 0;

            while (i < sudokuRowsToSolve.Count)
            {
                // Check for unicity in the rows
                int[] valueMask = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
                int row = sudokuRowsToSolve[i];
                bool anyToSolve = false;

                for (int value = 0; value < 9; value++)
                {
                    if (this[row, value].IsValid)
                    {
                        valueMask[this[row, value].Value - 1] = -2;
                    }
                    else
                    {
                        foreach (int item in this[row, value].GetPossibilities())
                        {
                            if (valueMask[item - 1] == -1)
                            {
                                valueMask[item - 1] = value;
                                numSolutions++;
                            }
                            else if (valueMask[item - 1] != -2)
                            {
                                valueMask[item - 1] = -2;
                                numSolutions--;
                            }
                        }

                        anyToSolve = true;
                    }
                }

                if (!anyToSolve)
                {
                    sudokuRowsToSolve.RemoveAt(i);
                }
                else if (numSolutions == 0)
                {
                    i++;
                }
                else
                {
                    for (int value = 0; value < 9; value++)
                    {
                        if (valueMask[value] >= 0)
                        {
                            this.SetValue(row, valueMask[value], value + 1);
                            this.OnSudokuStrategyUsed(
                                new SuDoStrategyUsedEventArgs(SuDoStrategy.UniqueValueForRow,
                                this[row, valueMask[value]]));
                            return true;
                        }
                    }
                }

            }

            return false;
        }

        /// <summary>
        /// Search for cols with an unique possible value.
        /// </summary>
        /// <param name="sudokuCellsToSolve">List of index for cols pending to solve.</param>
        private bool SolveUniqueCellValueInCols(List<int> sudokuColsToSolve)
        {
            int i = 0, numSolutions = 0;

            while (i < sudokuColsToSolve.Count)
            {
                // Check for unicity in the cols
                int[] valueMask = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
                int col = sudokuColsToSolve[i];
                bool anyToSolve = false;

                for (int value = 0; value < 9; value++)
                {
                    if (this[value, col].IsValid)
                    {
                        valueMask[this[value, col].Value - 1] = -2;
                    }
                    else
                    {
                        foreach (int item in this[value, col].GetPossibilities())
                        {
                            if (valueMask[item - 1] == -1)
                            {
                                valueMask[item - 1] = value;
                                numSolutions++;
                            }
                            else if (valueMask[item - 1] != -2)
                            {
                                valueMask[item - 1] = -2;
                                numSolutions--;
                            }
                        }

                        anyToSolve = true;
                    }
                }

                if (!anyToSolve)
                {
                    sudokuColsToSolve.RemoveAt(i);
                }
                else if (numSolutions == 0)
                {
                    i++;
                }
                else
                {
                    for (int value = 0; value < 9; value++)
                    {
                        if (valueMask[value] >= 0)
                        {
                            this.SetValue(valueMask[value], col, value + 1);
                            this.OnSudokuStrategyUsed(
                                new SuDoStrategyUsedEventArgs(SuDoStrategy.UniqueValueForColumn,
                                this[valueMask[value], col]));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Search for squares with an unique possible value.
        /// </summary>
        /// <param name="sudokuCellsToSolve">List of index for squares pending to solve.</param>
        private bool SolveUniqueCellValueInSquares(List<int> sudokuSquaresToSolve)
        {
            int i = 0, numSolutions = 0;

            while (i < sudokuSquaresToSolve.Count)
            {
                // Check for unicity in the squares
                int[] valueMask = new int[] { -1, -1, -1, -1, -1, -1, -1, -1, -1 };
                int square = sudokuSquaresToSolve[i];
                int row = (square / 3) * 3;
                int col = (square % 3) * 3;
                bool anyToSolve = false;

                for (int value = 0; value < 9; value++)
                {
                    int rowS = row + (value / 3);
                    int colS = col + (value % 3);

                    if (this[rowS, colS].IsValid)
                    {
                        valueMask[this[rowS, colS].Value - 1] = -2;
                    }
                    else
                    {
                        foreach (int item in this[rowS, colS].GetPossibilities())
                        {
                            if (valueMask[item - 1] == -1)
                            {
                                valueMask[item - 1] = value;
                                numSolutions++;
                            }
                            else if (valueMask[item - 1] != -2)
                            {
                                valueMask[item - 1] = -2;
                                numSolutions--;
                            }
                        }

                        anyToSolve = true;
                    }
                }

                if (!anyToSolve)
                {
                    sudokuSquaresToSolve.RemoveAt(i);
                }
                else if (numSolutions == 0)
                {
                    i++;
                }
                else
                {
                    for (int value = 0; value < 9; value++)
                    {
                        if (valueMask[value] >= 0)
                        {
                            int rowS = row + (valueMask[value] / 3);
                            int colS = col + (valueMask[value] % 3);

                            this.SetValue(rowS, colS, value + 1);
                            this.OnSudokuStrategyUsed(
                                new SuDoStrategyUsedEventArgs(SuDoStrategy.UniqueValueForSquare,
                                this[rowS, colS]));
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    sb.AppendFormat(" {0}", _DoTable[row, col]);
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }

        
        public event SudokuStrategyUsedEventHandler SudokuStrategyUsed;
        protected virtual void OnSudokuStrategyUsed(SuDoStrategyUsedEventArgs e)
        {
            if (SudokuStrategyUsed != null)
            {
                SudokuStrategyUsed(this, e);
            }
        }

    }
}
