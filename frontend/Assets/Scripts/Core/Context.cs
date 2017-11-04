﻿using Attributes;
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

            DTO.Init();
            Serialization<Weapon>.ScanAssembly<WeaponAttribute>();

            Connection = gameObject.AddComponent<Connection>();
            Switcher = new SceneSwitcher();

            Switcher.Load(Scenes.Menu);
        }

    }

}
