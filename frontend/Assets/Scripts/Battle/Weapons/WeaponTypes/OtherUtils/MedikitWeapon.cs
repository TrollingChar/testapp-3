using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Medikit)]
    public class MedikitWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Medikit,
                    The<WeaponIcons>.Get().Medikit
                );
            }
        }

    }

}
