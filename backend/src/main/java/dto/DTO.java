package dto;

import io.netty.buffer.ByteBuf;
import org.reflections.Reflections;

import java.lang.reflect.Constructor;
import java.util.HashMap;
import java.util.Set;


public abstract class DTO {

    private static HashMap<Short, Constructor<? extends DTO>> constructors;
    private static HashMap<Class<? extends DTO>, Short> codes;


    public static void init () {
        constructors = new HashMap<>();
        codes = new HashMap<>();

        System.out.println("scanning DTO...");

        Reflections refl = new Reflections();
        Set<Class<? extends DTO>> classes = refl.getSubTypesOf(DTO.class);
        for (Class<? extends DTO> clazz : classes) {
            DTOCode annotation;
            if ((annotation = clazz.getAnnotation(DTOCode.class)) != null) {
                register(clazz, annotation.value());
            }
        }
        System.out.printf("done - found %d DTO\n", constructors.size());
    }


    private static void register (Class<? extends DTO> clazz, DTOs value) {
        try {
            // todo: check if there already are values in maps
            constructors.put(value.code, clazz.getConstructor());
            codes.put(clazz, value.code);
        }
        catch (NoSuchMethodException e) {
            e.printStackTrace();
        }
    }


    public static <T extends DTO> T read (ByteBuf byteBuf) throws DTOException {
        try {
            short code = byteBuf.readShort();
            DTO result = constructors.get(code).newInstance();
            result.readMembers(byteBuf);
            return (T) result;
        }
        catch (Exception e) {
            throw new DTOException("Unable to read DTO from byteBuf", e);
        }
    }


    public void write (ByteBuf byteBuf) throws DTOException {
        try {
            byteBuf.writeShort(getClass().getAnnotation(DTOCode.class).value().code);
            writeMembers(byteBuf);
        }
        catch (Exception e) {
            throw new DTOException("Unable to write DTO to byteBuf", e);
        }
    }


    protected abstract void writeMembers (ByteBuf byteBuf) throws Exception;
    protected abstract void readMembers (ByteBuf buffer) throws Exception;
}
