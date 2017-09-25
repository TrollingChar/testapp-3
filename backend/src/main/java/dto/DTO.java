package dto;

import io.netty.buffer.ByteBuf;
import org.reflections.Reflections;

import java.lang.reflect.Constructor;
import java.util.HashMap;
import java.util.Set;


public abstract class DTO {

    private static HashMap<Short, Class<? extends DTO>> classes;
    private static HashMap<Class<? extends DTO>, Short> codes;


    public static void init () {
        classes = new HashMap<>();
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
        System.out.printf("done - found %d DTO\n", classes.size());
    }


    private static void register (Class<? extends DTO> clazz, DTOs value) {
        // todo: check if there already are values in maps
        classes.put(value.code, clazz);
        codes.put(clazz, value.code);
    }


    public static <T extends DTO> T read (ByteBuf byteBuf) throws DTOException {
        try {
            short code = byteBuf.readShort();
            DTO result = classes.get(code).newInstance();
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


    public abstract void writeMembers (ByteBuf byteBuf) throws Exception;
    public abstract void readMembers (ByteBuf buffer) throws Exception;
}
