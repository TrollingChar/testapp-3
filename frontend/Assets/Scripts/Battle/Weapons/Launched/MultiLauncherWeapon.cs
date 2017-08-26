﻿using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Launched {

    public class MultiLauncherWeapon : StandardWeapon {


        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.MultiLauncher,
                    The<WeaponIcons>.Get().MultiLauncher
                );
            }
        }
        

    }

}
