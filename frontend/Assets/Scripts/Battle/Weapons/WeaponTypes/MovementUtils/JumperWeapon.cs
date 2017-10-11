using Assets;
using Attributes;
using Battle.Objects.Controllers;
using Battle.Weapons.Crosshairs;
using Geometry;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.MovementUtils {

    [Weapon(WeaponId.Jumper)]
    public class JumperWeapon : StandardWeapon {
        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Jumper,
                    The<WeaponIcons>.Get().Jumper
                );
            }
        }


        protected override void OnEquip () {
            Removable = true; // todo handle ammo
            ConstPower = false;

            _crosshair = UnityEngine.Object.Instantiate(
                The<BattleAssets>.Get().LineCrosshair,
                GameObject.transform,
                false
            ).GetComponent<LineCrosshair>();
        }

        protected override void OnUpdate() {
            UpdateLineCrosshair(_crosshair);
        }

        protected override void OnShoot() {
            Object.Controller = new WormControllerJump();
            Object.Velocity = (TurnData.XY - Object.Position).WithLength(Power * 0.4f);
        }
    }

}
