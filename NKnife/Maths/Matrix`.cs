using System;
using System.Linq;
using CommunityToolkit.HighPerformance;

namespace NKnife.Maths
{
    /// <summary>
    /// 描述一个简单的矩阵，内部通过一维数组存储数据。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Matrix<T>
    {
        private readonly Guid _id;
        
        public static Matrix<T> NullMatrix => new Matrix<T>(0, 0);

        public Matrix() : this(0, 0) { }

        public Matrix(int rowSize, int columnSize)
        {
            if (rowSize < 0)
                throw new ArgumentOutOfRangeException(nameof(rowSize));
            if (columnSize < 0)
                throw new ArgumentOutOfRangeException(nameof(columnSize));
            _id = Guid.NewGuid();
            RowSize = rowSize;
            ColumnSize = columnSize;
            // 分配内存
            Elements = new T[columnSize * rowSize];
        }

        public Matrix(T[,] value)
        {
            _id = Guid.NewGuid();
            //指定值(二维数组)构造函数
            var span = new Span2D<T>(value);
            RowSize = span.Height;
            ColumnSize = span.Width;
            Elements = new T[span.Height * span.Width];
            span.TryCopyTo(Elements);
        }

        /// <summary>
        ///     方阵构造函数, 长宽相等
        /// </summary>
        /// <param name="size">长或宽的长度</param>
        public Matrix(int size) : this(size, size) { }

        public Matrix(Matrix<T> other) : this(other.RowSize, other.ColumnSize, other.Elements) { }
        
        public Matrix(int rowSize, int columnSize, T value)
        {
            _id = Guid.NewGuid();
            RowSize = rowSize;
            ColumnSize = columnSize;
            Elements = Enumerable.Repeat(value, rowSize * columnSize).ToArray();
        }
        
        /// <summary>
        ///     通过一维数组初始化矩阵
        /// </summary>
        public Matrix(int rowSize, int columnSize, T[] value)
        {
            if (rowSize * columnSize < value.Length)
                throw new ArgumentException(nameof(rowSize));
            _id = Guid.NewGuid();
            var span = new Span<T>(value);
            RowSize = rowSize;
            ColumnSize = columnSize;
            Elements = new T[value.Length];
            span.CopyTo(Elements);
        }

        /// <summary>
        ///     矩阵数据缓冲区
        /// </summary>
        public T[] Elements { get; set; }

        public T GetElement(int row = 0, int column = 0) => Elements[column + row * ColumnSize];
        public void SetElement(T value, int row = 0, int column = 0) => Elements[column + row * ColumnSize] = value;

        private T this[int row, int column]
        {
            get => Elements[column + row * ColumnSize];
            set => Elements[column + row * ColumnSize] = value;
        }
        public int ColumnSize { get; set; }
        public int RowSize { get; set; }

        /// <summary>
        ///     获取指定行的向量
        /// </summary>
        public int GetRowVector(int rowNumber, out T[] pVector)
        {
            pVector = new T[ColumnSize];
            for (var j = 0; j < ColumnSize; ++j)
                pVector[j] = this[rowNumber, j];

            return ColumnSize;
        }

        /// <summary>
        ///     获取指定列的向量
        /// </summary>
        public int GetColumnVector(int columnNumber, out T[] pVector)
        {
            pVector = new T[RowSize];
            for (var i = 0; i < RowSize; ++i)
                pVector[i] = this[i, columnNumber];

            return RowSize;
        }

        public override bool Equals(object? other)
        {
            if(other == null) return false;
            var matrix = other as Matrix<T>;

            if(matrix == null)
                return false;

            // 首先检查行列数是否相等
            if(ColumnSize != matrix.ColumnSize
               || ColumnSize != matrix.ColumnSize)
                return false;

            for (var i = 0; i < RowSize; ++i)
            {
                for (var j = 0; j < ColumnSize; ++j)
                {
                    var ele      = this[i, j];
                    var otherEle = matrix[i, j];

                    if(otherEle != null
                       && ele != null)
                    {
                        if(!ele.Equals(otherEle))
                            return false;
                    }
                    else if(ele == null
                            && otherEle != null)
                    {
                        return false;
                    }
                    else if(ele != null
                            && otherEle == null)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public Matrix<TSub> Select<TSub>(Func<T, TSub> selector)
        {
            var matrix = new Matrix<TSub>(RowSize, ColumnSize)
            {
                Elements = Elements.Select(selector).ToArray()
            };

            return matrix;
        }

        public void Resize(int rowSize, int columnSize)
        {
            RowSize = rowSize;
            ColumnSize = columnSize;

            if(Elements.Length < rowSize * columnSize)
            {
                var newElements = new T[rowSize * columnSize];
                Elements.CopyTo(newElements, 0);
                Elements = newElements;
            }
            else if(Elements.Length > rowSize * columnSize)
            {
                var newElements = new T[rowSize * columnSize];
                Array.Copy(Elements, newElements, rowSize * columnSize);
                Elements = newElements;
            }
        }

        public bool IsEmpty() => RowSize == 0 && ColumnSize == 0;
        
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
    }
}