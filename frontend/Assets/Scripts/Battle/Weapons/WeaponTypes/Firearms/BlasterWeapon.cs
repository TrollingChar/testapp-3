using Attributes;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Explosives;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Blaster)]
    public class BlasterWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Blaster,
                    The.WeaponIcons.Blaster,
                    "бластер"
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
                battleAssets.BlasterWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object).NewWormGO.UnlockHead ();
            
            SetCamera ();
        }


        private void SetCamera () {
            The.Camera.Controller = new ObjectBoundCameraController (Object, 1f);
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
            
            The.Camera.Controller = new CameraController ();
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            var direction = TurnData.XY - Object.Position;
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision != null) {
                new Explosive15().Detonate(Object.Position + collision.Offset);
            }
            if (GetAmmo() == 0) Attacks = 0;
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            if (Input.anyKeyDown) SetCamera ();
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
