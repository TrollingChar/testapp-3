using UnityEngine;


namespace Core {

    public struct Time {

        public const int TPS = 50;

        public int Ticks;

        public float Seconds {
            get { return (float) Ticks / TPS; }
            set { Ticks = Mathf.RoundToInt(value * TPS); }
        }


        public override string ToString () {
            return Mathf.CeilToInt(Seconds).ToString();
        }


        public static bool operator == (Time a, Time b) {
            return a.Ticks == b.Ticks;
        }


        public static bool operator != (Time a, Time b) {
            return a.Ticks != b.Ticks;
        }


        public static bool operator < (Time a, Time b) {
            return a.Ticks < b.Ticks;
        }


        public static bool operator > (Time a, Time b) {
            return a.Ticks < b.Ticks;
        }


        public static bool operator <= (Time a, Time b) {
            return a.Ticks <= b.Ticks;
        }


        public static bool operator >= (Time a, Time b) {
            return a.Ticks <= b.Ticks;
        }

    }

}
