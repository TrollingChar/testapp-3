package server.core;

import org.reflections.Reflections;
import server.annotations.ClientEventCode;
import server.event.client.ClientEvent;

import java.lang.annotation.Annotation;
import java.lang.reflect.InvocationTargetException;
import java.util.Set;

public class CmdManager {

    static {
        scanCommands(ClientEvent.class, ClientEventCode.class);
    }

    private static <TClass, TAnnotation extends Annotation>
    void scanCommands(Class<TClass> baseClass, Class<TAnnotation> annotationClass) {
        Reflections refl = new Reflections();
        Set<Class<? extends TClass>> classes = refl.getSubTypesOf(baseClass);
        for (Class<? extends TClass> clazz : classes) {
            TAnnotation annotation;
            if ((annotation = clazz.getAnnotation(annotationClass)) != null) {
                try {
                    bind(clazz, (byte) annotation.annotationType().getMethod("value").invoke(annotation));
                } catch (NoSuchMethodException | IllegalAccessException | InvocationTargetException e) {
                    e.printStackTrace();
                }
            }
        }
    }

    private static <TClass> void bind(Class<TClass> clazz, byte code) {
        System.out.println("...");
    }

}
