﻿using Assets;
using Utils.Singleton;


namespace War.Weapons.Firearms {

    public class PoisonArrowWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.PoisonArrow,
                    The<WeaponIcons>.Get().PoisonArrow
                );
            }
        }

    }

}