using System.Collections.Generic;
using System.Reflection;
using War.Objects;


namespace War.Weapons {

    public class WeaponFactory {

        private List<WeaponDescriptor> _descriptors = new List<WeaponDescriptor>();

        public WeaponFactory () {
//            foreach (var type in Assembly.GetExecutingAssembly().GetTypes()) {
//                if (!type.IsSubclassOf(typeof(Weapon))) continue;
//                var attr = type.GetCustomAttribute<IdAttribute>();
//                if (attr == null) continue;
//            }
        }


        public void RegisterWeapon<T> () {
            
        }

        
        public Weapon CreateWeapon (int id, Worm worm) {
            
        }

    }

}
