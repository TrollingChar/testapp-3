﻿using System;


namespace Utils.Singleton {

    [Obsolete]
    public class The<T> {

        private static T _instance;


        public static T Get () {
            return _instance;
        }


        public static void Set (T value) {
            _instance = value;
        }

    }

}
