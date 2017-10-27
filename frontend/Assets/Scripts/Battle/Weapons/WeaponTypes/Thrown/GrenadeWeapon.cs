using System;
using Assets;
using Attributes;
using Battle.Objects.Objects;
using Battle.Weapons.Crosshairs;
using UnityEngine;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Thrown {

    [Weapon(WeaponId.Grenade)]
    public class GrenadeWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Grenade,
                    The<WeaponIcons>.Get().Grenade
                );
            }
        }


        protected override void OnEquip () {
            ConstPower = false;

            _crosshair = UnityEngine.Object.Instantiate(
                The<BattleAssets>.Get().LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                The<BattleAssets>.Get().GrenadeWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnNumberPress (int n) {
            // todo
        }


        protected override void OnShoot () {
            Object.Spawn(
                new Grenade(),
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
