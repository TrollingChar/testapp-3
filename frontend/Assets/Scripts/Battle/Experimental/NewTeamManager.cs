using System.Collections.Generic;
using System.Linq;
using Battle.Arsenals;
using Battle.Teams;
using Core;
using Utils.Random;


namespace Battle.Experimental {

    public class NewTeamManager {

        public readonly List <Team> Teams = new List <Team> ();
        private         int         _i    = -1;


        public NewTeamManager (List <int> players) {
            var colors = TeamColors.Colors.Take (players.Count).ToList ();
            RNG.Shuffle (colors);
            for (int i = 0; i < players.Count; i++) {
                var team = new Team (players[i], colors[i], new AlphaArsenal ());
                Teams.Add (team);
                if (players[i] == The.PlayerInfo.Id) {
                    MyTeam = team;
                }
            }
        }


        public Team MyTeam     { get; private set; }
        public Team ActiveTeam { get; private set; }


        public bool MyTeamActive {
            get { return MyTeam == ActiveTeam; }
        }


        public void NextTeam () {
            _i++;
            ActiveTeam = Teams[_i % Teams.Count];
        }

    }

}