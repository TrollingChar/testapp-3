using System;
using Battle.Weapons;
using CrateLootTable = Utils.LootTable.LootTable<System.Func<Battle.Objects.Other.Crates.Crate>>;

// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable
// todo: разобраться как сборщик мусора работает если объект остался только в делегате


namespace Battle.Objects.Other.Crates {

    public class CrateFactory {
        
        private class Loot : Utils.Tuple.Tuple<Func<Crate>, int> {
            public Loot (Func<Crate> gen, int weight = 1) : base(gen, weight) {}
        }

        
        private readonly CrateLootTable
            _crates,
            _healthCrates,
            _weaponCrates;


        public CrateFactory () {
            _healthCrates = new CrateLootTable(
                new Loot(() => new HealthCrate(15), 80),
                new Loot(() => new HealthCrate(30), 20)
            );
            
            const int inf = -1; 
            _weaponCrates = new CrateLootTable(
                new Loot(() => new WeaponCrate(WeaponId.Bazooka,           inf), 100),
//                new Loot(() => new WeaponCrate(WeaponId.Plasmagun,         inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.HomingMissile,     1),   100),
                new Loot(() => new WeaponCrate(WeaponId.MultiLauncher,     8),   100),
                new Loot(() => new WeaponCrate(WeaponId.Minegun,           inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Cryogun,           2),   100),
                new Loot(() => new WeaponCrate(WeaponId.BirdLauncher,      1),   100),
                   
//                new Loot(() => new WeaponCrate(WeaponId.Grenade,           inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Limonka,           inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Molotov,           3),   100),
                new Loot(() => new WeaponCrate(WeaponId.GasGrenade,        2),   100),
                new Loot(() => new WeaponCrate(WeaponId.ControlledGrenade, 2),   100),
                new Loot(() => new WeaponCrate(WeaponId.PhantomGrenade,    3),   100),
                new Loot(() => new WeaponCrate(WeaponId.HolyGrenade,       1),   100),
                   
//                new Loot(() => new WeaponCrate(WeaponId.MachineGun,        inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Blaster,           inf), 100),
//                new Loot(() => new WeaponCrate(WeaponId.Pistol,            inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.HeatPistol,        3),   100),
                new Loot(() => new WeaponCrate(WeaponId.PoisonArrow,       3),   100),
                new Loot(() => new WeaponCrate(WeaponId.UltraRifle,        2),   100),
                new Loot(() => new WeaponCrate(WeaponId.GsomRaycaster,     1),   10),
                   
                new Loot(() => new WeaponCrate(WeaponId.FirePunch,         inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.ExplosivePunch,    inf), 100),
//                new Loot(() => new WeaponCrate(WeaponId.Finger,            inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Axe,               3),   100),
                new Loot(() => new WeaponCrate(WeaponId.Hammer,            1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Flamethrower,      1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Harpoon,           2),   100),
                   
//                new Loot(() => new WeaponCrate(WeaponId.Landmine,          inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Dynamite,          1),   100),
                new Loot(() => new WeaponCrate(WeaponId.PoisonContainer,   1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Turret,            1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Frog,              1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Mole,              2),   100),
                new Loot(() => new WeaponCrate(WeaponId.Superfrog,         1),   100),
                   
                new Loot(() => new WeaponCrate(WeaponId.Airstrike,         1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Napalm,            1),   100),
                new Loot(() => new WeaponCrate(WeaponId.DropMole,          1),   100),
                new Loot(() => new WeaponCrate(WeaponId.DropFrog,          1),   100),
                new Loot(() => new WeaponCrate(WeaponId.MineStrike,        1),   100),
                new Loot(() => new WeaponCrate(WeaponId.VacuumBomb,        1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Nuke,              1),   10),
                   
                new Loot(() => new WeaponCrate(WeaponId.Erosion,           1),   100),
                new Loot(() => new WeaponCrate(WeaponId.PoisonCloud,       1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Earthquake,        1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Flood,             1),   100),
                new Loot(() => new WeaponCrate(WeaponId.BulletHell,        1),   100),
                new Loot(() => new WeaponCrate(WeaponId.MindControl,       1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Armageddon,        1),   10),
                   
                new Loot(() => new WeaponCrate(WeaponId.Rope,              3),   100),
                new Loot(() => new WeaponCrate(WeaponId.Jetpack,           1),   100),
                new Loot(() => new WeaponCrate(WeaponId.Parachute,         inf), 100),
                new Loot(() => new WeaponCrate(WeaponId.Jumper,            3),   100),
                new Loot(() => new WeaponCrate(WeaponId.Teleport,          2),   100),
                new Loot(() => new WeaponCrate(WeaponId.MassTeleport,      1),   100),
                new Loot(() => new WeaponCrate(WeaponId.WormSelect,        2),   100),
                   
                new Loot(() => new WeaponCrate(WeaponId.Drill,             3),   100),
                new Loot(() => new WeaponCrate(WeaponId.Girder,            2),   100),
                new Loot(() => new WeaponCrate(WeaponId.Magnet,            2),   100),
                new Loot(() => new WeaponCrate(WeaponId.Medikit,           2),   100),
                new Loot(() => new WeaponCrate(WeaponId.Overheal,          1),   100)
            );
            
            _crates = new CrateLootTable(
                new Loot(() => _weaponCrates.GetLoot()(), 20),
                new Loot(() => _healthCrates.GetLoot()(), 10),
                new Loot(() => null, 70)
            );
        }
        

        public Crate GenCrate () {
            return _crates.GetLoot()();
        }

    }

}
