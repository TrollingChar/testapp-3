using Attributes;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.OtherUtils {

    [Weapon(WeaponId.Medikit)]
    public class MedikitWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Medikit,
                    The.WeaponIcons.Medikit,
                    "лечилка"
                );
            }
        }


        protected override void OnEquip () {
            var battleAssets = The.BattleAssets;

            _crosshair = UnityEngine.Object.Instantiate(
                battleAssets.MeleeCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();

            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.MedikitWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnShoot () {
            UseAmmo();
            var direction = (TurnData.XY - Object.Position).Normalized();

            var objects = The.World.CastMelee(Object.Position, direction * 30f);
            
            foreach (var o in objects) {
                if (o == Object) continue;
                o.TakeHealing(15);
                o.CureAllPoison();
            }
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
