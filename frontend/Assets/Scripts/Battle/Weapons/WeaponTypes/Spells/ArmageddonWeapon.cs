using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Armageddon)]
    public class ArmageddonWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Armageddon,
                    The<WeaponIcons>.Get().Armageddon
                );
            }
        }

        protected override void OnShoot () {}

    }

}
