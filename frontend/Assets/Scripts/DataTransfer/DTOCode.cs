namespace DataTransfer {

    public enum DTOCode : short {

        // client commands: ...0
        AuthRequest = 0,
        JoinLobby = 10,
        LeaveLobby = 20,
        TurnDataClient = 30,
        TurnEnded = 40,
        LeaveGame = 50,

        // server commands: ...1
        AuthSuccess = 1,
        LobbyStatus = 11,
        LeftLobby = 21,
        NewGame = 31,
        TurnDataServer = 41,
        NewTurn = 51,
        LeftGame = 61,
        GameEnded = 71,

        // non-command dto: ...2
        AccountData = 2,
        TurnData = 12,
        EndTurnData = 22,
        EndGameData = 32,
        GameInitData = 42

    }

}
