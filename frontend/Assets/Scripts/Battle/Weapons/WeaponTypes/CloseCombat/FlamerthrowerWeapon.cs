using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Flamethrower)]
    public class FlamethrowerWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Flamethrower,
                    The<WeaponIcons>.Get().Flamethrower
                );
            }
        }

        protected override void OnShoot () {}

    }

}
