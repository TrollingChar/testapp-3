using System;
using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.MultiLauncher)]
    public class MultiLauncherWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MultiLauncher,
                    The<WeaponIcons>.Get().MultiLauncher
                );
            }
        }

        protected override void OnEquip()
        {
            ConstPower = true;
            Attacks = Math.Min(5, GetAmmo());
            CrossHair = new LineCrosshair();
        }

        protected override void OnNumberPress(int n)
        {
            Attacks = Math.Min(n, GetAmmo());
        }

        protected override void OnShoot()
        {
            
        }
    }

}
