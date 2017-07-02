using System.Collections.Generic;


namespace Utils {

    public static class Pool <T> where T : new() {

        private static List<T> _list = new List<T>();
        private static int _size = 0;


        public static T GetObject () {
            if (_size >= _list.Count) _list.Add(new T());
            return _list[_size++];
        }


        public static void ReclaimAll () {
            _size = 0;
        }

    }

}
