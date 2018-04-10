using Attributes;
using Battle.Objects.Controllers;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Jumper)]
    public class JumperWeapon : StandardWeapon {

        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jumper,
                    The.WeaponIcons.Jumper,
                    "прыгалка"
                );
            }
        }


        protected override void OnEquip () {
            Removable = true;
            ConstPower = false;

            _crosshair = UnityEngine.Object.Instantiate(
                The.BattleAssets.LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
        }


        protected override void OnShoot () {
            UseAmmo();
            Object.Controller = new WormControllerJump();
            Object.Velocity =
                (TurnData.XY - Object.Position).WithLength(Power01 * Balance.BaseJumperSpeed);
        }

    }

}
