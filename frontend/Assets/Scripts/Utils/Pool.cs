using System.Collections.Generic;

namespace W3 {
    public static class Pool<T> where T : new() {

        private static List<T> list = new List<T>();
        private static int size = 0;

        public static T GetObject () {
            if (size >= list.Count) list.Add(new T());
            return list[size++];
        }

        public static void ReclaimAll () {
            size = 0;
        }
    }
}
