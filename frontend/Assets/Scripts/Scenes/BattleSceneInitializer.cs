using UnityEngine;
using War;
using Zenject;


namespace Scenes {

    public class BattleSceneInitializer : MonoBehaviour {

        [Inject] private BF _bf;
        [Inject] private GameInitData _data;


        private void Start () {
//            _bf.StartGame(_data);
        }

    }

}
