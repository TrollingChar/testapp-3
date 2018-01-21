using UnityEngine;


namespace Battle.Terrain {

    public class LandRendererTile {

        public const int Size = 256; // maybe use 254 with offset
        private GameObject _gameObject;
        private readonly Vector3 _position;
        private Texture2D _tex;
        private readonly Transform _parent;
        private bool _dirty;


        public LandRendererTile (Transform parent, int x, int y) {
            _parent = parent;
            _position = new Vector3(x * Size, y * Size, 0);
        }


        public bool IsEmpty {
            get { return _gameObject == null; }
        }


        public void SetPixel (int x, int y, Color color) {
            if (IsEmpty) {
                if (color == Color.clear) return;
                // create new
                _gameObject = new GameObject("LandChunk", typeof(SpriteRenderer));
                _gameObject.transform.localPosition = _position;
                _gameObject.transform.SetParent(_parent, false);
                var renderer = _gameObject.GetComponent<SpriteRenderer>();
                _tex = new Texture2D(Size, Size, TextureFormat.RGBA32, false);
                _tex.wrapMode = TextureWrapMode.Clamp;
                renderer.sprite = Sprite.Create(
                    _tex,
                    new Rect(0, 0, Size, Size),
                    new Vector2(0, 0),
                    1f,
                    0,
                    SpriteMeshType.FullRect
                );
                _tex.SetPixels(new Color[Size * Size]);
            }
            _tex.SetPixel(x, y, color);
            _dirty = true;
        }


        public void Clear () {
            Object.Destroy(_gameObject);
        }


        public void Apply () {
            if (_dirty) _tex.Apply();
            _dirty = false;
        }

    }

}
