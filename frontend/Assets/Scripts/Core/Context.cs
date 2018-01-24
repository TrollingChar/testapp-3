using Attributes;
using Battle.Weapons;
using DataTransfer;
using Net;
using UnityEngine;


namespace Core {

    public class Context : MonoBehaviour {

        public Connection Connection { get; private set; }
        public SceneSwitcher Switcher { get; private set; }


        private void Awake () {
            DontDestroyOnLoad(this);
            Application.targetFrameRate = 60;

            DTO.Init();
            Serialization<Weapon>.ScanAssembly<WeaponAttribute>();

            Connection = gameObject.AddComponent<Connection>();
            Switcher = new SceneSwitcher();

            Switcher.Load(Scenes.Menu, false); // dont bypass connection menu
        }

    }

}
