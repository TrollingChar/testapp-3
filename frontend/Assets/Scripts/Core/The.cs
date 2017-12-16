using Assets;
using Battle;
using Battle.Camera;
using Battle.State;
using Battle.Teams;
using Battle.UI;
using Menu;
using Net;


namespace Core {

    public static class The {

        public static SceneSwitcher SceneSwitcher;
        public static Connection Connection;
        public static PlayerInfo PlayerInfo;

        public static BattleAssets BattleAssets;
        public static WeaponIcons WeaponIcons;

        public static MenuScene MenuScene;
        public static BattleScene BattleScene;

        public static World World;
        public static TimerWrapper TimerWrapper;
        public static ActiveWormWrapper ActiveWorm;
        public static GameStateController GameState;
        public static WeaponWrapper WeaponWrapper;
        public static TeamManager TeamManager;
        public static ArsenalPanel ArsenalPanel;
        public static CameraWrapper Camera;

    }

}
