using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;

namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.GsomRaycaster)]
    public class GsomRaycasterWeapon : StandardWeapon {
        
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.GsomRaycaster,
                    The.WeaponIcons.GsomRaycaster
                );
            }
        }


        protected override void OnEquip () {
            Shots = 15;

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
            The.World.CastRay(Object.Position, TurnData.XY - Object.Position);
            The.World.CastRay(Object.Position, TurnData.XY - Object.Position);
            Debug.Log("GSOMGSOM!!1");
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
