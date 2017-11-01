﻿using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.MachineGun)]
    public class MachineGunWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MachineGun,
                    The.WeaponIcons.MachineGun
                );
            }
        }


        protected override void OnEquip () {
            Shots = 30;

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


        protected override void OnShoot() {var direction = TurnData.XY - Object.Position;
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            if (collision.Collider2 == null) {
                The.World.DestroyTerrain(Object.Position + collision.Offset, 5f);
            } else {
                var target = collision.Collider2.Object;
                target.GetDamage(1);
                target.ReceiveBlastWave(direction.WithLength(3f));
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
