using MazeLearner.GameContent.Entity.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MazeLearner.Collections
{
    public class NonNullList<T> : IEnumerable<T>
    {
        private T[] _items;
        private readonly T defaultValue;
        public int Size => _items.Length;
        public int Count { get; private set; }
        protected NonNullList(int size, T defaultValue)
        {
            this.defaultValue = defaultValue;
            _items = new T[size];
            for (int i = 0; i < size; i++)
            {
                _items[i] = defaultValue;
            }
        }
        public T this[int index]
        {
            get => _items[index];
            set => _items[index] = value ?? defaultValue;
        }
        public void Add(T value)
        {
            EnsureCapacity(Count + 1);
            _items[Count] = value;
            ++Count;
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
        /// Get the <see cref="NonNullListEnumerator"/> for this <see cref="Bag{T}"/>. 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Use this method preferentially over <see cref="IEnumerable.GetEnumerator"/> while enumerating via foreach
        /// to avoid boxing the enumerator on every iteration, which can be expensive in high-performance environments.
        /// </remarks>
        public NonNullListEnumerator GetEnumerator()
        {
            return new NonNullListEnumerator(this);
        }

        /// <summary>
        /// Enumerates a Bag.
        /// </summary>
        public struct NonNullListEnumerator : IEnumerator<T>
        {
            private readonly NonNullList<T> _bag;
            private volatile int _index;

            /// <summary>
            /// Creates a new <see cref="NonNullListEnumerator"/> for this <see cref="Bag{T}"/>.
            /// </summary>
            /// <param name="bag"></param>
            public NonNullListEnumerator(NonNullList<T> bag)
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