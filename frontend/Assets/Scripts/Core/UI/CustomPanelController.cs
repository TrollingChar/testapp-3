using UnityEngine;


namespace Core.UI {

    public class CustomPanelController : PanelController {

        private bool _initialized;

        [SerializeField] private Vector2
            _openPosition,
            _closedPosition,
            _openAnchorMin,
            _closedAnchorMin,
            _openAnchorMax,
            _closedAnchorMax;

        [SerializeField] private int _timeToOpen;


        protected override void UpdatePosition () {
            if (!_initialized) {
                FullOpenness = _timeToOpen;
                _initialized = true;
            }
            var rt = (RectTransform) Canvas.transform;
            float relativeOpenness = (float) CurrOpenness / FullOpenness;
            rt.anchorMin = relativeOpenness * (_openAnchorMin - _closedAnchorMin) + _closedAnchorMin;
            rt.anchorMax = relativeOpenness * (_openAnchorMax - _closedAnchorMax) + _closedAnchorMax;
            rt.anchoredPosition = relativeOpenness * (_openPosition - _closedPosition) + _closedPosition;
        }

    }

}
