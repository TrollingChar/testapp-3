using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.code.bf;

enum GameState {
    BeforeTurn,
    Synchronizing,
    Turn,
    EndingTurn,
    AfterTurn,
    Remove0hp
}

class GameStateController {
    Core core;

    const int turnTime = 30000;
    const int retreatTime = 3000;

    public bool synchronized;
    
    GameState current, next;

    bool wormFrozen;
    Worm worm;

    int time;
    public int timer {
        get { return time; }
        set { time = value; }
    }
    public string timerString { get { return ((time + 999) % 1000).ToString(); } }

    public GameStateController (Core core) {
        this.core = core;
        current = GameState.AfterTurn;
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

    public void StartTurn(int id) {
        ChangeState();
    }

    void ChangeState () {
        switch (current = next++) {
            case GameState.BeforeTurn:
                // Crates fall from the sky, regeneration works, shields replenish
                if (RNG.Bool(0)) {
                    // drop crates
                    timer = 500;
                } else ChangeState();
                break;
            case GameState.Synchronizing:
                // Game sends a signal and waits until server receives all signals
                synchronized = true;
                core.Synchronize();
                break;
            case GameState.Turn:
                // Player moves his worm and uses weapon
                wormFrozen = false;
                worm = core.NextWorm();
                timer = turnTime;
                break;
            case GameState.EndingTurn:
                // Player ended his turn, but projectiles still flying
                wormFrozen = true;
                synchronized = false;
                worm = null;
                core.ResetActivePlayer();
                timer = 500;
                break;
            case GameState.AfterTurn:
                // Poisoned worms take damage
                if (RNG.Bool(0)) {
                    // poison damage
                    timer = 500;
                } else ChangeState();
                break;
            case GameState.Remove0hp:
                // Worms with 0 HP explode
                if (RNG.Bool(0)) {
                    // blow them up
                    next = GameState.Remove0hp;
                    timer = 500;
                } ChangeState();
                break;
        }
    }
}
