using System;
using Assets;
using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Geometry;
using UnityEngine;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.MultiLauncher)]
    public class MultiLauncherWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.MultiLauncher,
                    The<WeaponIcons>.Get().MultiLauncher
                );
            }
        }


        protected override void OnEquip () {
            ConstPower = false;
            Shots = Math.Min(5, GetAmmo());
            Shots = 15;

            _crosshair = UnityEngine.Object.Instantiate(
                The<BattleAssets>.Get().LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                The<BattleAssets>.Get().BazookaWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnNumberPress (int n) {
            Attacks = Math.Min(n, GetAmmo());
        }


        protected override void OnShoot () {
            Object.Spawn(
                new MultiLauncherShell(),
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
