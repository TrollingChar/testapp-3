﻿using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    [Weapon(WeaponId.Overheal)]
    public class OverhealWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Overheal,
                    The<WeaponIcons>.Get().Overheal
                );
            }
        }

    }

}
