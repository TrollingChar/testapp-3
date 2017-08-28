using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Attributes;
using Commands.Client;
using Commands.Server;
using UnityEngine;


namespace Commands {

    public static class Serialization {

        private static Dictionary<Type, int> _codeByServerCmdType = new Dictionary<Type, int>();
        private static Dictionary<int, Type> _serverCmdTypeByCode = new Dictionary<int, Type>();

        private static Dictionary<Type, int> _codeByClientCmdType = new Dictionary<Type, int>();
        private static Dictionary<int, Type> _clientCmdTypeByCode = new Dictionary<int, Type>();


        public static void ScanAssembly () {
            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
                if (type.GetInterfaces().Contains(typeof(IClientCommand))) {
                    AddClientCommand(type);
                }
                if (type.GetInterfaces().Contains(typeof(IServerCommand))) {
                    AddServerCommand(type);
                }
            }
        }


        private static void AddServerCommand (Type type) {
            var attribute = (ServerCommandAttribute)
                type.GetCustomAttributes(true).FirstOrDefault(a => a is ServerCommandAttribute);
            if (attribute == null) return;

            if (_serverCmdTypeByCode.ContainsKey(attribute.Id)) {
                throw new Exception("FATAL: server commands must have distinct codes!");
            }
            
            Debug.Log(attribute.Id);

            _serverCmdTypeByCode[attribute.Id] = type;
            _codeByServerCmdType[type] = attribute.Id;
        }


        private static void AddClientCommand (Type type) {
            var attribute = (ClientCommandAttribute)
                type.GetCustomAttributes(true).FirstOrDefault(a => a is ClientCommandAttribute);
            if (attribute == null) return;

            if (_clientCmdTypeByCode.ContainsKey(attribute.Id)) {
                throw new Exception("FATAL: client commands must have distinct codes!");
            }

            _clientCmdTypeByCode[attribute.Id] = type;
            _codeByClientCmdType[type] = attribute.Id;
        }


        public static IServerCommand GetServerCmdByCode (byte code) {
            Debug.Log(code);
            Debug.Log(_serverCmdTypeByCode[code]);
            return (IServerCommand) Activator.CreateInstance(_serverCmdTypeByCode[code]);
        }

    }

}
