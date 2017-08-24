using System.Collections.Generic;
using Utils.Singleton;


namespace Battle.Teams {

    public class TeamManager {

        public Dictionary<int, Team> Teams { get; private set; }


        public TeamManager (Dictionary<int, Team> teams) {
            The<TeamManager>.Set(this);
            Teams = teams;
        }

    }

}
