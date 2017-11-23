﻿using System;
using Battle.Physics.Collisions;
using UnityEngine;
using BoxCollider = Battle.Physics.Collisions.BoxCollider;


namespace Geometry {

    // all of these must obey the 3 laws!
    public static class Physics {

        private const float Epsilon = 0.001f;
        private const float SqrEpsilon = Epsilon * Epsilon;


        public static bool Overlap (Circle a, Circle b) {
            float rr = a.Radius + b.Radius;
            return XY.SqrDistance(a.Center, b.Center) < rr * rr;
        }


        public static bool Overlap (Circle c, Box b) {
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


        public static bool Overlap (Box a, Box b) {
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


        public static NCollision FlyInto (Circle a, Circle b, XY v) {
            // вычисляем значение
            float dist = Geom.RayToCircle(a.Center, v, b.Center, a.Radius + b.Radius);

            float r2 = a.Radius + b.Radius;
            r2 *= r2;
            if (float.IsNaN(dist) || dist < 0 || dist * dist >= v.SqrLength) {
                // нет коллизии, проверить перекрывание
                if (XY.SqrDistance(a.Center + v, b.Center) < r2) return new NCollision(v, XY.NaN, null, null);
                dist = v.Length;
            }
            // есть коллизия, проверить перекрывание
            else if (XY.SqrDistance(a.Center + v.WithLength(dist), b.Center) < r2) return new NCollision(v, XY.NaN, null, null);

            // численными методами делаем чтобы не было ошибки с перекрыванием
            float lo = 0;
            float hi = dist;
            for (int i = 0; i < 10 && hi - lo > Epsilon; i++) {
                float mid = 0.5f * (lo + hi);
                if (XY.SqrDistance(a.Center + v.WithLength(mid), b.Center) < r2) {
                    lo = mid;
                } else {
                    hi = mid;
                }
            }
            var offset = v.WithLength(lo);
            return new NCollision(offset, a.Center - b.Center + offset, a, b);
        }


        public static NCollision FlyInto (Circle c, Box b, XY v) {
            // сначала стороны, потом углы, потом численными методами доделать
            float min = 1;
            if (v.X > 0) {
                float d = Geom.RayToVertical(c.Center.X, b.Left, v.X);
                float y = c.Center.Y + v.Y * d;
                if (d >= 0 && d < min && y >= b.Bottom && y <= b.Top) min = d;
            }
            if (v.X < 0) {
                float d = Geom.RayToVertical(c.Center.X, b.Right, v.X);
                float y = c.Center.Y + v.Y * d;
                if (d >= 0 && d < min && y >= b.Bottom && y <= b.Top) min = d;
            }
            if (v.Y > 0) {
                float d = Geom.RayToHorizontal(c.Center.Y, b.Bottom, v.Y);
                float x = c.Center.X + v.X * d;
                if (d >= 0 && d < min && x >= b.Left && x <= b.Right) min = d;
            }
            if (v.Y < 0) {
                float d = Geom.RayToHorizontal(c.Center.Y, b.Top, v.Y);
                float x = c.Center.X + v.X * d;
                if (d >= 0 && d < min && x >= b.Left && x <= b.Right) min = d;
            }
            // если нет столкновений то min будет 1
            float l = v.Length;
            float minDist = min * l;
            if (v.X > 0 || v.Y > 0) {
                float dist = Geom.RayToCircle(c.Center, v, new XY(b.Left, b.Bottom), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }
            if (v.X < 0 || v.Y > 0) {
                float dist = Geom.RayToCircle(c.Center, v, new XY(b.Right, b.Bottom), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }
            if (v.X < 0 || v.Y < 0) {
                float dist = Geom.RayToCircle(c.Center, v, new XY(b.Right, b.Top), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }
            if (v.X > 0 || v.Y < 0) {
                float dist = Geom.RayToCircle(c.Center, v, new XY(b.Left, b.Top), c.Radius);
                if (dist >= 0 && dist < minDist) minDist = dist;
            }

//            XY offset = v.WithLength(minDist);
            if (minDist == l) {
                XY newPosition = c.Center + v;
                // todo: clamp to rect then calc position
                if (TODO) return new NCollision(v, XY.NaN, null, null);
            } else {
                XY newPosition = c.Center + v.WithLength(minDist);
                // todo: clamp to rect then calc position
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
            XY newPosition = c.Center + offset;
            
            // будет (0, 0) если точка лежит внутри прямоугольника, но численные методы должны это исключить
            XY closestPoint = new XY(Mathf.Clamp(newPosition.X, b.Left, b.Right), Mathf.Clamp(newPosition.Y, b.Bottom, b.Top));
            
            return new NCollision(offset, newPosition - closestPoint, c, b);
        }


        public static NCollision FlyInto (Box a, Box b, XY v) {
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
