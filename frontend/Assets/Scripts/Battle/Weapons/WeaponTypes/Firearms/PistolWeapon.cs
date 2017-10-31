using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Pistol)]
    public class PistolWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Pistol,
                    The.WeaponIcons.Pistol
                );
            }
        }


        protected override void OnEquip () {
            Shots = 5;
            ShotCooldown = 30;

            var battleAssets = The.BattleAssets;
            
            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
            
            // todo: sprite
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot() {
            // todo
            // создать снаряд и сразу же вызвать у него Update
            // или написать функцию World.CastRay
            // второй вариант предпочтительнее так как есть еще ультравинтовка лучи которой проходят через все
            Debug.Log("bang");
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
