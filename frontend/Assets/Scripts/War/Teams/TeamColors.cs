using System.Collections.Generic;
using UnityEngine;


namespace War.Teams {

    public class TeamColors {

        private static List<Color> _colors = new List<Color> {
            new Color(1.0f, 0.6f, 0.2f),
            new Color(0.2f, 0.6f, 1.0f),
            new Color(0.4f, 1.0f, 0.4f),
            new Color(1.0f, 0.6f, 1.0f),
            new Color(1.0f, 1.0f, 0.4f)
        };

        public static List<Color> Colors {
            get { return _colors; }
        }

    }

}
