using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.MindControl)]
    public class MindControlWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MindControl,
                    The.WeaponIcons.MindControl
                );
            }
        }

        protected override void OnShoot () {}

    }

}
