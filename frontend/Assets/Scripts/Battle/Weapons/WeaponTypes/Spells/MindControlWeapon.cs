using Assets;
using Attributes;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Spells {

    [Weapon(WeaponId.MindControl)]
    public class MindControlWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MindControl,
                    The<WeaponIcons>.Get().MindControl
                );
            }
        }

        protected override void OnShoot () {}

    }

}
