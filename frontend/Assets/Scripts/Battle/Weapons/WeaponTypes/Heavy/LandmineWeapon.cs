using Attributes;
using Battle.Objects.Projectiles;
using Core;
using Geometry;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Landmine)]
    public class LandmineWeapon : StandardWeapon {
        
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Landmine,
                    The.WeaponIcons.Landmine
                );
            }
        }


        protected override void OnEquip () {
            _sprite = UnityEngine.Object.Instantiate(
                The.BattleAssets.LandmineWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnShoot () {
            UseAmmo();
            float dx = TurnData.XY.X - Object.Position.X;
            Object.Spawn(new Landmine(), Object.Position, new XY(dx < 0 ? -1 : 1, 3));
        }


        protected override void OnUpdate () {
            float dx = TurnData.XY.X - Object.Position.X;
            var tr = _sprite.transform;
            if (!(dx < 0 ^ tr.localScale.x < 0)) return;
            var scale = tr.localScale;
            scale.x = -tr.localScale.x;
            tr.localScale = scale;
        }

    }

}
