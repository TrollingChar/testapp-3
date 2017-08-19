﻿using Assets;
using Utils.Singleton;


namespace War.Weapons.Thrown {

    public class ControlledGrenadeWeapon {

        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) Weapons.ControlledGrenade,
                    The<WeaponIcons>.Get().ControlledGrenade
                );
            }
        }

        

    }

}