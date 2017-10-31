using Assets;
using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using UnityEngine;
using Core;


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
                    The.WeaponIcons.Limonka
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
        }


        protected override void OnNumberPress (int n) {
            _timer = n;
        }


        protected override void OnShoot () {
            Object.Spawn(
                new Limonka(_timer),
                Object.Position,
                (TurnData.XY - Object.Position).WithLength(Power * 0.6f)
            );
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
