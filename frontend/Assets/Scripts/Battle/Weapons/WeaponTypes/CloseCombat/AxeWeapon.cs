using Attributes;
using Battle.Objects;
using Battle.Weapons.Crosshairs;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.CloseCombat {

    [Weapon(WeaponId.Axe)]
    public class AxeWeapon : StandardWeapon {

        private GameObject _sprite;
        private LineCrosshair _crosshair;
        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Axe,
                    The.WeaponIcons.Axe,
                    "топор"
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
                battleAssets.AxeWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnShoot () {
            UseAmmo();
            var direction = (TurnData.XY - Object.Position).Normalized();

            var objects = The.World.CastMelee(Object.Position, direction * 30f);
            
            foreach (var o in objects) {
                if (o == Object) continue;
                o.ReceiveBlastWave(direction * 3f + new XY(0f, 2f));
                o.TakeAxeDamage(0.5f, 10, 60);
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
