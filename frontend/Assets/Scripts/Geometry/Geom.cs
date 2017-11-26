namespace Geometry {

    public static class Geom {

        // вместо x можно y и норм
        public static float RayTo1D (float ox, float dirX, float targX) {
            return (targX - ox) / dirX;
        }

    }

}
