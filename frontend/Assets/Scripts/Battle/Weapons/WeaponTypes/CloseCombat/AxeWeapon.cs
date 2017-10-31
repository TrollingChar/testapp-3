using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Axe)]
    public class AxeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Axe,
                    The.WeaponIcons.Axe
                );
            }
        }

        protected override void OnShoot () {}

    }

}
