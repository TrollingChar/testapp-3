using Messengers;


public class Root {

    private readonly PlayerInfoReceivedMessenger _messenger;


    public Root (PlayerInfoReceivedMessenger messenger) {
        _messenger = messenger;
        messenger.Subscribe(SetPlayerInfo);
    }


    public PlayerInfo PlayerInfo { get; private set; }


    private void SetPlayerInfo (PlayerInfo info) {
        _messenger.Unsubscribe(SetPlayerInfo);
        PlayerInfo = info;
    }

}
