using Attributes;
using Battle.Weapons.Crosshairs;
using Core;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Blaster)]
    public class BlasterWeapon : StandardWeapon {

        private LineCrosshair _crosshair;

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Blaster,
                    The.WeaponIcons.Blaster
                );
            }
        }


        protected override void OnEquip () {
            Attacks = 2;

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
            if (collision.IsEmpty) return;
            // todo: refactor explosives
            var world = The.World;
            var blastXY = Object.Position + collision.Offset;
            world.DealDamage(15, blastXY, 60f);
            world.DestroyTerrain(blastXY, 30f);
            world.SendBlastWave(6f, blastXY, 60f);
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
//            UpdateAimedWeapon(_sprite);
        }

    }

}
