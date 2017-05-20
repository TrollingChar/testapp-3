using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class Land {
    int progress, fullProgress;

    byte[,] array;
    LandTiles tiles;
    
    public Land (LandGen gen) {
        array = gen.array;
        
        int w = array.GetLength(0),
            h = array.GetLength(1);
        Texture2D tex = new Texture2D(w, h);
        for (int x = 0; x < w; ++x) {
            for (int y = 0; y < h; ++y) {
                tex.SetPixel(x, y, array[x, y] == 0 ? Color.clear : Core.I.landTexture.GetPixel(x & 0xff, y & 0xff));
            }
        }
        tex.Apply();
        Core.I.landRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, w, h), new Vector2(0.5f, 0.5f), 1f);
    }
}
