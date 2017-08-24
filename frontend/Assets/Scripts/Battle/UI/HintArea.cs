using Core.UI;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.UI {

    public class HintArea : Panel {

        [SerializeField] private Text _text;

        public string Text {
            get { return _text.text; }
            set { _text.text = value; }
        }

    }

}
