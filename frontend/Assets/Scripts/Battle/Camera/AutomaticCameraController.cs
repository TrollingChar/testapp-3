using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Geometry;
using UnityEngine;
using Utils;
using Object = Battle.Objects.Object;


namespace Battle.Camera {

    public class AutomaticCameraController : CameraController {

//        public AutomaticCameraController (CameraWrapper camera) : base(camera) {}

//        private HashSet <Object> _markedSet;
//        private List <Object>    _markedList;


        public AutomaticCameraController () {
//            _markedSet  = new HashSet <Object> ();
//            _markedList = new List <Object> ();
        }
/*

        // todo!! оптимизировать
        public override void Update () {
            if (Input.GetMouseButtonDown(MouseButtons.Right) && !Input.GetMouseButtonUp(MouseButtons.Right)) {
                Camera.Controller = new MouseBasedCameraController();
            }

            var target = (XY) (Vector2) Camera.Target;
            var box = new Box (target.X - 350, target.X + 350, target.Y - 150, target.Y + 150);

            var unmarked = The.World.Objects.
            Where             (o => o.Priority > 0).// && !_markedSet.Contains (o)).
            OrderByDescending (o => o.Priority).
            ThenBy            (o => Geom.SqrDistance (box, o.Position)).
            ToList            ();
            // сгруппировали объекты по приоритетам и расстояниям

//            _markedSet.RemoveWhere (o => o.Priority <= 0);
//            _markedList = _markedList.
//            Where (_markedSet.Contains).
//            OrderBy (o => o.Priority).
//            ToList ();

            box = new Box (
                float.PositiveInfinity,
                float.NegativeInfinity,
                float.PositiveInfinity,
                float.NegativeInfinity
            );
            
//            int i = 0, j = 0, count = _markedList.Count;
//            for (int pr = 4; pr > 0; pr--) {
//                for (; i < count && _markedList[i].Priority == pr; i++) {
//                    Add (_markedList[i], ref box);
//                }
//                for (; j < unmarked.Count && unmarked[j].Priority == pr; j++) {
//                    var o = unmarked[j];
//                    if (o.Priority != pr) break;
//                    if (!_markedSet.Contains (o)) Add (o, ref box);
//                }
//            }
            foreach (var o in unmarked) {
                Add (o, ref box);
            }

            // двигаем камеру
            target.X = Mathf.Clamp (target.X, box.Right - 350, box.Left   + 350);
            target.Y = Mathf.Clamp (target.Y, box.Top   - 150, box.Bottom + 150);
            Camera.LookAt (target, true);

            // очистка объектов, не попавших в камеру
            box = new Box (target.X - 350, target.X + 350, target.Y - 150, target.Y + 150);
//            _markedList.RemoveAll (o => !box.Contains (o.Position));
//            _markedSet = new HashSet <Object> (_markedList);
            
//            Debug.Log (_markedList.Count);
        }


        private void Add (Object o, ref Box box) {
            var p = o.Position;
            if (p.X > box.Left   + 700 || p.X < box.Right - 700 ||
                p.X > box.Bottom + 300 || p.Y < box.Top   - 300) return;
//            Mark (o);
            box.Left   = Math.Min (p.X, box.Left);
            box.Right  = Math.Max (p.X, box.Right);
            box.Bottom = Math.Min (p.Y, box.Bottom);
            box.Top    = Math.Max (p.Y, box.Top);
        }


//        private void Mark (Object o) {
//            if (!_markedSet.Add (o)) return;
//            _markedList.Add (o);
//        }
*/
    }

}
