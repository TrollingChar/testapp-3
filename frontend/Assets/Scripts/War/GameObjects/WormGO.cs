﻿using UnityEngine;
using UnityEngine.UI;


namespace War.GameObjects {

    public class WormGO : MonoBehaviour {

        [SerializeField] private Text _text;

        public string Text {
            get { return _text.text; }
            set { _text.text = value; }
        }

    }

}
