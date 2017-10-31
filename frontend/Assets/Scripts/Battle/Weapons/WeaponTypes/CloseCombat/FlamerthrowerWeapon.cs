using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Flamethrower)]
    public class FlamethrowerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Flamethrower,
                    The.WeaponIcons.Flamethrower
                );
            }
        }

        protected override void OnShoot () {}

    }

}
