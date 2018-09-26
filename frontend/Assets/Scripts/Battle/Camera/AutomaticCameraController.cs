using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Geometry;
using UnityEngine;
using Object = Battle.Objects.Object;


namespace Battle.Camera {

    public class AutomaticCameraController : CameraController {

//        public AutomaticCameraController (CameraWrapper camera) : base(camera) {}

        private HashSet <Object> _markedSet;
        private List <Object>    _markedList;


        public AutomaticCameraController () {
            _markedSet  = new HashSet <Object> ();
            _markedList = new List <Object> ();
        }


        public override void Update () {

            var target = Camera.Target;
            var box = new Box (target.x - 400, target.x + 400, target.y - 200, target.y + 200);

            var unmarked = The.World.Objects.
            Where   (o => o.CameraPriority > 0 && !_markedSet.Contains (o)).
            OrderBy (o => o.CameraPriority).
            ThenBy  (o => Geom.Distance (box, o.Position)).
            ToList  ();
            // сгруппировали объекты по приоритетам и расстояниям

            _markedSet.RemoveWhere (o => o.CameraPriority <= 0);
            _markedList = _markedList.Where (_markedSet.Contains).OrderBy (o => o.CameraPriority).ToList ();

            box = new Box (
                float.PositiveInfinity,
                float.NegativeInfinity,
                float.PositiveInfinity,
                float.NegativeInfinity
            );
            
            int i = 0, j = 0, count = _markedList.Count;
            for (int pr = 4; pr > 0; pr--) {
                for (; i < count && _markedList[i].CameraPriority == pr; i++) {
                    Add (_markedList[i], ref box);
                }
                for (; j < unmarked.Count && unmarked[j].CameraPriority == pr; j++) {
                    var o = unmarked[j];
                    if (o.CameraPriority != pr) break;
                    if (!_markedSet.Contains (o)) Add (o, ref box);
                }
            }

            // todo: actually move camera, clear marks
        }


        private void Add (Object o, ref Box box) {
            var p = o.Position;
            if (p.X > box.Left   + 400 || p.X < box.Right - 400 ||
                p.X > box.Bottom + 200 || p.Y < box.Top   - 200) return;
            Mark (o);
            box.Left   = Math.Min (p.X, box.Left);
            box.Right  = Math.Max (p.X, box.Right);
            box.Bottom = Math.Min (p.Y, box.Bottom);
            box.Top    = Math.Max (p.Y, box.Top);
        }


        private void Mark (Object o) {
            if (!_markedSet.Add (o)) return;
            _markedList.Add (o);
        }


/*
            var coords = new List <XY>[5];
            for (int i = 1; i < 5; i++) {
                coords[i] = new List <XY> ();
            }
            foreach (var o in The.World.Objects) {
                int p = o.CameraPriority;
                if (p != 0) coords[--p].Add (o.Position);
            }

            // вот кароч у нас есть камера
            var camPosition = (XY) (Vector2) Camera.Target;
            var box = new Box (
                camPosition.X - 300,
                camPosition.X + 300,
                camPosition.Y - 200,
                camPosition.Y + 200
            );

            // если камера не следит за объектами, найти ближайший с учетом приоритета
            // если камера следила за объектом, то постараться не потерять его
            // а для этого запомним все объекты, за которыми мы следим, и присвоим им дополнительные приоритеты
            // таким образом, первый объект будет оставаться для камеры главным
            // мы не крутим камеру специально, чтобы захватить дополнительные объекты
            // возможно, когда-нибудь это стоит сделать

            for (int i = 4; i > 0; i--) {
                if (coords[i].Count == 0) continue;

                float min     = XY.SqrDistance (coords[i][0], coords[i][0].Clamped (box));
                var   nearest = coords[i][0];

                for (int j = 1; j < coords[i].Count; j++) {
                    var   p  = coords[i][j];
                    float d2 = XY.SqrDistance (p, p.Clamped (box));
                    if (d2 < min) {
                        min     = d2;
                        nearest = p;
                    }
                }
            }
        }*/

    }

}