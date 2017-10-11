using UnityEngine;

namespace Geometry {

    public static class Angles {

        public static void ToLookAngle (ref float angle, ref bool isRight, float lerpCoeff = 1f) {
            if (isRight) {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, 0)) < 90) {
                    isRight = true;
                    angle = Mathf.LerpAngle(0, angle, lerpCoeff);
                } else {
                    isRight = false;
                    angle = Mathf.LerpAngle(0, angle-180, lerpCoeff);
                }
            } else {
                if (Mathf.Abs(Mathf.DeltaAngle(angle, 180)) < 90) {
                    isRight = true;
                    angle = Mathf.LerpAngle(0, -angle - 180, lerpCoeff);
                } else {
                    isRight = false;
                    angle = Mathf.LerpAngle(0, -angle, lerpCoeff);
                }
            }
        }

    }

}
