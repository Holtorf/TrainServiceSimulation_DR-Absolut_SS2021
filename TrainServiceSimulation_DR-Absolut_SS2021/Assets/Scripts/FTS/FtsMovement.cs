using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    public class FtsMovement : MonoBehaviour
    {
        [SerializeField]
        private SearchGraphManager _searchGraphManager;

        [SerializeField]
        private AStar _aStar;

        [SerializeField]
        private bool drawLine;

        [SerializeField]
        private float _speed = 50;

        [SerializeField]
        private bool _isMoving;

        private float _movement;

        public bool IsMoving { get => _isMoving;}

        public void DriveToDestination(Vector3 endPoint)
        {
            WayPoint start = _searchGraphManager.GetClosestWayPoint(transform.position);
            WayPoint end = _searchGraphManager.GetClosestWayPoint(endPoint);
            _aStar.FindPath(start, end);

            MoveFTS(1);
            _isMoving = true;

            if (drawLine)
            {
                for (int i = 0; i < _aStar.ShortestPath.Count - 1; i++)
                {

                    Debug.DrawLine(_aStar.ShortestPath[i], _aStar.ShortestPath[i + 1], Color.red, 100f);
                }
            }
        }

        private void MoveFTS(int index)
        {
            if(index >= _aStar.ShortestPath.Count)
            {
                _isMoving = false;
                return;
            }
            Vector3 nextPosition = _aStar.ShortestPath[index];
            index++;
            _movement = (nextPosition - transform.position).magnitude / _speed;
            LeanTween.move(gameObject,  nextPosition, _movement).setOnComplete(() => MoveFTS(index));
        }
            
    }
}


