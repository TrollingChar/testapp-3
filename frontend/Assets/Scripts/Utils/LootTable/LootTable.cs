using Utils.Random;
using Utils.Tuple;


namespace Utils.LootTable {

    public class LootTable<T> {

//        private T _content;
        private T[] _table;
        private int[] _sums;


        public LootTable () {}


        public LootTable (T content) {
            //_content = content;
            _table = new[] {content};
            _sums  = new[] {1};
        }


        public LootTable (params Tuple<T, int>[] table) {
            SetTable(table);
        }


        protected void SetTable (params Tuple<T, int>[] table) {
            _table = new T[table.Length];
            _sums = new int[table.Length];
            for (int i = 0, sum = 0; i < table.Length; i++) {
                _table[i] = table[i].Item1;
                _sums[i] = sum += table[i].Item2;
            }
        }


        public T GetLoot () {
//            if (_table == null) return _content;
            int n = RNG.Int(_sums[_sums.Length - 1]);

            int l = 0;
            int h = _sums.Length;
            while (l != h) {
                int m = (l + h) / 2;
                if (_sums[m] > n) h = m;
                else l = m + 1;
            }

//            var table = h < _table.Length ? _table[h] : null;
//            return table == null ? default(T) : table.GetLoot();
            return _table[h];
        }

    }

}
