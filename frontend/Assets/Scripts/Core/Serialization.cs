using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Attributes;
using Commands.Client;
using Commands.Server;
using UnityEngine;


namespace Commands {

    public static class Serialization<TSerializable> {

        private static Dictionary<Type, byte> _codeByType = new Dictionary<Type, byte>();
        private static Dictionary<byte, Type> _typeByCode = new Dictionary<byte, Type>();


        public static void ScanAssembly<TAttribute> () where TAttribute : IdAttribute {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
                if (type.GetInterfaces().Contains(typeof(TSerializable))) {
                    AddCommand<TAttribute>(type);
                }
            }
        }


        private static void AddCommand<TAttribute> (Type type) where TAttribute : IdAttribute {
            var attribute = (TAttribute) type.GetCustomAttributes(true).FirstOrDefault(a => a is TAttribute);
            if (attribute == null) return;

            if (_typeByCode.ContainsKey(attribute.Id)) {
                throw new Exception("FATAL: server commands must have distinct codes!");
            }

            _typeByCode[attribute.Id] = type;
            _codeByType[type] = attribute.Id;
        }


        public static TSerializable GetNewInstanceByCode (byte code) {
            return (TSerializable) Activator.CreateInstance(_typeByCode[code]);
        }


        public static byte GetCodeByType (Type type) {
            return _codeByType[type];
        }

    }

}
