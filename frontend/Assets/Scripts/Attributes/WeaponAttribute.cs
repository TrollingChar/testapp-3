using Battle.Weapons;


namespace Attributes {

    public class WeaponAttribute : IdAttribute {

        private readonly byte _id;


        public WeaponAttribute (WeaponId id) {
            _id = (byte) id;
        }


        public override byte Id {
            get { return _id; }
        }

    }

}
