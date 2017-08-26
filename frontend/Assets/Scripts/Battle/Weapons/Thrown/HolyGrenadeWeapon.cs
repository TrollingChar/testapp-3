﻿using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    public class HolyGrenadeWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.HolyGrenade,
                    The<WeaponIcons>.Get().HolyGrenade
                );
            }
        }

    }

}
