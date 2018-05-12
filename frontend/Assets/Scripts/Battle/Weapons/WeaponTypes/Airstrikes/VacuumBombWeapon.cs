using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon(WeaponId.VacuumBomb)]
    public class VacuumBombWeapon : AbstractAirstrikeWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.VacuumBomb,
                    The.WeaponIcons.VacuumBomb,
                    "вакуумная бомба"
                );
            }
        }


        protected override void OnShoot () {
            UseAmmo();
            The.World.LaunchAirstrike(
                () => new VacuumBomb(),
                Target,
                Direction == PointCrosshair.Direction.Right
            );
        }

    }

}
