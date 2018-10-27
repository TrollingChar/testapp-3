using UnityEngine;


namespace UI {

    public class VerticalLayout : MonoBehaviour {

        public float Spacing = -30;
        public float Pivot   = 0.5f;


        private void Start () {
            ApplyLayout ();
        }


        public void ApplyLayout () {
            int   childCount = transform.childCount;
            float x          = transform.position.x;
            float y          = transform.position.y - Pivot * (childCount - 1) * Spacing;
            for (int i = 0; i < childCount; i++) {
                transform.GetChild (i).position = new Vector2 (x, y + i * Spacing);
            }
        }

    }

}