using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.Limonka)]
    public class LimonkaWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Limonka,
                    The<WeaponIcons>.Get().Limonka
                );
            }
        }

        protected override void OnEquip()
        {
            CrossHair = new LineCrosshair();
        }

        protected override void OnShoot()
        {
        }

    }

}
