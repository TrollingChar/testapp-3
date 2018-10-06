using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


namespace Battle.Terrain.Generation {

    public class EstimatedLandGen {

        private const int ExpandEstimation   = 5;
        private const int CellularEstimation = 10;
        private const int RescaleEstimation  = 5;
        private const int RotateEstimation   = 5;

        private readonly Queue <IEnumerator> _instructions;

        public event Action <float>   OnProgress;
        public event Action <LandGen> OnComplete;

        private MonoBehaviour _coroutineKeeper;

        private long _estimation;
        private long _done;

        private LandGen _landGen;
        private int     _width, _height;


        public EstimatedLandGen (LandGen landGen) {
            _landGen = landGen;
            _width   = landGen.Array.GetLength (0);
            _height  = landGen.Array.GetLength (1);

            _instructions = new Queue <IEnumerator> ();
        }


        public long Done {
            get { return _done; }
            private set {
                _done = value;
                OnProgress._ (100f * value / _estimation);
            }
        }


        public EstimatedLandGen Expand (int iterations = 1) {
            for (int i = 0; i < iterations; i++) {
                _width  *= 2;
                _height *= 2;
                _width--;
                _height--;
                _estimation += _width * _height * ExpandEstimation;
            }
            _instructions.Enqueue (DoExpand (iterations));
            return this;
        }


        public EstimatedLandGen Cellular (uint rules, int iterations = 5) {
            _estimation += _width * _height * CellularEstimation * iterations;
            _instructions.Enqueue (DoCellular (rules, iterations));
            return this;
        }


        public EstimatedLandGen Rescale (int w, int h) {
            _estimation += w * h * RescaleEstimation;
            _width      =  w;
            _height     =  h;
            _instructions.Enqueue (DoRescale (w, h));
            return this;
        }


        public EstimatedLandGen SwitchDimensions () {
            _estimation += _width * _height * RotateEstimation;
            int t   = _width;
            _width  = _height;
            _height = t;
            _instructions.Enqueue (DoSwitchDimensions ());
            return this;
        }


        private IEnumerator DoExpand (int iterations) {
            yield return null;
            for (int i = 0; i < iterations; i++) {
                _landGen =  _landGen.Expand ();
                Done     += _landGen.Array.GetLength (0) * _landGen.Array.GetLength (1) * ExpandEstimation;
                yield return null;
            }
        }


        private IEnumerator DoCellular (uint rules, int iterations) {
            yield return null;
            for (int i = 0; i < iterations; i++) {
                _landGen =  _landGen.Cellular (rules, 1);
                Done     += _landGen.Array.GetLength (0) * _landGen.Array.GetLength (1) * CellularEstimation;
                yield return null;
            }
        }


        private IEnumerator DoRescale (int w, int h) {
            yield return null;
            _landGen =  _landGen.Rescale (w, h);
            Done     += _landGen.Array.GetLength (0) * _landGen.Array.GetLength (1) * RescaleEstimation;
            yield return null;
        }


        private IEnumerator DoSwitchDimensions () {
            yield return null;
            _landGen =  _landGen.SwitchDimensions ();
            Done     += _landGen.Array.GetLength (0) * _landGen.Array.GetLength (1) * RotateEstimation;
            yield return null;
        }


        private IEnumerator GenerationCoroutine () {
            foreach (var instruction in _instructions) {
                yield return _coroutineKeeper.StartCoroutine (instruction);
            }
            OnComplete._ (_landGen);
        }


        public void Generate (MonoBehaviour coroutineKeeper) {
            _coroutineKeeper = coroutineKeeper;
            coroutineKeeper.StartCoroutine(GenerationCoroutine());
        }

    }

}
