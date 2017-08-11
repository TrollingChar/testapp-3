using Messengers;
using Zenject;


public class Root {

    private readonly PlayerInfoReceivedMessenger _messenger;
    
    public Root (PlayerInfoReceivedMessenger messenger) {
        _messenger = messenger;
        messenger.sSubscribe(SetPlayerInfo);
    }


    private void SetPlayerInfo (PlayerInfo info) {
        _messenger.Unsubscribe(SetPlayerInfo);
        PlayerInfo = info;
    }


    public PlayerInfo PlayerInfo { get; private set; }

//    private void FixedUpdate () {
//        The<WSConnection>.Get().Work(); // receive data from server and update world
//        if (_bf != null) _bf.Work(); // update world independently
//    }


//    public void UpdateWorld (TurnData td) {
//        _bf.Work(td);
//    }


//    public void NewTurn (int player) {
//        _bf.State.StartTurn(player);
//    }


//    public void Synchronize () {
//        _connection.SendEndTurn(true);
//    }

}
