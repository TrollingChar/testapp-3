using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Teleport)]
    public class TeleportWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Teleport,
                    The.WeaponIcons.Teleport,
                    "телепорт"
                );
            }
        }


        protected override void OnEquip () {
            Removable = true;
//            CrossHair = new PointCrosshair();
        }


        protected override void OnShoot () {}

    }

}
