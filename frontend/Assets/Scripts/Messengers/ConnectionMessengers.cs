using Utils.Messenger;
using War;


namespace Messengers {

    public class PlayerInfoReceivedMessenger : Messenger<PlayerInfo> {}
    public class HubChangedMessenger : Messenger<int, int> {}
    public class StartGameMessenger : Messenger<GameInitData> {}
    public class PlayerQuitMessenger : Messenger<int> {}
    public class PlayerWinMessenger : Messenger<int> {}
    public class TurnDataReceivedMessenger : Messenger<TurnData> {}
    public class NoWinnerMessenger : Messenger {}
    public class NewTurnMessenger : Messenger<int> {} // int - player that receive turn

}
