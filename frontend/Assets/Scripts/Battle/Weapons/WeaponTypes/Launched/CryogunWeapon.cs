using Assets;
using Attributes;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Launched
{
    [Weapon(WeaponId.Cryogun)]
    public class CryogunWeapon : StandardWeapon
    {
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Cryogun,
                    The<WeaponIcons>.Get().Cryogun
                );
            }
        }
    }
}