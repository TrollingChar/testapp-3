using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Jumper)]
    public class JumperWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jumper,
                    The<WeaponIcons>.Get().Jumper
                );
            }
        }


        protected override void OnEquip () {
            Removable = true;
            CrossHair = new LineCrosshair();
        }

    }

}
