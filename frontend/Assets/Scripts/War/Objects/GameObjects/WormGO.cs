using UnityEngine;
using UnityEngine.UI;


namespace War.Objects.GameObjects {

    public class WormGO : MonoBehaviour {

        [SerializeField] private Text _name, _hp;
        [SerializeField] private SpriteRenderer _arrow, _headRenderer, _tailRenderer;
        [SerializeField] private Transform _sprite, _head, _tail;

        public string Name {
            set { _name.text = value; }
        }

        public Color Color {
            set {
                _name.color =
                _hp.color =
                _arrow.color = value;
            }
        }

        public int HP {
            set { _hp.text = value.ToString(); }
        }

        public bool ArrowVisible {
            set { _arrow.enabled = value; }
        }


        public void OnAdd (Worm worm) {
            Name = worm.Name;
            HP = worm.HP;
            Color = worm.Color;
            ArrowVisible = worm.ArrowVisible;
            FacesRight = worm.FacesRight;
        }


        public bool FacesRight {
            set {
                if (_sprite.localScale.x < 0 ^ value) return;
                var v3 = _sprite.localScale;
                v3.x = -v3.x;
                _sprite.localScale = v3;
            }
        }

        public float HeadAngle {
            set {
                if (_sprite.localScale.x > 0) {
                    if (Mathf.Abs(value) < 90) {
                        _headRenderer.flipX = false;
                        _head.localEulerAngles = new Vector3(0, 0, value);
                    } else {
                        _headRenderer.flipX = true;
                        _head.localEulerAngles = new Vector3(0, 0, value + 180);
                    }
                } else {
                    if (Mathf.Abs(value) < 90) {
                        _headRenderer.flipX = true;
                        _head.localEulerAngles = new Vector3(0, 0, value + 180);
                    } else {
                        _headRenderer.flipX = false;
                        _head.localEulerAngles = new Vector3(0, 0, value);
                    }
                }
            }
        }

    }

}
