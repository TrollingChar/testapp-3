using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitmapData : MonoBehaviour {

    public Texture2D source;
    Texture2D tex;

    // Use this for initialization
    void Start () {
        int w = 2000,
            h = 1000;

        // Create a new 2x2 texture ARGB32 (32 bit with alpha) and no mipmaps
        tex = new Texture2D(w, h, TextureFormat.RGBA32, false);

        // GENERATE!
        LandGen land =
            new LandGen(new byte[,] {{0, 0, 0, 0, 0},
                                     {0, 1, 1, 1, 0},
                                     {0, 1, 0, 1, 0}})
            .SwitchDimensions()
            .Expand(7)
            .Cellular(0x01e801d0, 20)
            .Cellular(0x01f001e0)
            .Expand()
            .Cellular(0x01e801d0, 20)
            .Cellular(0x01f001e0)
            .Rescale(2000, 1000)
            .Cellular(0x01f001e0);

        for (int x = 0; x < w; ++x) {
            for (int y = 0; y < h; ++y) {
                tex.SetPixel(x, y, land.array[x, y] == 0 ? Color.clear : source.GetPixel(x & 0xff, y & 0xff));
            }
        }
        tex.Apply();
        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f));
    }

    // Work is called once per frame
    void Update () { }
}
