using Assets;
using Utils.Singleton;


namespace War.Weapons {

    public class GrenadeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get { return new WeaponDescriptor(2, The<WeaponIcons>.Get().Grenade); }
        }

    }

}
