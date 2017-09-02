using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.OtherUtils {

    [Weapon(WeaponId.Girder)]
    public class GirderWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Girder,
                    The<WeaponIcons>.Get().Girder
                );
            }
        }

    }

}
