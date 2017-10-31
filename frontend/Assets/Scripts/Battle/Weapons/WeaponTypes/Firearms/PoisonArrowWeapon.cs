using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.PoisonArrow)]
    public class PoisonArrowWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PoisonArrow,
                    The.WeaponIcons.PoisonArrow
                );
            }
        }


        protected override void OnEquip ()
        {
            Attacks = 2;

            var battleAssets = The.BattleAssets;
            
            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
            
            // todo: sprite
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot() {
            // todo
            The.World.CastRay(Object.Position, TurnData.XY - Object.Position);
            Debug.Log("...");
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
