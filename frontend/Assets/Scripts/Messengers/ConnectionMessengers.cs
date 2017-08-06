using Utils.Messenger;
using War;


namespace Messengers {

    public class PlayerInfoReceivedMessenger : Messenger<PlayerInfo> {}
    public class HubChangedMessenger : Messenger<int, int> {}
    public class StartGameMessenger : Messenger<GameInitData> {}

}
