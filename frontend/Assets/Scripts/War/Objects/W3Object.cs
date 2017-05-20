using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class W3Object {
    static W3Object empty = new EmptyObject();

    public LinkedListNode<W3Object> node;

    public Controller controller;

    public Vector2 position, velocity;

    public float movement;
    public HashSet<W3Object> excluded;

    public void ExcludeObjects () {
        throw new NotImplementedException();
    }

    public W3Collision NextCollision () {
        throw new NotImplementedException();
    }

    public void Update () {
        controller.Update();
    }

    public void Remove () {
        node.Value = empty;
    }
}
