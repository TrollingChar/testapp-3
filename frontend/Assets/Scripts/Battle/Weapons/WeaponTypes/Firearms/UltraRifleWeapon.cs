using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Utils.Random;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.UltraRifle)]
    public class UltraRifleWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;
        private Animator _animator;
        

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.UltraRifle,
                    The.WeaponIcons.UltraRifle,
                    "ультравинтовка"
                );
            }
        }


        protected override void OnEquip () {
            Shots = 15;
            ShotCooldown.Seconds = 0.15f;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.UltraRifleWeapon,
                GameObject.transform,
                false
            );

            _animator = _sprite.GetComponent<Animator>();
            
            ((Worm) Object)._newWormGO.UnlockHead ();
        }


        protected override void OnUnequip () {
            ((Worm) Object)._newWormGO.LockHead ();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            _animator.SetTrigger("Shoot");
            var direction = (TurnData.XY - Object.Position).Rotated(0.05f * (RNG.Float() - RNG.Float()));
            var collisions = The.World.CastUltraRay(Object.Position, direction);
            foreach (var c in collisions) {
                c.Collider2.Object.TakeDamage(1);
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
