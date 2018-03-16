using Battle.Objects.Controllers;
using Core;
using UnityEngine;
using UnityEngine.UI;


namespace Battle.Objects.Effects {

    public class Label : Effect {

        private readonly string _text;
        private readonly Color _color;


        public Label (string text, Color color) {
            _text = text;
            _color = color;
        }


        public override void OnAdd () {
            var assets = The.BattleAssets;
            var canvas = UnityEngine.Object.Instantiate(assets.CenterCanvas, GameObject.transform, false);
            canvas.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            canvas.GetComponent<Canvas>().sortingLayerName = "Text";
            var text = UnityEngine.Object.Instantiate(assets.Text, canvas.transform, false).GetComponent<Text>();
            text.text = _text;
            text.color = _color;
            Controller = new StandardController();
        }

    }

}
