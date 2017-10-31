using Assets;
using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Parachute)]
    public class ParachuteWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Parachute,
                    The.WeaponIcons.Parachute
                );
            }
        }


        protected override void OnEquip () {
            Removable = true;
        }

    }

}
