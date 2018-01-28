using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Utils.Random;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.MachineGun)]
    public class MachineGunWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

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

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.MachineGunWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            var direction = (TurnData.XY - Object.Position).Rotated(0.05f * (RNG.Float() - RNG.Float()));
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            if (collision.Collider2 == null) {
                The.World.DestroyTerrain(Object.Position + collision.Offset, 5f);
            }
            else {
                var target = collision.Collider2.Object;
                target.GetDamage(1);
                target.ReceiveBlastWave(direction.WithLength(2f));
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
