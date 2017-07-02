using UnityEngine;
using Random = System.Random;


namespace Utils {

    public static class RNG {

        private static Random rng = new Random();


        public static void Init (int seed) {
            rng = new Random(seed);
        }


        public static bool Bool () {
            return rng.Next(2) != 0;
        }


        public static bool Bool (double chance) {
            return rng.NextDouble() < chance;
        }


        public static bool Bool (int chance, int outOf) {
            return rng.Next(outOf) < chance;
        }


        public static int Int () {
            return rng.Next();
        }


        public static int Int (int max) {
            return rng.Next(max);
        }


        public static int Int (int min, int max) {
            return rng.Next(min, max);
        }


        public static float Float () {
            return (float) rng.NextDouble();
        }


        public static double Double () {
            return rng.NextDouble();
        }


        public static Vector2 Vector2 () {
            return new Vector2(Float(), Float());
        }

    }

}
