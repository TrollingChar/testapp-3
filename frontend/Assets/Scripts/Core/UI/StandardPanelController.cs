using UnityEngine;


namespace Core.UI {

    public class StandardPanelController : PanelController {

        [SerializeField] private float _openAnchorY; 
        [SerializeField] private float _closedAnchorY; 
        [SerializeField] private float _openPositionY; 
        [SerializeField] private float _closedPositionY;
        

        protected override void UpdatePosition () {
            var rt = (RectTransform) Canvas.transform;
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
