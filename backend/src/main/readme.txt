
    Player can join  room and room must react.
    Player can leave room and room must react.

    Room can kick player and player must react.

    +--------+
    | Player |
    |--------|
    |+ joinRoom(room:Room)
    |+ leaveRoom(room:Room)
    |+ whenKicked(room:Room)
    +--------+


    +--------+
    |  Room  |
    |--------|
    |+ onPlayerJoined(player:Player)
    |+ onPlayerLeft(player:Player)
    |+ kickPlayer(player:Player)
    +--------+