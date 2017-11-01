using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Utils.Random;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.UltraRifle)]
    public class UltraRifleWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.UltraRifle,
                    The.WeaponIcons.UltraRifle
                );
            }
        }


        protected override void OnEquip () {
            Shots = 15;

            var battleAssets = The.BattleAssets;
            
            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
            
            // todo: sprite
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot()
        {
            var direction = (TurnData.XY - Object.Position).Rotated(0.1f * (RNG.Float() - RNG.Float()));
            var collisions = The.World.CastUltraRay(Object.Position, direction);
            foreach (var c in collisions) {
                c.Collider2.Object.GetDamage(1);
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
