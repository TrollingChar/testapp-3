using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelController : MonoBehaviour {
    protected int currOpenness = 0;
    protected int fullOpenness = 15;
    public Canvas canvas;
    public bool open;

    void Start () {
        UpdatePosition();
    }

    void Update () {
        if (open) {
            if (currOpenness < fullOpenness) {
                ++currOpenness;
                UpdatePosition();
            }
        } else {
            if (currOpenness > 0) {
                --currOpenness;
                UpdatePosition();
            }
        }
    }

    protected abstract void UpdatePosition ();

    public void Show (bool instant = false) {
        open = true;
        if (instant) {
            currOpenness = fullOpenness;
            UpdatePosition();
        }
    }

    public void Hide (bool instant = false) {
        open = false;
        if (instant) {
            currOpenness = 0;
            UpdatePosition();
        }
    }

    public void Toggle (bool instant = false) {
        if (open) Hide(instant);
        else Show(instant);
    }
}

