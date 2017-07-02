using UnityEngine;


namespace UI {

    public class CustomPanelController : PanelController {

        [SerializeField] private int _timeToOpen;
        [SerializeField] private Vector2
            _openPosition,
            _closedPosition,
            _openAnchorMin,
            _closedAnchorMin,
            _openAnchorMax,
            _closedAnchorMax;

        private bool _initialized;


        protected override void UpdatePosition () {
            if (!_initialized) {
                FullOpenness = _timeToOpen;
                _initialized = true;
            }
            var rt = Canvas.transform as RectTransform;
            float relativeOpenness = (float) CurrOpenness / FullOpenness;
            rt.anchorMin = relativeOpenness * (_openAnchorMin - _closedAnchorMin) + _closedAnchorMin;
            rt.anchorMax = relativeOpenness * (_openAnchorMax - _closedAnchorMax) + _closedAnchorMax;
            rt.anchoredPosition = relativeOpenness * (_openPosition - _closedPosition) + _closedPosition;
        }

    }

}
