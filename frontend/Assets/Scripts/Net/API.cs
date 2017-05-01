using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAPI {
    public const byte Auth = 0;
    public const byte ToHub = 1;
    public const byte Quit = 2;
    public const byte TurnData = 3;
    public const byte EndTurn = 4;
}

public class ServerAPI {
    public const byte AccountData = 0;
    public const byte HubChanged = 1;
    public const byte StartGame = 2;
    public const byte LeftGame = 3;
    public const byte EndGame = 4;
    public const byte TurnData = 5;
}
