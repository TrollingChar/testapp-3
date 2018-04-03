using Battle.Weapons;


namespace Battle.Arsenals {

    public class AlphaArsenal : Arsenal {

        public AlphaArsenal () {
            const int inf = -1;

            SetAmmo(WeaponId.Bazooka, 6);
            SetAmmo(WeaponId.Plasmagun, inf);
            SetAmmo(WeaponId.MultiLauncher, 12);
            SetAmmo(WeaponId.HomingMissile, 1);
            SetAmmo(WeaponId.Minegun, 2);

            SetAmmo(WeaponId.Grenade, inf);
            SetAmmo(WeaponId.Limonka, inf);
            SetAmmo(WeaponId.PhantomGrenade, 4);

            SetAmmo(WeaponId.MachineGun, inf);
            SetAmmo(WeaponId.Blaster, 6);
            SetAmmo(WeaponId.Pistol, inf);
            SetAmmo(WeaponId.PoisonArrow, 3);
            SetAmmo(WeaponId.UltraRifle, 2);
            SetAmmo(WeaponId.GsomRaycaster, 1);
            
            SetAmmo(WeaponId.FirePunch, inf);
            SetAmmo(WeaponId.Finger, inf);
            SetAmmo(WeaponId.Axe, 2);
            SetAmmo(WeaponId.Hammer, 2);

            SetAmmo(WeaponId.Landmine, inf);
            SetAmmo(WeaponId.Dynamite, 1);
            
            SetAmmo(WeaponId.Jumper, 4);

            // todo: they must be present in ALL arsenals, init them in base class
            SetAmmo(WeaponId.SkipTurn, inf);
//            SetAmmo(WeaponId.Surrender, inf);
        }

    }

}
