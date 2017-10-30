using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Thrown
{
    public class PhantomGrenade
    {
        [Weapon(WeaponId.PhantomGrenade)]
        public class PhantomGrenadeWeapon : StandardWeapon {

            public static WeaponDescriptor Descriptor {
                get {
                    return new WeaponDescriptor(
                        WeaponId.PhantomGrenade,
                        The<WeaponIcons>.Get().PhantomGrenade
                    );
                }
            }


            protected override void OnEquip () {
//            CrossHair = new LineCrosshair();
            }


            protected override void OnShoot () {}

        }
    }
}