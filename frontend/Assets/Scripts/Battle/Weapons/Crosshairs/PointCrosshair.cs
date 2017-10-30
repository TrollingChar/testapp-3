using Geometry;
using UnityEngine;

namespace Battle.Weapons.Crosshairs
{
    public class PointCrosshair : MonoBehaviour
    {
        public enum Direction { None, Left, Right }
        
        [SerializeField] private GameObject _arrowPrefab;
        [SerializeField] private GameObject _squarePrefab;
        private GameObject _square;
        [SerializeField] private float _firstArrowOffset;
        [SerializeField] private float _lastArrowOffset;
        [SerializeField] private int _arrows;

        private void Awake()
        {
            // todo
        }

        public Direction Type
        {
            set
            {
                // todo
            }
        }

        public XY Point
        {
            set
            {
                // todo
            }
        }
    }
}