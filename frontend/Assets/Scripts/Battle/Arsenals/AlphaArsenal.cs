using Battle.Weapons;


namespace Battle.Arsenals {

    public class AlphaArsenal : Arsenal {

        public AlphaArsenal () {
            const int inf = -1;

            SetAmmo(WeaponId.Bazooka, 6);
            SetAmmo(WeaponId.Plasmagun, inf);
            SetAmmo(WeaponId.MultiLauncher, 12);
            SetAmmo(WeaponId.HomingMissile, 1);
            SetAmmo(WeaponId.Minegun, 3);

            SetAmmo(WeaponId.Grenade, inf);
            SetAmmo(WeaponId.Limonka, inf);
            SetAmmo(WeaponId.GasGrenade, inf);
            SetAmmo(WeaponId.PhantomGrenade, 3);

            SetAmmo(WeaponId.MachineGun, inf);
            SetAmmo(WeaponId.Blaster, 6);
            SetAmmo(WeaponId.Pistol, inf);
            SetAmmo(WeaponId.PoisonArrow, 3);
            SetAmmo(WeaponId.UltraRifle, 2);
            SetAmmo(WeaponId.GsomRaycaster, 1); // !!!
            
            SetAmmo(WeaponId.FirePunch, inf);
            SetAmmo(WeaponId.Finger, inf);
            SetAmmo(WeaponId.Axe, 2);
            SetAmmo(WeaponId.Hammer, 1);

            SetAmmo(WeaponId.Landmine, inf);
            SetAmmo(WeaponId.Dynamite, 1);
            
            SetAmmo(WeaponId.Erosion, 1);
            SetAmmo(WeaponId.PoisonCloud, inf);
            SetAmmo(WeaponId.BulletHell, inf);
            SetAmmo(WeaponId.Armageddon, inf);
            
            SetAmmo(WeaponId.Jumper, 6);
            
            SetAmmo(WeaponId.Medikit, 2);
            SetAmmo(WeaponId.Overheal, 1);

            // todo: they must be present in ALL arsenals, init them in base class
            SetAmmo(WeaponId.SkipTurn, inf);
//            SetAmmo(WeaponId.Surrender, inf);
        }

    }

}
