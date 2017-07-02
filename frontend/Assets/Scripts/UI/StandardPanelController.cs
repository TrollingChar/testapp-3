using UnityEngine;


namespace UI {

    public class StandardPanelController : PanelController {

        [SerializeField] private float
            openAnchorY,
            closedAnchorY,
            openPositionY,
            closedPositionY;


        protected override void UpdatePosition () {
            var rt = canvas.transform as RectTransform;
            Vector2
                min = rt.anchorMin,
                max = rt.anchorMax,
                pos = rt.anchorMax;
            float relativeOpenness = (float) currOpenness / fullOpenness;
            min.y = max.y = relativeOpenness * (openAnchorY - closedAnchorY) + closedAnchorY;
            pos.y = relativeOpenness * (openPositionY - closedPositionY) + closedPositionY;
            rt.anchorMin = min;
            rt.anchorMax = max;
            rt.anchoredPosition = pos;
        }

    }

}
