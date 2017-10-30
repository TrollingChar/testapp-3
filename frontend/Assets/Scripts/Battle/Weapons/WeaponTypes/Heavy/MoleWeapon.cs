using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Mole)]
    public class MoleWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Mole,
                    The<WeaponIcons>.Get().Mole
                );
            }
        }

        protected override void OnShoot () {}

    }

}
