using System;
using Battle.Physics.Collisions;
using UnityEngine;
using BoxCollider = Battle.Physics.Collisions.BoxCollider;


namespace Geometry {

    // all of these must obey the 3 laws!
    public static class Physics {

        private const float Epsilon = 0.001f;
        private const float SqrEpsilon = Epsilon * Epsilon;


        public static bool Overlap (CircleCollider a, CircleCollider b) {
            float rr = a.Radius + b.Radius;
            return XY.SqrDistance(a.Center, b.Center) < rr * rr;
        }


        public static bool Overlap (CircleCollider c, BoxCollider b) {
            //* switch algorithm
            float closestX = Mathf.Clamp(c.Center.X, b.Left, b.Right);
            float closestY = Mathf.Clamp(c.Center.Y, b.Bottom, b.Top);

            float dx = c.Center.X - closestX;
            float dy = c.Center.Y - closestY;

            return dx * dx + dy * dy < c.Radius * c.Radius;
            /*/
            float halfW = 0.5f * (b.Right - b.Left);
            float halfH = 0.5f * (b.Top - b.Bottom);

            float distX = Mathf.Abs(c.Center.X - b.Left - halfW);
            float distY = Mathf.Abs(c.Center.Y - b.Bottom - halfH);

            float r = c.Radius;
            if (distX >= halfW + r || distY >= halfH + r) return false;
            if (distX < halfW || distY < halfH) return true;

            distX -= halfW;
            distY -= halfH;
            return distX * distX + distY * distY < r * r;
            //*/
        }


        public static bool Overlap (BoxCollider a, BoxCollider b) {
            return a.Top > b.Bottom
                && a.Left < b.Right
                && a.Right > b.Left
                && a.Bottom < b.Top;
        }
        
        
        /* общий алгоритм:
            найти "точное" значение по формуле и есть ли коллизия
            если коллизии типа нет, то проверить вычисленное значение, если нет перекрывания то вернуть пустую коллизию
            если коллизия есть то проверить вычисленное значение и если нет перекрывания то вернуть коллизию
            если есть перекрывание то подогнать результат численными методами и вернуть коллизию
        */


        public static NCollision FlyInto (CircleCollider a, CircleCollider b, XY v) {
            // вычисляем значение
            float dist = Geom.RayToCircle(a.Center, v, b.Center, a.Radius + b.Radius);
            
            var ao = a.Center;
            var bo = b.Center;
            float r2 = a.Radius + b.Radius;
            r2 *= r2;
            if (float.IsNaN(dist) || dist < 0 || dist * dist >= v.SqrLength) {
                // нет коллизии, проверить перекрывание
                if (XY.SqrDistance(ao + v, bo) < r2) return new NCollision(v, XY.NaN, null, null);
                dist = v.Length;
            }
            // есть коллизия, проверить перекрывание
            else if (XY.SqrDistance(ao + v.WithLength(dist), bo) < r2) return new NCollision(v, XY.NaN, null, null);

            // численными методами делаем чтобы не было ошибки с перекрыванием
            float lo = 0;
            float hi = dist;
            for (int i = 0; i < 10 && hi - lo > Epsilon; i++) {
                float mid = 0.5f * (lo + hi);
                if (XY.SqrDistance(ao + v.WithLength(mid), bo) < r2) {
                    lo = mid;
                } else {
                    hi = mid;
                }
            }
            var offset = v.WithLength(lo);
            return new NCollision(offset, ao - bo + offset, a, b);
        }


