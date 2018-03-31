using Attributes;
using Battle.Objects;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Axe)]
    public class AxeWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Axe,
                    The.WeaponIcons.Axe
                );
            }
        }


        protected override void OnShoot () {
            UseAmmo();
            var direction = TurnData.XY - Object.Position;
            var objects = The.World.CastUltraRay(Object.Position, direction, Worm.HeadRadius * 0.9f, 30f)
                .ConvertAll(c => c.Collider2.Object);

            foreach (var o in objects) {
                o.ReceiveBlastWave(direction.WithLength(3f));
                var w = o as Worm;
                // todo: можно сделать TakeAxeDamage(0.5, 10, 50) наверное
                if (w == null) o.TakeDamage(10);
                else           w.TakeDamage(Mathf.Clamp(Mathf.CeilToInt(w.HP * 0.5f), 10, 60));
            }
        }

    }

}
