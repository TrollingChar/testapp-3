﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace W3 {
    public class Worm : Object {
        public Worm (XY position) {
            this.position = position;
        }

        public override void Detonate () {
            base.Detonate();
        }

        protected override void InitColliders () {
        }

        protected override void InitController () {
        }

        protected override void InitSprite () {
        }

        protected override void RemoveSprite () {
        }

        protected override bool PassableFor (Object o) {
            return this == o;
        }
    }
}