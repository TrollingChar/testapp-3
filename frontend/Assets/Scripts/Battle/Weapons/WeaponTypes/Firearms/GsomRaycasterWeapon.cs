using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


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
            ShotCooldown.Seconds = 0.2f;

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


        protected override void OnShoot () {
            var direction = TurnData.XY - Object.Position;
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            // todo: make green blasts, annihilate everything, heal allies...
            var world = The.World;
            var blastXY = Object.Position + collision.Offset;
            world.DealDamage(9999, blastXY, 160f);
            world.DestroyTerrain(blastXY, 130f);
            world.SendBlastWave(16f, blastXY, 160f);
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
