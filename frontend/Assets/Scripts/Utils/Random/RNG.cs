using System.Collections.Generic;
using UnityEngine;


namespace Utils.Random {

    public static class RNG {

        private static System.Random _rng = new System.Random();


        public static void Init (int seed) {
            _rng = new System.Random(seed);
        }


        public static bool Bool () {
            return _rng.Next(2) != 0;
        }


        public static bool Bool (double chance) {
            return _rng.NextDouble() < chance;
        }


        public static bool Bool (int chance, int outOf) {
            return _rng.Next(outOf) < chance;
        }


        public static int Int () {
            return _rng.Next();
        }


        public static int Int (int max) {
            return _rng.Next(max);
        }


        public static int Int (int min, int max) {
            return _rng.Next(min, max);
        }


        public static float Float () {
            return (float) _rng.NextDouble();
        }


        public static float SignedFloat () {
            return Float() - 0.5f;
        }


        public static float Angle () {
            return Float() * 2f * Mathf.PI;
        }


//        public static double Double () {
//            return _rng.NextDouble();
//        }


/*
        public static Vector2 Vector2 () {
            return new Vector2(Float(), Float());
        }
*/


        public static T Pick<T> (List<T> list) {
            return list[Int(list.Count)];
        }


        public static List<T> PickSome<T> (List<T> list, int count) {
            var array = list.ToArray();
            var result = new List<T>();
            int j = array.Length;
            for (int i = 0; i < count; i++) {
                if (j == 1) result.Add(array[--j]);
                if (j == 0) break;
                int temp = Int(j);
                result.Add(array[temp]);
                array[temp] = array[--j];
            }
            return result;
        }

    }

}
