using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.Thrown {

    [Weapon(WeaponId.ControlledGrenade)]
    public class ControlledGrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.ControlledGrenade,
                    The<WeaponIcons>.Get().ControlledGrenade
                );
            }
        }

    }

}
