using Attributes;
using Battle;
using Battle.Weapons;
using Commands;
using Commands.Client;
using Commands.Server;
using DataTransfer;
using Net;
using UnityEngine;
using Utils.Singleton;


namespace Core {

    public class Context : MonoBehaviour {

        private WSConnection _connection;
        private SceneSwitcher _sceneSwitcher;


        private void Awake () {
            DontDestroyOnLoad(this);
            
            DTO.Init();
            
//            Serialization<ClientCommand>.ScanAssembly<ClientCmdAttribute>();
//            Serialization<IServerCommand>.ScanAssembly<ServerCmdAttribute>();
            Serialization<Weapon>.ScanAssembly<WeaponAttribute>();

            _connection = gameObject.AddComponent<WSConnection>();
            _sceneSwitcher = new SceneSwitcher();

            CommandExecutor<AuthorizedCmd>.AddHandler(OnAuthorized);
            CommandExecutor<GameStartedCmd>.AddHandler(OnGameStarted);
            _sceneSwitcher.Load(Scenes.Menu);
        }


        private void OnGameStarted (GameStartedCmd cmd) {
            _sceneSwitcher.Load(Scenes.Battle, cmd.Data);
        }


        private void OnAuthorized (AuthorizedCmd cmd) {
            The<PlayerInfo>.Set(cmd.PlayerInfo);
            CommandExecutor<AuthorizedCmd>.RemoveHandler(OnAuthorized);
        }

    }

}
