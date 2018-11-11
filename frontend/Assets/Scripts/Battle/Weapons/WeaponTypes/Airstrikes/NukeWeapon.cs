using Attributes;
using Battle.Objects.Effects;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Airstrikes {

    [Weapon (WeaponId.Nuke)]
    public class NukeWeapon : StandardWeapon {

        private GameObject _crosshair;


        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor (
                    WeaponId.Nuke,
                    The.WeaponIcons.Nuke,
                    "ядерная ракета"
                );
            }
        }


        protected override void OnEquip () {
            _crosshair = UnityEngine.Object.Instantiate (The.BattleAssets.PointCrosshair);
        }


        protected override void OnUnequip () {
            UnityEngine.Object.Destroy (_crosshair);
        }


        protected override void OnShoot () {
            UseAmmo ();
            
            The.Battle.World.Spawn (new NukeTarget (), TurnData.XY);
            
            // todo: сделать объект который сначала задает альфу, потом убирает
            var alert = The.Battle.Alert;
            alert.Text = "ОПАСНО!!";
            alert.Alpha = 1;
        }

    }

}