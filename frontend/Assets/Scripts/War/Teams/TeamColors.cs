using System.Collections.Generic;
using UnityEngine;


namespace War.Teams {

    public class TeamColors {

        private static List<Color> _colors = new List<Color> {
            new Color(0.8f, 0.1f, 0.1f),
            new Color(0.1f, 0.3f, 0.8f),
            new Color(0.1f, 0.5f, 0.1f),
            new Color(0.6f, 0.1f, 0.8f),
            new Color(0.2f, 0.2f, 0.2f)
        };

        public static List<Color> Colors {
            get { return _colors; }
        }

    }

}
