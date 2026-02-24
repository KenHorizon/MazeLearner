using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLearner.Collections
{
    public class Array<T> : IEnumerable<T>
    {
        private T[] _items;
        private readonly bool _isPrimitive;

        public int Capacity => _items.Length;
        public bool IsEmpty => Count == 0;
        public int Count { get; private set; }

        public Array(int capacity = 16)
        {
            _isPrimitive = typeof(T).IsPrimitive;
            _items = new T[capacity];
        }

        public T this[int index]
        {
            get => index >= _items.Length ? default(T) : _items[index];
            set
            {
                EnsureCapacity(index + 1);
                if (index >= Count)
                    Count = index + 1;
                _items[index] = value;
            }
        }

        public void Add(T element)
        {
            EnsureCapacity(Count + 1);
            _items[Count] = element;
            ++Count;
        }

        public void AddRange(Array<T> range)
        {
            for (int index = 0, j = range.Count; j > index; ++index)
                Add(range[index]);
        }

        public void Clear()
        {
            if (Count == 0)
                return;

            // non-primitive types are cleared so the garbage collector can release them
            if (!_isPrimitive)
                Array.Clear(_items, 0, Count);

            Count = 0;
        }

        public bool Contains(T element)
        {
            for (var index = Count - 1; index >= 0; --index)
            {
                if (element.Equals(_items[index]))
                    return true;
            }

            return false;
        }

        public T RemoveAt(int index)
        {
            var result = _items[index];
            --Count;
            _items[index] = _items[Count];
            _items[Count] = default(T);
            return result;
        }

        public bool Remove(T element)
        {
            for (var index = Count - 1; index >= 0; --index)
            {
                if (element.Equals(_items[index]))
                {
                    --Count;
                    _items[index] = _items[Count];
                    _items[Count] = default(T);

                    return true;
                }
            }

            return false;
        }

        public bool RemoveAll(Array<T> bag)
        {
            var isResult = false;

            for (var index = bag.Count - 1; index >= 0; --index)
            {
                if (Remove(bag[index]))
                    isResult = true;
            }

            return isResult;
        }

        private void EnsureCapacity(int capacity)
        {
            if (capacity < _items.Length)
                return;

            var newCapacity = Math.Max((int)(_items.Length * 1.5), capacity);
            var oldElements = _items;
            _items = new T[newCapacity];
            Array.Copy(oldElements, 0, _items, 0, oldElements.Length);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Get the <see cref="ArrayEnumerator"/> for this <see cref="Bag{T}"/>. 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Use this method preferentially over <see cref="IEnumerable.GetEnumerator"/> while enumerating via foreach
        /// to avoid boxing the enumerator on every iteration, which can be expensive in high-performance environments.
        /// </remarks>
        public ArrayEnumerator GetEnumerator()
        {
            return new ArrayEnumerator(this);
        }

        /// <summary>
        /// Enumerates a Bag.
        /// </summary>
        public struct ArrayEnumerator : IEnumerator<T>
        {
            private readonly Array<T> _bag;
            private volatile int _index;

            /// <summary>
            /// Creates a new <see cref="ArrayEnumerator"/> for this <see cref="Bag{T}"/>.
            /// </summary>
            /// <param name="bag"></param>
            public ArrayEnumerator(Array<T> bag)
            {
                _bag = bag;
                _index = -1;
            }

            readonly T IEnumerator<T>.Current => _bag[_index];

            readonly object IEnumerator.Current => _bag[_index];

            /// <summary>
            /// Gets the element in the <see cref="Bag{T}"/> at the current position of the enumerator.
            /// </summary>
            public readonly T Current => _bag[_index];

            /// <inheritdoc/>
            public bool MoveNext()
            {
                return ++_index < _bag.Count;
            }

            /// <inheritdoc/>
            public readonly void Dispose()
            {
            }

            /// <inheritdoc/>
            public readonly void Reset()
            {
            }
        }
    }
}
