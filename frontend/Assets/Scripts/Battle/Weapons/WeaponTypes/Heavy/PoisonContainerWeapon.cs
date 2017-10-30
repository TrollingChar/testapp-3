using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.PoisonContainer)]
    public class PoisonContainerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonContainer,
                    The<WeaponIcons>.Get().PoisonContainer
                );
            }
        }

        protected override void OnShoot () {}

    }

}
