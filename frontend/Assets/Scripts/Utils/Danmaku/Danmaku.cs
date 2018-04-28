using Geometry;
using UnityEngine;
using Utils.Random;


namespace Utils.Danmaku {

    public static class Danmaku {

        public static XY[] Ring (XY dir, int bullets) {
            float step = Mathf.PI * 2 / bullets;
            var arr = new XY[bullets];
            for (int i = 0; i < bullets; i++) {
                arr[i] = dir.Rotated(step * i);
            }
            return arr;
        }


        public static XY[] Cloud (float radius, int bullets) {
            var arr = new XY[bullets];
            for (int i = 0; i < bullets; i++) {
                arr[i] = CloudParticle(radius);
            }
            return arr;
        }


        public static XY CloudParticle (float radius) {
            float f = 1 - RNG.Float() * RNG.Float() * RNG.Float();
            return XY.Polar(f * radius, RNG.Angle());
        }


        public static XY[] Scatter (XY dir, float cone, int bullets) {
            if (bullets <= 1) return new[] {dir};

            var arr = new XY[bullets];
            float step = cone / (bullets - 1);
            float half = cone * 0.5f;
            for (int i = 0; i < bullets; i++) {
                arr[i] = dir.Rotated(step * i - half);
            }
            return arr;
        }


        public static XY[] Line (XY dir, float minCoeff, float maxCoeff, int bullets) {
            var arr = new XY[bullets];
            float div = bullets - 1;
            for (int i = 0; i < bullets; i++) {
                arr[i] = dir * Mathf.Lerp(minCoeff, maxCoeff, i / div);
            }
            return arr;
        }


        public static XY[] Shotgun (XY dir, float cone, float minCoeff, float maxCoeff, int bullets) {
            var arr = new XY[bullets];
            for (int i = 0; i < bullets; i++) {
                arr[i] = ShotgunBullet(dir, cone, minCoeff, maxCoeff);
            }
            return arr;
        }


        private static XY ShotgunBullet (XY dir, float cone, float minCoeff, float maxCoeff) {
            dir.Rotate(cone * RNG.SignedFloat());
            return dir * Mathf.Lerp(minCoeff, maxCoeff, RNG.Float());
        }

    }

}
