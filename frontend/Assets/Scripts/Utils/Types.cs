using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;

[Serializable] public class UnityEvent_int : UnityEvent<int> { }
[Serializable] public class UnityEvent_int_int : UnityEvent<int, int> { }
[Serializable] public class UnityEvent_string : UnityEvent<string> { }
[Serializable] public class UnityEvent_string_int : UnityEvent<string, int> { }
[Serializable] public class UnityEvent_byteArray : UnityEvent<byte[]> { }