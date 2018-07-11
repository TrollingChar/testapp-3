using Attributes;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;
using Time = Core.Time;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Bazooka)]
    public class BazookaWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Bazooka,
                    The.WeaponIcons.Bazooka,
                    "базука"
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
                battleAssets.BazookaWeapon,
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
            Object.Spawn(
                new BazookaShell(),
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
