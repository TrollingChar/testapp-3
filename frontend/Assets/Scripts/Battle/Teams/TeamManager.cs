using System.Collections.Generic;
using Battle.Objects;
using Core;


namespace Battle.Teams {

    public class TeamManager {

        private readonly int _myId;

        public Dictionary<int, Team> Teams { get; private set; }
        private Team activeTeam;

        public Team MyTeam { get; private set; }

        public TeamManager (Dictionary<int, Team> teams) {
            The.TeamManager = this;
            _myId = The.PlayerInfo.Id;
            Teams = teams;
            MyTeam = teams[_myId];
        }


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
