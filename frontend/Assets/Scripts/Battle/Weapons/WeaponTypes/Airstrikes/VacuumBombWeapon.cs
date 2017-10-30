using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Airstrikes
{
    [Weapon(WeaponId.VacuumBomb)]
    public class VacuumBombWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.VacuumBomb,
                    The<WeaponIcons>.Get().VacuumBomb
                );
            }
        }


        protected override void OnEquip () {
//            CrossHair = new AirstrikeCrosshair();
        }


        protected override void OnShoot () {}

    }
}