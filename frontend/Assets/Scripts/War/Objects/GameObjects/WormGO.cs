using UnityEngine;
using UnityEngine.UI;


namespace War.Objects.GameObjects {

    public class WormGO : MonoBehaviour {

        [SerializeField] private Text _text;

        public string Text {
            get { return _text.text; }
            set { _text.text = value; }
        }

        public Color Color {
            get { return _text.color; }
            set { _text.color = value; }
        }

    }

}
