using System.Collections.Generic;
using Battle.Arsenals;
using Battle.Weapons;
using Battle.Weapons.WeaponTypes.Airstrikes;
using Battle.Weapons.WeaponTypes.CloseCombat;
using Battle.Weapons.WeaponTypes.Firearms;
using Battle.Weapons.WeaponTypes.Heavy;
using Battle.Weapons.WeaponTypes.Launched;
using Battle.Weapons.WeaponTypes.MovementUtils;
using Battle.Weapons.WeaponTypes.OtherUtils;
using Battle.Weapons.WeaponTypes.Spells;
using Battle.Weapons.WeaponTypes.Thrown;
using Core.UI;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.UI {

    public class ArsenalPanel : Panel {

        [SerializeField] private GameObject _weaponButtonPrefab;
        private Dictionary<int, WeaponButton> _buttons;
        

        private void Start () {
            _buttons = new Dictionary<int, WeaponButton>();
            
            AddWeapon(BazookaWeapon.Descriptor);
            AddWeapon(PlasmagunWeapon.Descriptor);
            AddWeapon(HomingMissileWeapon.Descriptor);
            AddWeapon(MultiLauncherWeapon.Descriptor);
            AddWeapon(MinegunWeapon.Descriptor);
            AddWeapon(CryogunWeapon.Descriptor);
            AddWeapon(BirdLauncherWeapon.Descriptor);

            AddWeapon(GrenadeWeapon.Descriptor);
            AddWeapon(LimonkaWeapon.Descriptor);
            AddWeapon(MolotovWeapon.Descriptor);
            AddWeapon(GasGrenadeWeapon.Descriptor);
            AddWeapon(ControlledGrenadeWeapon.Descriptor);
            AddWeapon(PhantomGrenadeWeapon.Descriptor);
            AddWeapon(HolyGrenadeWeapon.Descriptor);

            AddWeapon(MachineGunWeapon.Descriptor);
            AddWeapon(BlasterWeapon.Descriptor);
            AddWeapon(PistolWeapon.Descriptor);
            AddWeapon(HeatPistolWeapon.Descriptor);
            AddWeapon(PoisonArrowWeapon.Descriptor);
            AddWeapon(UltraRifleWeapon.Descriptor);
            AddWeapon(GsomRaycasterWeapon.Descriptor);

            AddWeapon(FirePunchWeapon.Descriptor);
            AddWeapon(ExplosivePunchWeapon.Descriptor);
            AddWeapon(FingerWeapon.Descriptor);
            AddWeapon(AxeWeapon.Descriptor);
            AddWeapon(HammerWeapon.Descriptor);
            AddWeapon(FlamethrowerWeapon.Descriptor);
            AddWeapon(HarpoonWeapon.Descriptor);

            AddWeapon(LandmineWeapon.Descriptor);
            AddWeapon(DynamiteWeapon.Descriptor);
            AddWeapon(PoisonContainerWeapon.Descriptor);
            AddWeapon(TurretWeapon.Descriptor);
            AddWeapon(FrogWeapon.Descriptor);
            AddWeapon(MoleWeapon.Descriptor);
            AddWeapon(SuperfrogWeapon.Descriptor);

            AddWeapon(AirstrikeWeapon.Descriptor);
            AddWeapon(NapalmWeapon.Descriptor);
            AddWeapon(DropMoleWeapon.Descriptor);
            AddWeapon(DropFrogWeapon.Descriptor);
            AddWeapon(MineStrikeWeapon.Descriptor);
            AddWeapon(VacuumBombWeapon.Descriptor);
            AddWeapon(NukeWeapon.Descriptor);

            AddWeapon(ErosionWeapon.Descriptor);
            AddWeapon(PoisonCloudWeapon.Descriptor);
            AddWeapon(EarthquakeWeapon.Descriptor);
            AddWeapon(FloodWeapon.Descriptor);
            AddWeapon(BulletHellWeapon.Descriptor);
            AddWeapon(MindControlWeapon.Descriptor);
            AddWeapon(ArmageddonWeapon.Descriptor);

            AddWeapon(RopeWeapon.Descriptor);
            AddWeapon(JetpackWeapon.Descriptor);
            AddWeapon(ParachuteWeapon.Descriptor);
            AddWeapon(JumperWeapon.Descriptor);
            AddWeapon(TeleportWeapon.Descriptor);
            AddWeapon(MassTeleportWeapon.Descriptor);
            AddWeapon(WormSelectWeapon.Descriptor);

            AddWeapon(DrillWeapon.Descriptor);
            AddWeapon(GirderWeapon.Descriptor);
            AddWeapon(MagnetWeapon.Descriptor);
            AddWeapon(MedikitWeapon.Descriptor);
            AddWeapon(OverhealWeapon.Descriptor);
            AddWeapon(SkipTurnWeapon.Descriptor);
            AddWeapon(SurrenderWeapon.Descriptor);

            ApplyLayout();
        }


        private void ApplyLayout () {
            int childCount = transform.childCount;

            int w = (childCount + 6) / 7;
            int h = w > 1 ? 7 : childCount;
            float xOffset = (1 - w) * 55;
            float yOffset = (h - 1) * 55;

            for (int i = 0; i < childCount; i++) {
                var rt = (RectTransform) transform.GetChild(i);

                rt.pivot =
                rt.anchorMin =
                rt.anchorMax = new Vector2(0.5f, 0.5f);

                rt.anchoredPosition = new Vector2(
                    xOffset + i / 7 * 110,
                    yOffset - i % 7 * 110
                );
            }
        }


        private void Update () {
            if (Input.GetKeyDown(KeyCode.Q)) Toggle();
        }


        private void AddWeapon (WeaponDescriptor descriptor) {
            var button = Instantiate(_weaponButtonPrefab, gameObject.transform, false);
            var component = button.GetComponent<WeaponButton>();
            component.Configure(descriptor);
            button.transform.SetAsLastSibling();
            _buttons.Add(component.WeaponId, component);
        }


        private void AddEmpty (int count = 1) {
            for (int i = 0; i < count; i++) {
                var button = Instantiate(_weaponButtonPrefab, gameObject.transform, false);
                button.transform.SetAsLastSibling();
                button.GetComponent<Button>().interactable = false;
            }
        }


        public void Bind (Arsenal arsenal) {
            arsenal.OnAmmoChanged.Subscribe(ChangeAmmo);
            foreach (int id in _buttons.Keys) ChangeAmmo(id, arsenal.GetAmmo(id));
        }


        public void Unbind (Arsenal arsenal) {
            foreach (int id in _buttons.Keys) ChangeAmmo(id, 0);
            arsenal.OnAmmoChanged.Unsubscribe(ChangeAmmo);
        }


        private void ChangeAmmo (int weapon, int ammo) {
            _buttons[weapon].SetAmmo(ammo);
        }

    }

}
