using Attributes;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Minegun)]
    public class MinegunWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Minegun,
                    The.WeaponIcons.Minegun,
                    "миномет"
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
                battleAssets.MinegunWeapon,
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
            Object.Spawn(
                new Landmine(),
                Object.Position,
                (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseShotSpeed)
            );
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
            ((Worm) Object).LookAt (TurnData.XY);
        }

    }

}
