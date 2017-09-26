using System.Collections.Generic;
using Battle.Objects;
using Core;
using Utils.Singleton;


namespace Battle.Teams {

    public class TeamManager {

        private readonly int _myId;

        private Team activeTeam;


        public TeamManager (Dictionary<int, Team> teams) {
            The<TeamManager>.Set(this);
            _myId = The<PlayerInfo>.Get().Id;
            Teams = teams;
        }


        public Dictionary<int, Team> Teams { get; private set; }

        public bool IsMyTurn {
            get { return activeTeam.Player == _myId; }
        }


        public Worm NextWorm () {
            return activeTeam.NextWorm();
        }


        public void SetActive (int playerId) {
            activeTeam = Teams[playerId];
        }

    }

}
