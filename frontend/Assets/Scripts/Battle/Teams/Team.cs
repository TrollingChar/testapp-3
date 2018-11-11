using System.Collections.Generic;
using Battle.Arsenals;
using Battle.Objects;
using UnityEngine;


namespace Battle.Teams {

    public class Team {

        private readonly Queue <Worm> _worms;
//        private Worm _activeWorm;
        public int WormsAlive;


        public Team (int player, Color color, Arsenal arsenal) {
            Player = player;
            Color = color;
            _worms = new Queue <Worm> ();
            Arsenal = arsenal;
        }


        public int Player { get; private set; }
        public Arsenal Arsenal { get; private set; }


        public Color Color { get; private set; }


        public void AddWorm (Worm worm) {
            if (worm.HP > 0) WormsAlive++;
            worm.Team = this;
            _worms.Enqueue (worm);
            worm.OnAddToTeam(this);
        }


        public Worm ActiveWorm {
            get {
                var worm = _worms.Peek ();
                while (worm.HP <= 0) {
                    _worms.Dequeue ();
                    worm = _worms.Peek ();
                }
                return worm;
            }
        }


        public void NextWorm () {
            _worms.Enqueue (_worms.Dequeue ());
        }

    }

}
