using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Pistol)]
    public class PistolWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;
        private Animator _animator;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Pistol,
                    The.WeaponIcons.Pistol,
                    "пистолет"
                );
            }
        }


        protected override void OnEquip () {
            Shots = 5;
            ShotCooldown.Seconds = 0.6f;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.PistolWeapon,
                GameObject.transform,
                false
            );

            _animator = _sprite.GetComponent<Animator>();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            _animator.SetTrigger("Shoot");
            var direction = TurnData.XY - Object.Position;
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            if (collision.Collider2 == null) {
                The.World.DestroyTerrain(Object.Position + collision.Offset, 5f);
            }
            else {
                var target = collision.Collider2.Object;
                target.TakeDamage(2);
                target.ReceiveBlastWave(direction.WithLength(4f));
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
