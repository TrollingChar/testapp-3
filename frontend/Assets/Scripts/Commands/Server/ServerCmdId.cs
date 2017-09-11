namespace Commands.Server {

    public enum ServerCmdId : byte {

        Authorized = 0,
        UpdateLobbyStatus = 1,
        RemovedFromLobby = 2,
        PlayerLeftGame = 3,
        ShowWinner = 4,
        HandleTurnData = 5,
        NoWinner = 6,
        StartNewTurn = 7,
        GameStarted = 8,

    }

}
