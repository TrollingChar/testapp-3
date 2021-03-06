﻿using UnityEngine;
using UnityEngine.UI;


namespace UI {

    public class HintArea : Panel {

        [SerializeField] private Text _text;

        public string Text {
            get { return _text.text; }
            set { _text.text = value; }
        }

    }

}
