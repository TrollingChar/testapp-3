using System.Collections.Generic;
using Battle.Objects;
using Battle.State;
using Utils.Singleton;


namespace Battle.Teams {

    public class TeamManager {

        public Dictionary<int, Team> Teams { get; private set; }
        private Team activeTeam;

        public TeamManager (Dictionary<int, Team> teams) {
            The<TeamManager>.Set(this);
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
