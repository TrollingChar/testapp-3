using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.UltraRifle)]
    public class UltraRifleWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.UltraRifle,
                    The<WeaponIcons>.Get().UltraRifle
                );
            }
        }

        protected override void OnEquip()
        {
            Shots = 15;
            CrossHair = new LineCrosshair();
        }

        protected override void OnBeginAttack()
        {
            UseAmmo();
        }

        protected override void OnShoot()
        {
        }
    }

}
