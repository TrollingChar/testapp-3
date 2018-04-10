using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.FirePunch)]
    public class FirePunchWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.FirePunch,
                    The.WeaponIcons.FirePunch,
                    "огненный удар"
                );
            }
        }

        protected override void OnShoot () {}

    }

}
