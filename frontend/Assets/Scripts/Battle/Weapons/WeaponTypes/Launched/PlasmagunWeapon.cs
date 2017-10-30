using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Launched
{
    [Weapon(WeaponId.Plasmagun)]
    public class PlasmagunWeapon : StandardWeapon
    {
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Plasmagun,
                    The<WeaponIcons>.Get().Plasmagun
                );
            }
        }
    }
}