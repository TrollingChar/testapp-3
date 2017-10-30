using Assets;
using Attributes;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using UnityEngine;
using Utils.Singleton;

namespace Battle.Weapons.WeaponTypes.Thrown
{
    [Weapon(WeaponId.PhantomGrenade)]
    public class PhantomGrenadeWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;
        private int _timer = 1;
        
        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.PhantomGrenade,
                    The<WeaponIcons>.Get().PhantomGrenade
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
                The<BattleAssets>.Get().PhantomGrenadeWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnNumberPress (int n) {
            _timer = n;
        }


        protected override void OnShoot () {
            Object.Spawn(
                new PhantomGrenade(_timer),
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