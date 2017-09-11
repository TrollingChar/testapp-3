namespace Commands.Client {

    public enum ClientCmdId : byte {

        Authorize = 0,
        JoinLobby = 1,
        QuitGame = 2,
        SendTurnData = 3,
        EndTurn = 4,
        RemoveFromLobby = 5,

    }

}
