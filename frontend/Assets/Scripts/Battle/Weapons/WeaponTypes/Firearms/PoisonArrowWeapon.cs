using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.PoisonArrow)]
    public class PoisonArrowWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonArrow,
                    The.WeaponIcons.PoisonArrow,
                    "арбалет"
                );
            }
        }


        protected override void OnEquip () {
            Attacks = 2;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.CrossbowWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object).NewWormGO.UnlockHead ();
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            var direction = TurnData.XY - Object.Position;
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            if (collision.Collider2 == null) {
                // todo: CREATE terrain in shape of arrow
                The.World.DestroyTerrain(Object.Position + collision.Offset, 5f);
            }
            else {
                var target = collision.Collider2.Object;
                target.TakeDamage(3);
                target.AddPoison(3, true);
                target.ReceiveBlastWave(direction.WithLength(3f));
            }

            if (GetAmmo() == 0) Attacks = 0;
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
