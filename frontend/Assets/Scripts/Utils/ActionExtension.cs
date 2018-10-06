using System;
// ReSharper disable InconsistentNaming


namespace Utils {

    public static class ActionExtension {

        public static void _ (this Action f) {
            if (f != null) f ();
        }

        public static void _ <A> (this Action<A> f, A a) {
            if (f != null) f (a);
        }

        public static void _ <A, B> (this Action<A, B> f, A a, B b) {
            if (f != null) f (a, b);
        }

    }

}