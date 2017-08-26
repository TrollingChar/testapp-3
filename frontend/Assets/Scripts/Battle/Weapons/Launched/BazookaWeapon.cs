using Assets;
using Utils.Singleton;


namespace Battle.Weapons.Launched {

    
    public class BazookaWeapon : StandardWeapon {

        // todo: decsriptors must have unique id and icons. check it somewhere!
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    (int) WeaponId.Bazooka,
                    The<WeaponIcons>.Get().Bazooka
                );
            }
        }

    }

}
