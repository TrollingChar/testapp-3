using Commands.Client;
using Utils.Random;


namespace Battle.State {

    public partial class GameStateController {

        private GameState _next;
        public GameState CurrentState { get; private set; }
        public bool Synchronized; // todo: remove?


        private void ChangeState () {
            switch (CurrentState = _next++) {

                case GameState.BeforeTurn:
                    Hint("BEF");
                    // Crates fall from the sky, regeneration works, shields replenish
                    if (RNG.Bool(0)) {
                        // drop crates
                        Timer = 500;
                    } else {
                        ChangeState();
                    }
                    break;

                case GameState.Synchronizing:
                    Hint("SYN");
                    // Game sends a signal and waits until server receives all signals
                    Synchronized = true;
                    new EndTurnCmd(true).Send();
                    break;

                case GameState.Turn:
                    Hint(ActivePlayer == _playerId ? "MY" : "TURN");
                    // Player moves his worm and uses weapon
                    WormFrozen = false;
                    //Worm = Core.BF.NextWorm();
                    Timer = TurnTime;
                    break;

                case GameState.EndingTurn:
                    Hint("END");
                    // Player ended his turn, but projectiles still flying
                    WormFrozen = true;
                    Synchronized = false;
                    Worm = null;
                    //Core.bf.ResetActivePlayer();
                    Timer = 500;
                    break;

                case GameState.AfterTurn:
                    Hint("AFT");
                    // Poisoned worms take damage
                    if (RNG.Bool(0)) {
                        // poison damage
                        Timer = 500;
                    } else {
                        ChangeState();
                    }
                    break;

                case GameState.Remove0Hp:
                    Hint("REM");
                    _next = GameState.BeforeTurn; // no overflow
                    // Worms with 0 HP explode
                    if (RNG.Bool(0)) {
                        // blow them up
                        _next = GameState.Remove0Hp;
                        Timer = 500;
                    }
                    ChangeState();
                    break;

                default:
                    Hint("ERR");
                    break;
            }
        }

    }

}
