using Attributes;
using Core;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Overheal)]
    public class OverhealWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Overheal,
                    The.WeaponIcons.Overheal,
                    "общее исцеление"
                );
            }
        }


        protected override void OnShoot () {
            UseAmmo();
            var objects = The.World.Objects;
            for (int i = 0; i < objects.Count; i++) {
                var o = objects[i];
                if (o.Despawned) continue;
                o.CureAllPoison();
                o.TakeHealing(30);
            }
        }

    }

}
