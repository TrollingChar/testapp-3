using System;
using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.MultiLauncher)]
    public class MultiLauncherWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MultiLauncher,
                    The.WeaponIcons.MultiLauncher,
                    "мини-ракеты"
                );
            }
        }


        protected override void OnEquip () {
            ConstPower = false;
            Shots = Math.Min(5, GetAmmo());
//            Shots = 5;
            ShotCooldown.Seconds = 0.2f;

            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.MultiLauncherWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnNumberPress (int n) {
            Attacks = Math.Min(n, GetAmmo());
        }


        protected override void OnShoot () {
            UseAmmo();
            Object.Spawn(
                new MultiLauncherShell(),
                Object.Position,
                (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseShotSpeed)
            );
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
