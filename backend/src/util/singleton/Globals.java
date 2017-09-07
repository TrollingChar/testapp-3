package util.singleton;

import java.util.HashMap;

@SuppressWarnings("unchecked")
public class Globals {

    // java generics suck
    // why i can't just write The<Server> ? - because it's java
    // why i can't just write The.<Server>get() ? - because i can't write T.class, stupid java
    // why should i pass class in another parameter, java?
    // ...

    private static HashMap<Class<?>, Object> map = new HashMap<>();

    public static <T> T get(Class<T> clazz) {
        return (T) map.get(clazz);
    }

    public static <T> void set(Class<T> clazz, T t) {
        System.err.println(t.getClass());
        map.put(clazz, t);
    }
}
