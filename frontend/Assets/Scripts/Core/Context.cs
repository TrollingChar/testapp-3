using Attributes;
using Battle.Weapons;
using Commands.Server;
using DataTransfer;
using DataTransfer.Server;
using Net;
using UnityEngine;
using Utils.Singleton;


namespace Core {

    public class Context : MonoBehaviour {

        private Connection _connection;
        private SceneSwitcher _sceneSwitcher;


        private void Awake () {
            DontDestroyOnLoad(this);

            DTO.Init();

//            Serialization<ClientCommand>.ScanAssembly<ClientCmdAttribute>();
//            Serialization<IServerCommand>.ScanAssembly<ServerCmdAttribute>();
            Serialization<Weapon>.ScanAssembly<WeaponAttribute>();

            _connection = gameObject.AddComponent<Connection>();
            _sceneSwitcher = new SceneSwitcher();

            CommandExecutor<AuthSuccessCmd>.AddHandler(OnAuthSuccess);
            CommandExecutor<NewGameCmd>.AddHandler(OnNewGame);
            _sceneSwitcher.Load(Scenes.Menu);
        }


        private void OnNewGame (NewGameCmd cmd) {
            _sceneSwitcher.Load(Scenes.Battle, cmd.Data);
        }


        private void OnAuthSuccess (AuthSuccessCmd cmd) {
            The<PlayerInfo>.Set(cmd.PlayerInfo);
            CommandExecutor<AuthSuccessCmd>.RemoveHandler(OnAuthSuccess);
        }

    }

}
