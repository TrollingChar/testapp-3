package commands;

import annotations.ClientCommandCode;
import commands.client.ClientCommand;
import org.reflections.Reflections;

import java.util.HashMap;
import java.util.Map;
import java.util.Set;

// todo: commands must have distinct codes
public class Commands {

    private static Map<Byte, Class<? extends ClientCommand>> clientCommands;

    private Commands () {
        // no instance
    }

    public static void init() {
        System.out.println("scanning client commands...");
        clientCommands = new HashMap<>();

        Reflections refl = new Reflections();
        Set<Class<? extends ClientCommand>> classes = refl.getSubTypesOf(ClientCommand.class);
        for (Class<? extends ClientCommand> clazz : classes) {
            ClientCommandCode annotation;
            if ((annotation = clazz.getAnnotation(ClientCommandCode.class)) != null) {
                register(clazz, annotation.value());
            }
        }
        System.out.printf("done - found %d client commands\n", clientCommands.size());
    }

    private static void register(Class<? extends ClientCommand> clazz, byte value) {
        clientCommands.put(value, clazz);
    }

    public static ClientCommand resolveClientCommand(byte code) {
        try {
            return clientCommands.get(code).newInstance();
        } catch (InstantiationException | IllegalAccessException e) {
            e.printStackTrace();
        }
        return null; // todo wrap exception
    }
}
