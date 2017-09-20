namespace Battle.State {

    public partial class GameStateController {

        private readonly int _playerId;
        public int ActivePlayer;

        public bool IsMyTurn {
            get { return ActivePlayer == _playerId && CurrentState == GameState.Turn; }
        }

    }

}
