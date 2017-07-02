using UnityEngine;


namespace UI {

    public class CustomPanelController : PanelController {

        public int timeToOpen;

        public Vector2
            openPosition,
            closedPosition,
            openAnchorMin,
            closedAnchorMin,
            openAnchorMax,
            closedAnchorMax;

        private bool initialized;


        protected override void UpdatePosition () {
            if (!initialized) {
                fullOpenness = timeToOpen;
                initialized = true;
            }
            var rt = canvas.transform as RectTransform;
            float relativeOpenness = (float) currOpenness / fullOpenness;
            rt.anchorMin = relativeOpenness * (openAnchorMin - closedAnchorMin) + closedAnchorMin;
            rt.anchorMax = relativeOpenness * (openAnchorMax - closedAnchorMax) + closedAnchorMax;
            rt.anchoredPosition = relativeOpenness * (openPosition - closedPosition) + closedPosition;
        }

    }

}
