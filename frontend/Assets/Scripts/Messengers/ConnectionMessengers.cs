using System;
using Battle;
using Core;
using Utils.Messenger;


namespace Messengers {

    [Obsolete] public class PlayerInfoReceivedMessenger : Messenger<PlayerInfo> {}
    [Obsolete] public class HubChangedMessenger : Messenger<int, int> {}
    [Obsolete] public class StartGameMessenger : Messenger<GameInitData> {}
    [Obsolete] public class PlayerQuitMessenger : Messenger<int> {}
    [Obsolete] public class PlayerWinMessenger : Messenger<int> {}
    [Obsolete] public class TurnDataReceivedMessenger : Messenger<TurnData> {}
    [Obsolete] public class NoWinnerMessenger : Messenger {}
    [Obsolete] public class NewTurnMessenger : Messenger<int> {} // int - player that receive turn

}
