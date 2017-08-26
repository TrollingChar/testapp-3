using Battle.Weapons;


namespace Battle.Arsenals {

    public class StandardArsenal : Arsenal {

        public StandardArsenal () {
            const int inf = -1; 
            
            SetAmmo(WeaponId.Bazooka, 5);
            SetAmmo(WeaponId.Plasmagun, inf);
            SetAmmo(WeaponId.HomingMissile, 1);
            SetAmmo(WeaponId.MultiLauncher, 3);
            
            SetAmmo(WeaponId.Grenade, inf);
            SetAmmo(WeaponId.Limonka, 2);
            SetAmmo(WeaponId.Molotov, 2);
            SetAmmo(WeaponId.GasGrenade, 1);
            SetAmmo(WeaponId.ControlledGrenade, 1);
            SetAmmo(WeaponId.PhantomGrenade, 2);
            
            SetAmmo(WeaponId.MachineGun, inf);
            SetAmmo(WeaponId.Blaster, 5);
            SetAmmo(WeaponId.Pistol, inf);
            SetAmmo(WeaponId.HeatPistol, 2);
            SetAmmo(WeaponId.PoisonArrow, 2);
            SetAmmo(WeaponId.UltraRifle, 1);
            
            SetAmmo(WeaponId.FirePunch, inf);
            SetAmmo(WeaponId.ExplosivePunch, 2);
            SetAmmo(WeaponId.Finger, inf); // todo: init in base class
            SetAmmo(WeaponId.Axe, 2);
            SetAmmo(WeaponId.Hammer, 1);
            SetAmmo(WeaponId.Harpoon, 1);
            
            SetAmmo(WeaponId.Landmine, 2);
            SetAmmo(WeaponId.Dynamite, 1);
            SetAmmo(WeaponId.Frog, 1);
            SetAmmo(WeaponId.Mole, 1);
            
            SetAmmo(WeaponId.Airstrike, 1);
            SetAmmo(WeaponId.Napalm, 1);
            SetAmmo(WeaponId.DropMole, 1);
            
            SetAmmo(WeaponId.Rope, 5);
            SetAmmo(WeaponId.Jetpack, 1);
            SetAmmo(WeaponId.Parachute, 3);
            SetAmmo(WeaponId.Jumper, 3);
            SetAmmo(WeaponId.Teleport, 2);
            SetAmmo(WeaponId.WormSelect, 3);
            
            SetAmmo(WeaponId.Drill, 5);
            SetAmmo(WeaponId.Girder, 3);
            SetAmmo(WeaponId.Magnet, 2);
            SetAmmo(WeaponId.Medikit, 1);
            
            // todo: they must be present in ALL arsenals, init them in base class
            SetAmmo(WeaponId.SkipTurn, inf);
            SetAmmo(WeaponId.Surrender, inf);
        }

    }

}
