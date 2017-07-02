using UnityEngine;
using UnityEngine.UI;


namespace W3 {

    public class WormGO : MonoBehaviour {

        [SerializeField]
        private Text _text;

        public string text {
            get { return _text.text; }
            set { _text.text = value; }
        }

    }

}
