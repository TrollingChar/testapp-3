using Assets;
using Attributes;
using Battle.Objects;
using Battle.Objects.Projectiles;
using Battle.Weapons.Crosshairs;
using Geometry;
using UnityEngine;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Bazooka)]
    public class BazookaWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Bazooka,
                    The<WeaponIcons>.Get().Bazooka
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
                The<BattleAssets>.Get().BazookaWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnShoot () {
            var world = The<World>.Get();
//            world.AddObject(new BazookaShell(), Object.Position, (TurnData.XY - Object.Position).WithLength(Power * 0.6f));
//            world.AddObject(new BazookaShell(), Object.Position, (TurnData.XY - Object.Position).WithLength(Power * 0.6f));
            world.AddObject(new BazookaShell(), Object.Position, (TurnData.XY - Object.Position).WithLength(Power * 0.6f));
//            world.AddObject(new BazookaShell(), Object.Position, (TurnData.XY - Object.Position).WithLength(Power * 0.6f));
//            world.AddObject(new BazookaShell(), Object.Position, (TurnData.XY - Object.Position).WithLength(Power * 0.6f));
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }


        private void UpdateAimedWeapon (GameObject sprite) {
            var xy = TurnData.XY - Object.Position;
            if (xy == XY.Zero) xy = XY.Up;
            sprite.transform.localRotation = Quaternion.Euler(0, 0, xy.Angle * Mathf.Rad2Deg);

            float angle = xy.Angle * Mathf.Rad2Deg;
            bool deltaTooBig = Mathf.Abs(Mathf.DeltaAngle(0, angle)) > 90;
            var scale = sprite.transform.localScale;
            scale.x *= scale.x > 0 ^ deltaTooBig ? 1f : -1f;
            sprite.transform.localScale = scale;
            sprite.transform.localEulerAngles = new Vector3(0, 0, angle + (deltaTooBig ? 180 : 0));
        }

    }

}
