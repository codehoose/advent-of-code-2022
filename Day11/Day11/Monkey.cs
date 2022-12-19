using System;
using System.Collections.Generic;

namespace Day11
{
    internal class Monkey
    {
        private List<int> _items;
        private Func<int, int> _operation;
        private Func<int, int> _test;

        private int _current;

        private int _id;

        private uint _inspected;

        public int CurrentItem => _current;

        public uint Inspected => _inspected;

        public bool HasItems => _items.Count > 0;

        public int Id => _id;

        public int DoAll()
        {
            _inspected++;
            return _test(_operation(InspectNext()));
        }

        public void Inspect()
        {
            _current = _items[0];
            _inspected++;
        }

        public void Worry()
        {
            _current = _operation(_current);
        }

        public void Bored()
        {
            _current = (int)Math.Floor(_current / 3f);
        }

        public int ChuckTo()
        {
            _items.RemoveAt(0);
            return _test(_current);
        }

        public void Receive(int item)
        {
            _items.Add(item);
        }

        public Monkey(int id, IEnumerable<int> items, Func<int, int> operation, Func<int, int> test)
        {
            _id = id;
            _items = new List<int>();
            _items.AddRange(items);
            _operation = operation;
            _test = test;
        }

        private int InspectNext()
        {
            int temp = _items[0];
            _items.RemoveAt(0);
            return temp;
        }
    }
}
