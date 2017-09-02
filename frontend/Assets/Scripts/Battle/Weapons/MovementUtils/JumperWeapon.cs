using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.MovementUtils {

    [Weapon(WeaponId.Jumper)]
    public class JumperWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jumper,
                    The<WeaponIcons>.Get().Jumper
                );
            }
        }

    }

}
