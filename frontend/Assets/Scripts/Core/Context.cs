using Attributes;
using Battle.Weapons;
using DataTransfer;
using DataTransfer.Server;
using Net;
using UnityEngine;
using Utils.Singleton;


namespace Core {

    public class Context : MonoBehaviour {
        
        public Connection Connection { get; private set; }
        public SceneSwitcher Switcher { get; private set; }


        private void Awake () {
            DontDestroyOnLoad(this);

            DTO.Init();
            Serialization<Weapon>.ScanAssembly<WeaponAttribute>();

            Connection = gameObject.AddComponent<Connection>();
            Switcher = new SceneSwitcher();

            Switcher.Load(Scenes.Menu);
        }

    }

}
