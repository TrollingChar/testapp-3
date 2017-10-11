using Assets;
using Attributes;
using Battle.Objects;
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
            Debug.Log("Bazooka selected");
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

        protected override void OnBeginAttack()
        {
            Debug.Log("begin attack");
        }

        protected override void OnEndAttack()
        {
            Debug.Log("end attack");
        }

        protected override void OnUnequip()
        {
            Debug.Log("unequip");
        }

        protected override void OnShoot () {
            Debug.Log("shoot!");
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
            bool tooBigDelta = Mathf.Abs(Mathf.DeltaAngle(0, angle)) > 90;
            foreach (var renderer in sprite.GetComponentsInChildren<SpriteRenderer>()) {
                renderer.flipX = tooBigDelta;
            }
            sprite.transform.localEulerAngles = new Vector3(0, 0, angle + (tooBigDelta ? 180 : 0));
            
        }


        protected override void OnLastAttack()
        {
            base.OnLastAttack();
            Debug.Log("last attack");
        }
    }

}
