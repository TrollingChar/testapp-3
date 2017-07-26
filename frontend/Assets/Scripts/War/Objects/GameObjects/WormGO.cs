﻿using System;
using Geometry;
using UnityEngine;
using UnityEngine.UI;


namespace War.Objects.GameObjects {

    public class WormGO : MonoBehaviour {

        private Worm _worm;

        [SerializeField] private Text _name, _hp;
        [SerializeField] private SpriteRenderer _arrow, _headRenderer, _tailRenderer;
        [SerializeField] private Transform _sprite, _head, _tail;
        private float _headAngle;

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
            _worm = worm;
            Name = worm.Name;
            HP = worm.HP;
            Color = worm.Color;
            ArrowVisible = worm.ArrowVisible;
            FacesRight = worm.FacesRight;
            Look(_headAngle = worm.FacesRight ? 0 : 180);
        }


        public bool FacesRight {
            set {
                if (_sprite.localScale.x < 0 ^ value) return;
                var v3 = _sprite.localScale;
                v3.x = -v3.x;
                _sprite.localScale = v3;
                
                Look(_headAngle);
            }
        }


        public void Look (float angle) {
            _headAngle = angle;
            if (_sprite.localScale.x > 0) {
                if (Mathf.Abs(angle) < 100) {
                    _headRenderer.flipX = false;
                    _head.localEulerAngles = new Vector3(0, 0, angle * 0.5f);
                } else {
                    _headRenderer.flipX = true;
                    _head.localEulerAngles = new Vector3(0, 0, angle * 0.5f + (angle < 0 ? 90 : -90));
                }
            } else {
                if (Mathf.Abs(angle) > 80) {
                    _headRenderer.flipX = false;
                    _head.localEulerAngles = new Vector3(0, 0, angle * -0.5f - (angle < 0 ? 90 : -90));
                } else {
                    _headRenderer.flipX = true;
                    _head.localEulerAngles = new Vector3(0, 0, angle * -0.5f);
                }
            }
        }

    }

}