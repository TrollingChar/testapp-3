using Assets;
using Attributes;
using Battle.Weapons.Crosshairs;
using UnityEngine;
using Utils.Singleton;


namespace Battle.Weapons.WeaponTypes.Launched {

    [Weapon(WeaponId.Bazooka)]
    public class BazookaWeapon : StandardWeapon {

        public static WeaponDescriptor Descriptor {
            get {
                return new WeaponDescriptor(
                    WeaponId.Bazooka,
                    The<WeaponIcons>.Get().Bazooka
                );
            }
        }


        protected override void OnEquip () {
            Debug.Log("Bazooka selected");
            ConstPower = false;
//            CrossHair = new LineCrosshair();
        }

        protected override void OnBeginAttack()
        {
            Debug.Log("begin attack");
        }

        protected override void OnEndAttack()
        {
            Debug.Log("end attack");
        }

        protected override void OnUnequip()
        {
            Debug.Log("unequip");
        }

        protected override void OnShoot () {
            Debug.Log("shoot!");
        }

        protected override void OnLastAttack()
        {
            base.OnLastAttack();
            Debug.Log("last attack");
        }
    }

}
