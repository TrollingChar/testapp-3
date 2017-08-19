﻿using Assets;
using Utils.Singleton;


namespace War.Weapons.Firearms {

    public class UltraRifleWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.UltraRifle,
                    The<WeaponIcons>.Get().UltraRifle
                );
            }
        }

    }

}
