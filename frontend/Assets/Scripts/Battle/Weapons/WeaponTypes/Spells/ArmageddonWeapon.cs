using Attributes;
using Battle.Objects;
using Battle.Objects.Timers;
using Core;
using Geometry;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.Armageddon)]
    public class ArmageddonWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Armageddon,
                    The.WeaponIcons.Armageddon,
                    "армагеддон"
                );
            }
        }


        protected override void OnShoot () {
            var effector = new Effector();
            The.World.Spawn(effector, XY.Zero);
            effector.Timer = new ArmageddonTimer();
        }

    }

}
