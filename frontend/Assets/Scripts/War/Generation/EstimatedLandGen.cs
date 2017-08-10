using System;
using System.Collections;
using System.Collections.Generic;
using Messengers;
using UI.Panels;
using UnityEngine;
using Utils;
using Zenject;


namespace War.Generation {

    public class EstimatedLandGen {

        [InjectOptional] private HintArea _hintArea;
        [Inject] private CoroutineKeeper _coroutineKeeper;

        private const int ExpandEstimation = 5;
        private const int CellularEstimation = 10;
        private const int RescaleEstimation = 5;
        private const int RotateEstimation = 5;

        private int _done;
        private int _lastProgress;
        private int _estimation;

        private LandGen _landGen;
        private int _width, _height;
        private Queue<IEnumerator> _instructions;

        private readonly LandGenProgressMessenger _progressMessenger;
        private readonly LandGenCompleteMessenger _completeMessenger;

        public EstimatedLandGen (LandGen landGen) {
            _landGen = landGen;
            _width = landGen.Array.GetLength(0);
            _height = landGen.Array.GetLength(1);
            
            _progressMessenger = new LandGenProgressMessenger();
            _completeMessenger = new LandGenCompleteMessenger();
            _instructions = new Queue<IEnumerator>();
        }


        public EstimatedLandGen Expand (int iterations = 1) {
            for (int i = 0; i < iterations; i++) {
                _width *= 2;
                _height *= 2;
                _estimation += _width * _height * ExpandEstimation;
            }
            _instructions.Enqueue(DoExpand(iterations));
            return this;
        }


        public EstimatedLandGen Cellular (uint rules, int iterations = 5) {
            _estimation += _width * _height * CellularEstimation * iterations;
            _instructions.Enqueue(DoCellular(rules, iterations));
            return this;
        }


        public EstimatedLandGen Rescale (int w, int h) {
            _estimation += w * h * RescaleEstimation;
            _width = w;
            _height = h;
            _instructions.Enqueue(DoRescale(w, h));
            return this;
        }


        public EstimatedLandGen SwitchDimensions () {
            _estimation += _width * _height * RotateEstimation;
            int t = _width;
            _width = _height;
            _height = t;
            _instructions.Enqueue(DoSwitchDimensions());
            return this;
        }


        private IEnumerator DoExpand (int iterations) {
            yield return null;
            for (int i = 0; i < iterations; i++) {
                _landGen = _landGen.Expand();
                _done += _landGen.Array.GetLength(0) * _landGen.Array.GetLength(1) * ExpandEstimation;

                foreach (var p in CheckProgress()) yield return p;
            }
        }


        private IEnumerator DoCellular (uint rules, int iterations) {
            yield return null;
            for (int i = 0; i < iterations; i++) {
                _landGen = _landGen.Cellular(rules, iterations);
                _done += _landGen.Array.GetLength(0) * _landGen.Array.GetLength(1) * CellularEstimation;

                foreach (var p in CheckProgress()) yield return p;
            }
        }


        private IEnumerator DoRescale (int w, int h) {
            yield return null;
            _landGen = _landGen.Rescale(w, h);
            _done += _landGen.Array.GetLength(0) * _landGen.Array.GetLength(1) * CellularEstimation;

            foreach (var p in CheckProgress()) yield return p;
        }


        private IEnumerator DoSwitchDimensions () {
            yield return null;
            _landGen = _landGen.SwitchDimensions();
            _done += _landGen.Array.GetLength(0) * _landGen.Array.GetLength(1) * RotateEstimation;

            foreach (var p in CheckProgress()) yield return p;
        }


        private IEnumerable CheckProgress () {
            int progress = 100 * _done / _estimation;
            if (progress < _lastProgress) yield break;
            _hintArea.Text = "Прогресс генерации: " + progress + "%";
            _progressMessenger.Send(_lastProgress = progress);
            yield return null;
        }


        public IEnumerator GenerationCoroutine () {
            foreach (var instruction in _instructions) {
                yield return _coroutineKeeper.StartCoroutine(instruction);
            }
            _hintArea.Text = "Готово!";
            _completeMessenger.Send(_landGen);
        }


        public void Generate () {
            Debug.Log(_estimation);
            _coroutineKeeper.StartCoroutine(GenerationCoroutine());
        }

    }

}
