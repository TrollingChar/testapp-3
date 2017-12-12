using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.PoisonContainer)]
    public class PoisonContainerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonContainer,
                    The.WeaponIcons.PoisonContainer
                );
            }
        }

        protected override void OnShoot () {}

    }

}
