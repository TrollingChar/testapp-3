using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientAPI {
    public const byte AUTH = 0;
    public const byte TO_HUB = 1;
    public const byte QUIT = 2;
    public const byte TURN_DATA = 3;
    public const byte END_TURN = 4;
}

public class ServerAPI {
    public const byte ACCOUNT_DATA = 0;
    public const byte HUB_CHANGED = 1;
    public const byte START_GAME = 2;
    public const byte LEFT_GAME = 3;
    public const byte END_GAME = 4;
    public const byte TURN_DATA = 5;
}
