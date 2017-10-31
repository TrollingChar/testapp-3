using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Blaster)]
    public class BlasterWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Blaster,
                    The.WeaponIcons.Blaster
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
            Debug.Log("boom");
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