        public static NCollision FlyInto (CircleCollider c, BoxCollider b, XY v) {
            float top = b.Top;
            float left = b.Left;
            float right = b.Right;
            float bottom = b.Bottom;

            XY cCenter = c.Center;
            float cx = c.Center.X;
            float cy = c.Center.Y;
            
            // сначала стороны, потом углы, потом численными методами доделать
            float min = 1;
            if (v.X > 0) {
                float d = Geom.RayToVertical(cx, left, v.X);
                float y = cy + v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) min = d;
            }
            if (v.X < 0) {
                float d = Geom.RayToVertical(cx, right, v.X);
                float y = cy + v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) min = d;
            }
            if (v.Y > 0) {
                float d = Geom.RayToHorizontal(cy, bottom, v.Y);
                float x = cx + v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) min = d;
            }
            if (v.Y < 0) {
                float d = Geom.RayToHorizontal(cy, top, v.Y);
                float x = cx + v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) min = d;
            }
            // если нет столкновений то min будет 1
            float l = v.Length;
            float minDist = min * l;
            if (v.X > 0 || v.Y > 0) {
                float dist = Geom.RayToCircle(cCenter, v, new XY(left, bottom), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }
            if (v.X < 0 || v.Y > 0) {
                float dist = Geom.RayToCircle(cCenter, v, new XY(right, bottom), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }
            if (v.X < 0 || v.Y < 0) {
                float dist = Geom.RayToCircle(cCenter, v, new XY(right, top), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }
            if (v.X > 0 || v.Y < 0) {
                float dist = Geom.RayToCircle(cCenter, v, new XY(left, top), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }

//            XY offset = v.WithLength(minDist);
            if (minDist == l) {
                if (TODO) return new NCollision(v, XY.NaN, null, null);
            } else {
                if (TODO) return TODO;
            }
            
            // todo: численные методы
            float lo = 0;
            float hi = minDist;
            for () {
                float mid = 0.5f * (lo + hi);
                if () {
                    hi = mid;
                } else {
                    lo = mid;
                }
            }

            XY offset = v.WithLength(lo);
            XY newPosition = cCenter + offset;
            
            // будет (0, 0) если точка лежит внутри прямоугольника, но численные методы должны это исключить
            XY closestPoint = new XY(Mathf.Clamp(newPosition.X, left, right), Mathf.Clamp(newPosition.Y, bottom, top));
            
            return new NCollision(offset, newPosition - closestPoint, c, b);
        }


        public static NCollision FlyInto (BoxCollider a, BoxCollider b, XY v) {
            // float hw = 0.5f * (a.Right - a.Left + b.Right - b.Left);
            // float hh = 0.5f * (a.Top - a.Bottom + b.Top - b.Bottom);
            // float dx = 0.5f * (b.Left - a.Left + b.Right - a.Right);
            // float dy = 0.5f * (b.Bottom - a.Bottom + b.Top - a.Top);

            float top = b.Top - a.Bottom; //dy + hh;
            float left = b.Left - a.Right; //dx - hw;
            float right = b.Right - a.Left; //dx + hw;
            float bottom = b.Bottom - a.Top; //dy - hh;

            float min = 1;
            if (v.X > 0) {
                float d = Geom.ORayToVertical(left, v.X);
                float y = v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) min = d;
            }
            if (v.X < 0) {
                float d = Geom.ORayToVertical(right, v.X);
                float y = v.Y * d;
                if (d >= 0 && d < min && y >= bottom && y <= top) min = d;
            }
            if (v.Y > 0) {
                float d = Geom.ORayToHorizontal(bottom, v.Y);
                float x = v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) min = d;
            }
            if (v.Y < 0) {
                float d = Geom.ORayToHorizontal(top, v.Y);
                float x = v.X * d;
                if (d >= 0 && d < min && x >= left && x <= right) min = d;
            }

            var offset = v * min;
            if (offset.Y + a.Top > b.Bottom &&
                offset.X + a.Left < b.Right &&
                offset.X + a.Right > b.Left &&
                offset.Y + a.Bottom < b.Top) {
                // если нет перекрывания
                return min == 1
                    ? new NCollision(offset, XY.NaN, null, null)
                    : new NCollision(offset, Geom.BoxQuarter(offset, left, right, bottom, top), a, b);
            }

            // включаем численные методы
            float lo = 0;
            float hi = min;
            float l = v.Length;
            for (int i = 0; i < 10 && l * (hi - lo) > Epsilon; i++) {
                float mid = 0.5f * (lo + hi);
                offset = v * mid;
                if (offset.Y + a.Top > b.Bottom &&
                    offset.X + a.Left < b.Right &&
                    offset.X + a.Right > b.Left &&
                    offset.Y + a.Bottom < b.Top) {
                    hi = mid;
                } else {
                    lo = mid;
                }
            }
            
            return new NCollision(v * lo, Geom.BoxQuarter(offset, left, right, bottom, top), a, b);
        }
    }
}
