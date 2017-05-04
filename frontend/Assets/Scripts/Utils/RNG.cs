using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class RNG {
    static System.Random rng = new System.Random();
    //static MT19937 mt19937 = new MT19937();

    //static public void Init (ulong seed) { mt19937.Init(seed); }
    //static public bool Bool (double chance) { return mt19937.NextDouble() < chance; }
    //static public bool Bool (int chance, int outOf) { return ((uint)mt19937.NextInt() % outOf) < chance; }
    //static public int Int () { return (int)mt19937.NextInt(); }
    //static public int Int (int max) { return (int)((uint)mt19937.NextInt() % max); }
    //static public int Int (int min, int max) { return mt19937.RandomRange(min, max); }
    //static public float Float () { return (float)mt19937.NextDouble(); }
    //static public double Double () { return mt19937.NextDouble(); }
    //static public Vector2 Vector2() { return new Vector2(Float(), Float()); }

    static public void Init (int seed) { rng = new System.Random(seed); }
    static public bool Bool (double chance) { return rng.NextDouble() < chance; }
    static public bool Bool (int chance, int outOf) { return rng.Next(outOf) < chance; }
    static public int Int () { return rng.Next(); }
    static public int Int (int max) { return rng.Next(max); }
    static public int Int (int min, int max) { return rng.Next(min, max); }
    static public float Float () { return (float)rng.NextDouble(); }
    static public double Double () { return rng.NextDouble(); }
    static public Vector2 Vector2 () { return new Vector2(Float(), Float()); }
}
