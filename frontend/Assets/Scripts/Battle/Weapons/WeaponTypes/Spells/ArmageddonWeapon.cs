using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Armageddon)]
    public class ArmageddonWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Armageddon,
                    The.WeaponIcons.Armageddon
                );
            }
        }

        protected override void OnShoot () {}

    }

}
