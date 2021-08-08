using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    /// <summary>
    /// Class to controle how the FTS ist moving through the scene
    /// </summary>
    public class FtsMovement : MonoBehaviour
    {

        [SerializeField]
        private SearchGraphManager _searchGraphManager;
        [SerializeField]
        private AStar _aStar;
        
        [SerializeField]
        private bool drawLine = true;

        /// <summary>
        /// speedmodifier for the FTS movement
        /// </summary>
        [SerializeField]
        private float _speed = 30;

        /// <summary>
        /// bool to check if the FTS is moving
        /// this check is importend so the FTS get no new Jobs when its moving
        /// </summary>
        [SerializeField]
        private bool _isMoving;

        /// <summary>
        ///  the variable with what the FTS is moving through the scene
        /// </summary>
        private float _movement;

        public bool IsMoving { get => _isMoving;}

        /// <summary>
        /// Function that is giving getting the positions from the _searchGraphManager() of the start and end point and give it to the
        /// FindPath() from the AStar class
        /// && drawing the shortest path for the user
        /// </summary>
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

        /// <summary>
        /// Function that is moving the FTS along the shortestPath of the AStar Class
        /// </summary>
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


