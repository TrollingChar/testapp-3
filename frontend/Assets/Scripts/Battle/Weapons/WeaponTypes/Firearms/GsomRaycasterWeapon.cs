using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.GsomRaycaster)]
    public class GsomRaycasterWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.GsomRaycaster,
                    The.WeaponIcons.GsomRaycaster
                );
            }
        }


        protected override void OnEquip () {
            Shots = 10;
//            CrossHair = new LineCrosshair();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {}

    }

}
