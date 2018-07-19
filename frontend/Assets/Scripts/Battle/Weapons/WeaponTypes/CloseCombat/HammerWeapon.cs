using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Hammer)]
    public class HammerWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Hammer,
                    The.WeaponIcons.Hammer,
                    "молот"
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
                battleAssets.HammerWeapon,
                GameObject.transform,
                false
            );
            
            ((Worm) Object).NewWormGO.UnlockHead ();
        }


        protected override void OnUnequip () {
            ((Worm) Object).NewWormGO.LockHead ();
        }


        protected override void OnShoot () {
            UseAmmo();
            var direction = (TurnData.XY - Object.Position).Normalized();

            var objects = The.World.CastMelee(Object.Position, direction * 30f);
            
            foreach (var o in objects) {
                if (o == Object) continue;
                o.ReceiveBlastWave(direction * 20f);
                o.TakeDamage(15);
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
