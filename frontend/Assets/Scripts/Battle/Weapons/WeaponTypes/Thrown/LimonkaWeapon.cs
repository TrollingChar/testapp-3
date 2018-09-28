using Attributes;
using Battle.Camera;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.Limonka)]
    public class LimonkaWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;
        private int _timer = 5;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Limonka,
                    The.WeaponIcons.Limonka,
                    "лимонка"
                );
            }
        }


        protected override void OnEquip () {
            ConstPower = false;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.LimonkaWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object).NewWormGO.UnlockHead ();
            
            SetCamera ();
        }


        private void SetCamera () {
            The.Camera.Controller = new ObjectBoundCameraController (Object, 0.5f);
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
        }


        protected override void OnNumberPress (int n) {
            _timer = n;
        }


        protected override void OnShoot () {
            UseAmmo();
            Object.Spawn(
                new Limonka(_timer),
                Object.Position,
                (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseShotSpeed)
            );
            The.Camera.Controller = new AutomaticCameraController ();
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
