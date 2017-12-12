using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Rope)]
    public class RopeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Rope,
                    The.WeaponIcons.Rope
                );
            }
        }


        protected override void OnEquip () {
            // todo: handle all these rope mechanics
            Removable = true;
//            CrossHair = new LineCrosshair();
        }

    }

}
