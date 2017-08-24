using System;
using Battle;
using UnityEngine.Events;

// ReSharper disable All


namespace Old {

    [Serializable]
    public class UnityEvent_int : UnityEvent<int> {}


    [Serializable]
    public class UnityEvent_int_int : UnityEvent<int, int> {}


    [Serializable]
    public class UnityEvent_string : UnityEvent<string> {}


    [Serializable]
    public class UnityEvent_string_int : UnityEvent<string, int> {}


    [Serializable]
    public class UnityEvent_byteArray : UnityEvent<byte[]> {}


    [Serializable]
    public class UnityEvent_TurnData : UnityEvent<TurnData> {}


    [Serializable]
    public class UnityEvent_GameData : UnityEvent<GameInitData> {}

}
