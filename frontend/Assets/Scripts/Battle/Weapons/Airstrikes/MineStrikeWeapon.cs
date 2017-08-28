﻿using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Airstrikes {

    public class MineStrikeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MineStrike,
                    The<WeaponIcons>.Get().MineStrike
                );
            }
        }

    }

}