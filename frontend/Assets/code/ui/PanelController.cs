using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour {

    public Canvas canvas;
    public Vector2
        openAnchor,
        closedAnchor,
        openPosition,
        closedPosition;
    int currOpenness = 0;
    public int fullOpenness;
    public bool open;

	// Use this for initialization
	void Start () {
        UpdatePosition();
	}
	
	// Update is called once per frame
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

    void UpdatePosition () {
        Vector2 position = (float)currOpenness / fullOpenness * (openPosition - closedPosition) + closedPosition;
        Vector2 anchor = (float)currOpenness / fullOpenness * (openAnchor - closedAnchor) + closedAnchor;
        RectTransform rt = canvas.transform as RectTransform;
        rt.anchorMin = rt.anchorMax = anchor;
        rt.anchoredPosition = position;
    }

    public void Show () {
        open = true;
    }

    public void Hide () {
        open = false;
    }

    public void Toggle () {
        open = !open;
    }
}
