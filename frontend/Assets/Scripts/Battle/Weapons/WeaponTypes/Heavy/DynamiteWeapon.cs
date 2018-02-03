using Assets;
using Attributes;
using Battle.Objects.Projectiles;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Heavy {

    [Weapon(WeaponId.Dynamite)]
    public class DynamiteWeapon : StandardWeapon {
        
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Dynamite,
                    The.WeaponIcons.Dynamite
                );
            }
        }


        protected override void OnEquip () {
            _sprite = UnityEngine.Object.Instantiate(
                The.BattleAssets.DynamiteWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnShoot () {
            UseAmmo();
            Object.Spawn(new Dynamite(), Object.Position);
        }


        protected override void OnUpdate () {
//            UpdateAimedWeapon(_sprite);
        }

    }

}
