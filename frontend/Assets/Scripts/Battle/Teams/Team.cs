using System.Collections.Generic;
using Battle.Arsenals;
using Battle.Objects;
using UnityEngine;


namespace Battle.Teams {

    public class Team {

        private int _player;
        private readonly LinkedList<Worm> _worms;
        private Worm _activeWorm;
        private int _wormsAlive;
        public Arsenal Arsenal { get; private set; }


        public Team (int player, Color color, Arsenal arsenal) {
            _player = player;
            Color = color;
            _worms = new LinkedList<Worm>();
            Arsenal = arsenal;
        }


        public Color Color { get; private set; }


        public void AddWorm (Worm worm) {
            worm.Team = this;
            _worms.AddLast(worm); // if going to add them at mid-fight, refactor
            worm.OnAddToTeam(this);
        }


//        public void RemoveWorm (Worm worm);


        public Worm NextWorm () {
            do {
                if (_worms.Count <= 0) return null;
                _activeWorm = _worms.First.Value;
                _worms.RemoveFirst();
            } while (_activeWorm.HP <= 0);
            _worms.AddLast(_activeWorm);
            return _activeWorm;
        }

    }

}
