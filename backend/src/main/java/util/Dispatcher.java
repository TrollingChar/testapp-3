package util;

import java.util.ArrayList;
import java.util.List;
import java.util.function.Consumer;


public class Dispatcher <T> {

    // Set быстрее, но List гарантирует правильный порядок вызовов
    private List<Consumer<T>> listeners = new ArrayList<>();


    public void addListener (Consumer<T> listener) {
        listeners.add(listener);
    }


    public void removeListener (Consumer<T> listener) {
        listeners.remove(listener);
    }


    public void dispatch (T event) {
        for (Consumer<T> listener : listeners) listener.accept(event);
    }
}
