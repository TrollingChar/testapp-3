using Battle.Objects.Controllers;
using Core;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.Objects.Effects {

    public class Label : Effect {

        private readonly string _text;
        private readonly Color _color;
        private readonly float _effectTime;


        public Label (string text, Color color, float effectTime) {
            _text = text;
            _color = color;
            _effectTime = effectTime;
        }


        public override void OnAdd () {
            var assets = The.BattleAssets;
            var canvas = UnityEngine.Object.Instantiate(assets.CenterCanvas, GameObject.transform, false);
            canvas.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
//            canvas.GetComponent<Canvas>().sortingLayerName = "TextFront";
            var text = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();
            text.text = _text;
            text.color = _color;
            Controller = new LabelController(_effectTime);
        }

    }

}
