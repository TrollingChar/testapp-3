using Battle.Weapons;


namespace Battle.Arsenals {

    public class AlphaArsenal : Arsenal {

        public AlphaArsenal () {
            const int inf = -1;

            SetAmmo(WeaponId.Bazooka, inf);
            SetAmmo(WeaponId.Plasmagun, inf);
            SetAmmo(WeaponId.MultiLauncher, 8);

            SetAmmo(WeaponId.Grenade, inf);
            SetAmmo(WeaponId.Limonka, inf);
            SetAmmo(WeaponId.PhantomGrenade, 8);

            SetAmmo(WeaponId.MachineGun, inf);
            SetAmmo(WeaponId.Blaster, 8);
            SetAmmo(WeaponId.Pistol, inf);
            SetAmmo(WeaponId.PoisonArrow, 8);
            SetAmmo(WeaponId.UltraRifle, 2);
            SetAmmo(WeaponId.GsomRaycaster, 1);

            SetAmmo(WeaponId.Landmine, 2);
            SetAmmo(WeaponId.Dynamite, 2);
            
            SetAmmo(WeaponId.Jumper, 8);

            // todo: they must be present in ALL arsenals, init them in base class
            SetAmmo(WeaponId.SkipTurn, inf);
//            SetAmmo(WeaponId.Surrender, inf);
        }

    }

}
