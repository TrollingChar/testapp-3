using System.Collections.Generic;
using Utils.Singleton;


namespace War.Teams {

    public class TeamManager {

        public TeamManager (Dictionary<int, Team> teams) {
            The<TeamManager>.Set(this);
            Teams = teams;
        }


        public Dictionary<int, Team> Teams { get; private set; }

    }

}
