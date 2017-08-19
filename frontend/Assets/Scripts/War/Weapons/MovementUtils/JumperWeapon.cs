﻿using Assets;
using Utils.Singleton;


namespace War.Weapons.MovementUtils {

    public class JumperWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.Jumper,
                    The<WeaponIcons>.Get().Jumper
                );
            }
        }

    }

}
