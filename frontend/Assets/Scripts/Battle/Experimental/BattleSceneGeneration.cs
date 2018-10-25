using System;
using Battle.Terrain.Generation;


namespace Battle.Experimental {

    public partial class NewBattleScene {

        private EstimatedLandGen _landGen;


        private void StartGeneration () {
            _landGen =
            new EstimatedLandGen (
                new LandGen (
                    new byte[,] {
                        {0, 0, 0, 0, 0},
                        {0, 1, 1, 1, 0},
                        {0, 1, 0, 1, 0}
                    }
                )
            ).
            SwitchDimensions ().
            Expand (7).
            Cellular (0x01e801d0, 20).
            Cellular (0x01f001e0).
            Expand ().
            Cellular (0x01e801d0, 20).
            Cellular (0x01f001e0).
            Rescale (2000, 1000).
            Cellular (0x01f001e0);

            _landGen.OnProgress += OnProgress;
            _landGen.OnComplete += OnComplete; // maybe the world should handle this?
            _landGen.Generate (this);
        }


        private void OnProgress (float progress) {
            Hint.text = "Прогресс генерации: " + (int) progress + "%";
        }


        private void OnComplete (LandGen landGen) {
            _landGen.OnProgress -= OnProgress;
            _landGen.OnComplete -= OnComplete;

            StartGame (landGen);
        }


    }

}