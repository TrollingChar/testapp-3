using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Hammer)]
    public class HammerWeapon : StandardWeapon {

        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Hammer,
                    The.WeaponIcons.Hammer
                );
            }
        }


        protected override void OnEquip () {
            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
        }


        protected override void OnShoot () {
            UseAmmo();
            var direction = TurnData.XY - Object.Position;
            var objects = The.World.CastUltraRay(Object.Position, direction, Worm.HeadRadius * 0.9f, 30f)
                .ConvertAll(c => c.Collider2.Object);

            foreach (var o in objects) {
                o.ReceiveBlastWave(direction.WithLength(20f));
                o.TakeDamage(15);
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
