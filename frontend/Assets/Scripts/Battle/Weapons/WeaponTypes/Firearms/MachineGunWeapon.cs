using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Utils.Random;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.MachineGun)]
    public class MachineGunWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;
        private Animator _animator;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MachineGun,
                    The.WeaponIcons.MachineGun,
                    "автомат"
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

            _animator = _sprite.GetComponent<Animator>();
            
            ((Worm) Object).NewWormGO.UnlockHead ();
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            _animator.SetTrigger("Shoot");
            var direction = (TurnData.XY - Object.Position).Rotated(0.05f * (RNG.Float() - RNG.Float()));
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            if (collision.Collider2 == null) {
                The.World.DestroyTerrain(Object.Position + collision.Offset, 5f);
            }
            else {
                var target = collision.Collider2.Object;
                target.TakeDamage(1);
                target.ReceiveBlastWave(direction.WithLength(3f));
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
