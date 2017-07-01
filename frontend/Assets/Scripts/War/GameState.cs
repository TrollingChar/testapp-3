using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public enum GameState {
        BeforeTurn,
        Synchronizing,
        Turn,
        EndingTurn,
        AfterTurn,
        Remove0hp
    }

    public class GameStateController {
        const int turnTime = 999000;
        const int retreatTime = 3000;

        public bool synchronized;

        GameState current, next;
        public GameState currentState { get { return current; } }

        public int activePlayer;

        bool wormFrozen;
        public Worm worm;

        int time;
        public int timer {
            get { return time; }
            set {
                time = value;
                Core.coreEvents.SetTurnTime.Invoke(timerString);
            }
        }
        public string timerString { get { return ((time + 999) / 1000).ToString(); } }

        public GameStateController () {
            current = GameState.AfterTurn;
            Hint("AFT");
            next = GameState.Remove0hp;
            timer = 500;
            wormFrozen = false;
            worm = null;
        }

        public void Update () {
            if ((timer -= 20) <= 0) ChangeState();
        }

        public void Wait (int milliseconds) {
            if (current == GameState.Turn) return;
            if (timer < milliseconds) timer = milliseconds;
        }

        public void StartTurn (int id) {
            activePlayer = id;
            ChangeState();
        }

        void Hint (string text) {
            Core.coreEvents.SetGameTime.Invoke(text);
        }

        void ChangeState () {
            switch (current = next++) {
                case GameState.BeforeTurn:
                    Hint("BEF");
                    // Crates fall from the sky, regeneration works, shields replenish
                    if (RNG.Bool(0)) {
                        // drop crates
                        timer = 500;
                    } else ChangeState();
                    break;
                case GameState.Synchronizing:
                    Hint("SYN");
                    // Game sends pos0 signal and waits until server receives all signals
                    synchronized = true;
                    Core.Synchronize();
                    break;
                case GameState.Turn:
                    Hint(activePlayer == Core.id ? "MY" : "TURN");
                    // Player moves his worm and uses weapon
                    wormFrozen = false;
                    worm = Core.bf.NextWorm();
                    timer = turnTime;
                    break;
                case GameState.EndingTurn:
                    Hint("END");
                    // Player ended his turn, but projectiles still flying
                    wormFrozen = true;
                    synchronized = false;
                    worm = null;
                    //Core.bf.ResetActivePlayer();
                    timer = 500;
                    break;
                case GameState.AfterTurn:
                    Hint("AFT");
                    // Poisoned worms take damage
                    if (RNG.Bool(0)) {
                        // poison damage
                        timer = 500;
                    } else ChangeState();
                    break;
                case GameState.Remove0hp:
                    Hint("REM");
                    next = GameState.BeforeTurn; // no overflow
                    // Worms with 0 HP explode
                    if (RNG.Bool(0)) {
                        // blow them up
                        next = GameState.Remove0hp;
                        timer = 500;
                    } ChangeState();
                    break;
                default:
                    Hint("ERR");
                    break;
            }
        }
    }
}