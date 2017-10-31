using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Turret)]
    public class TurretWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Turret,
                    The.WeaponIcons.Turret
                );
            }
        }

        protected override void OnShoot () {}

    }

}
