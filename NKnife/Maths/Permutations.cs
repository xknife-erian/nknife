using System;
using System.Collections;
using System.Collections.Generic;

namespace NKnife.Maths
{
    /// <summary>
    ///     描述数学中的“排列”概念
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Permutations<T> : IEnumerable<IList<T>>
    {
        private readonly List<int[]> _indices;
        private readonly int _length;
        private readonly List<IList<T>> _permutations;
        private int _level = -1;
        private int[] _value;

        public Permutations(IList<T> items)
            : this(items, items.Count)
        {
        }

        public Permutations(IList<T> items, int length)
        {
            _length = length;
            _permutations = new List<IList<T>>();
            _indices = new List<int[]>();
            BuildIndices();
            foreach (var oneCom in new Combinations<T>(items, length))
            {
                _permutations.AddRange(GetPermutations(oneCom));
            }
        }

        public int Count
        {
            get { return _permutations.Count; }
        }

        public IEnumerator<IList<T>> GetEnumerator()
        {
            return _permutations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _permutations.GetEnumerator();
        }

        private void BuildIndices()
        {
            _value = new int[_length];
            Visit(0);
        }

        private void Visit(int k)
        {
            _level += 1;
            _value[k] = _level;

            if (_level == _length)
            {
                _indices.Add(_value);
                var newValue = new int[_length];
                Array.Copy(_value, newValue, _length);
                _value = newValue;
            }
            else
            {
                for (int i = 0; i < _length; i++)
                {
                    if (_value[i] == 0)
                    {
                        Visit(i);
                    }
                }
            }

            _level -= 1;
            _value[k] = 0;
        }

        private IEnumerable<IList<T>> GetPermutations(IList<T> oneCom)
        {
            var t = new List<IList<T>>();

            foreach (var idxs in _indices)
            {
                var onePerm = new T[_length];
                for (int i = 0; i < _length; i++)
                {
                    onePerm[i] = oneCom[idxs[i] - 1];
                }
                t.Add(onePerm);
            }

            return t;
        }
    }
}