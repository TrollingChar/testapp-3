﻿using Assets;
using Utils.Singleton;


namespace War.Weapons.OtherUtils {

    public class MagnetWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Magnet,
                    The<WeaponIcons>.Get().Magnet
                );
            }
        }

    }

}
