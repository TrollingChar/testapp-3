using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.code.bf;

enum GameState {

}

class GameStateController {
    const int turnTime = 30000;
    const int retreatTime = 3000;

    bool synchronized;

    GameState current, next;
    bool wormFrozen;
    Worm worm;

    int timer;
    public int Timer { get { return timer; } }
    public string TimerString { get { return ((timer + 999) % 1000).ToString(); } }

    public void Update () {

    }

    void ChangeState () {
    }

    void EnterState () {
    }
}
