using UnityEngine;


namespace UI {

    public class StandardPanelController : PanelController {

        [SerializeField] private float
            _openAnchorY,
            _closedAnchorY,
            _openPositionY,
            _closedPositionY;


        protected override void UpdatePosition () {
            var rt = Canvas.transform as RectTransform;
            Vector2
                min = rt.anchorMin,
                max = rt.anchorMax,
                pos = rt.anchorMax;
            float relativeOpenness = (float) CurrOpenness / FullOpenness;
            min.y = max.y = relativeOpenness * (_openAnchorY - _closedAnchorY) + _closedAnchorY;
            pos.y = relativeOpenness * (_openPositionY - _closedPositionY) + _closedPositionY;
            rt.anchorMin = min;
            rt.anchorMax = max;
            rt.anchoredPosition = pos;
        }

    }

}
