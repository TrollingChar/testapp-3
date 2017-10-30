using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Harpoon)]
    public class HarpoonWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Harpoon,
                    The<WeaponIcons>.Get().Harpoon
                );
            }
        }

        protected override void OnShoot () {}

    }

}
