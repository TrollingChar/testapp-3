﻿using System.Collections.Generic;
using UnityEngine;


namespace Battle.Teams {

    public class TeamColors {

        private static readonly List<Color> _colors = new List<Color> {
            new Color(1.0f, 0.2f, 0.2f),
            new Color(0.4f, 0.7f, 1.0f),
            new Color(0.4f, 1.0f, 0.4f),
            new Color(1.0f, 0.6f, 1.0f),
            new Color(1.0f, 1.0f, 0.5f),
            new Color(1.0f, 0.7f, 0.4f),
//            new Color(0.4f, 1.0f, 1.0f),
//            new Color(0.7f, 0.6f, 1.0f),
//            new Color(0.9f, 0.8f, 0.6f),
//            new Color(0.7f, 0.8f, 0.5f),
//            new Color(0.8f, 0.8f, 0.8f),
        };

        public static List<Color> Colors {
            get { return _colors; }
        }

    }

}
