using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Rope)]
    public class RopeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Rope,
                    The<WeaponIcons>.Get().Rope
                );
            }
        }

    }

}
