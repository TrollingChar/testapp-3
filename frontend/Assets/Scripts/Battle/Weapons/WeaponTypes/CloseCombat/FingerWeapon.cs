using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Finger)]
    public class FingerWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Finger,
                    The.WeaponIcons.Finger,
                    "толчок"
                );
            }
        }


        protected override void OnEquip () {
            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.MeleeCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.FingerWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object)._newWormGO.UnlockHead ();
        }


        protected override void OnUnequip () {
            ((Worm) Object)._newWormGO.LockHead ();
        }


        protected override void OnShoot () {
            UseAmmo();
            var direction = (TurnData.XY - Object.Position).Normalized();

            var objects = The.World.CastMelee(Object.Position, direction * 20f);
            
            foreach (var o in objects) {
                if (o == Object) continue;
                o.ReceiveBlastWave(direction * 3f + new XY(0f, 2f));
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
