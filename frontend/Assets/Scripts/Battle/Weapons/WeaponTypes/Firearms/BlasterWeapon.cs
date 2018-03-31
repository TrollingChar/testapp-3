using Attributes;
using Battle.Objects.Effects;
using Battle.Weapons.Crosshairs;
using Core;
using UnityEngine;


namespace Battle.Weapons.WeaponTypes.Firearms {

    [Weapon(WeaponId.Blaster)]
    public class BlasterWeapon : StandardWeapon {

        private LineCrosshair _crosshair;
        private GameObject _sprite;

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


            _sprite = UnityEngine.Object.Instantiate(
                battleAssets.BlasterWeapon,
                GameObject.transform,
                false
            );
        }


        protected override void OnBeginAttack () {
            UseAmmo();
        }


        protected override void OnShoot () {
            var direction = TurnData.XY - Object.Position;
            var collision = The.World.CastRay(Object.Position, direction);
            if (collision == null) return;
            
            // todo: refactor explosives
            
            var world = The.World;
            var blastXY = Object.Position + collision.Offset;
            world.DealDamage(15, blastXY, 60f, 15f);
            world.DestroyTerrain(blastXY, 30f);
            world.SendBlastWave(6f, blastXY, 60f);
//            Object.Spawn(new Explosion(10f), Object.Position + direction.WithLength(20f));
//            Object.Spawn(new Explosion(30f), Object.Position + collision.Offset);
            The.World.MakeSmoke(Object.Position + collision.Offset, 30f);

            if (GetAmmo() == 0) Attacks = 0;
        }


        protected override void OnUpdate () {
            UpdateLineCrosshair(_crosshair);
            UpdateAimedWeapon(_sprite);
        }

    }

}
