using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Axe)]
    public class AxeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Axe,
                    The<WeaponIcons>.Get().Axe
                );
            }
        }

        protected override void OnShoot () {}

    }

}
