namespace Commands.Server {

    public enum ServerCmdId : byte {

        Authorized = 0,
        HubChanged = 1,
        GameStarted = 2,
        PlayerLeftGame = 3,
        ShowWinner = 4,
        HandleTurnData = 5,
        NoWinner = 6,
        StartNewTurn = 7

    }

}
