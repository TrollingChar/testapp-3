public class WeaponFactory {
    public WeaponFactory(params Func<Weapon>[] weapons) {
        // todo init
    }
    public Weapon this [int index] {
        get {
            Weapon weapon; // todo get from func
            weapon.Id = index;
            return weapon;
        }
    }
}
