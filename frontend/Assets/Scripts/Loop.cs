using Net;
using UnityEngine;
using Zenject;


public class Loop : MonoBehaviour {

    [Inject] private WSConnection _connection;


    private void FixedUpdate () {
        // todo: split logic in menu and in game
        _connection.Work();

        // todo: update bf if exists
    }


    private void Update () {}

}
