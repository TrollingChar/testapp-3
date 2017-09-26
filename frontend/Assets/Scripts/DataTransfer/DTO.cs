using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Attributes;
using UnityEngine;


namespace DataTransfer {

    public abstract class DTO {

        private static Dictionary<short, Type> _types;
        private static Dictionary<Type, DTOCode> _codes;


        public static void Init () {
            _types = new Dictionary<short, Type>();
            _codes = new Dictionary<Type, DTOCode>();

            Debug.Log("scanning DTO...");

            var baseType = typeof(DTO);
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
                if (!type.IsSubclassOf(baseType) && !type.GetInterfaces().Contains(baseType)) continue;

                var attribute = (DTOAttribute) type.GetCustomAttributes(true).FirstOrDefault(a => a is DTOAttribute);
                if (attribute == null) {
//                    Debug.LogError("No attribute on class " + type);
                    continue;
                }
                Register(type, attribute.Code);
            }
            Debug.Log("done - found " + _types.Count + " DTO");
        }


        private static void Register (Type type, DTOCode code) {
            _types.Add((short) code, type);
            _codes.Add(type, code);
        }


        public static DTO Read (BinaryReader reader) {
            short code = reader.ReadInt16();
            var result = (DTO) Activator.CreateInstance(_types[code]);
            result.ReadMembers(reader);
            return result;
        }


        public void Write (BinaryWriter writer) {
            writer.Write((short) _codes[GetType()]);
            WriteMembers(writer);
        }


        protected abstract void WriteMembers (BinaryWriter writer);
        protected abstract void ReadMembers (BinaryReader reader);

    }

}
