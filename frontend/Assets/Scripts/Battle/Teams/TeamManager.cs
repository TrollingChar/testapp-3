using System.Collections.Generic;
using Battle.Objects;
using Battle.State;
using Core;
using Utils.Singleton;


namespace Battle.Teams {

    public class TeamManager
    {
        private int _myId;
        public Dictionary<int, Team> Teams { get; private set; }

        public bool IsMyTurn
        {
            get { return activeTeam.Player == _myId; }
        }

        private Team activeTeam;

        public TeamManager (Dictionary<int, Team> teams) {
            The<TeamManager>.Set(this);
            _myId = The<PlayerInfo>.Get().Id;
            Teams = teams;
        }

        public Worm NextWorm()
        {
            return activeTeam.NextWorm();
        }

        public void SetActive(int playerId)
        {
            activeTeam = Teams[playerId];
        }
    }

}
