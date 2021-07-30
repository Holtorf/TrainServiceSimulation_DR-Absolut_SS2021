using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    /// <summary>
    /// Class to manage all the importent informations for the A* Algorythm  
    /// that is used to find the path for the FTS
    /// </summary>
    public class SearchGraphManager : MonoBehaviour
    {

        /// <summary>
        /// Connection for the Waypoints in the Insepctor
        /// is needed for the SearchGraphManagerEditor
        /// </summary>
        [SerializeField, HideInInspector]
        private WayPoint[] _wayPoints;

        /// <summary>
        /// calculated the path cost between the waypoint 
        /// </summary>
        public void CalculatePathCost()
        {
            _wayPoints = GetComponentsInChildren<WayPoint>();
            foreach (WayPoint wayPoint in _wayPoints)
            {
                wayPoint.CalculatePathCost();
            }
        }

        /// <summary>
        /// function to search for the nearst waypoint of an object
        /// </summary>
        public WayPoint GetClosestWayPoint(Vector3 position)
        {
            float wayPointDistance = float.MaxValue;
            WayPoint nearestWayPoint = null;

            for(int i = 0; i < _wayPoints.Length; i++)
            {
                float nextWayPoint = (_wayPoints[i].transform.position - position).sqrMagnitude;

                if(nextWayPoint < wayPointDistance)
                {
                    wayPointDistance = nextWayPoint;
                    nearestWayPoint = _wayPoints[i];
                }
            }

            return nearestWayPoint;
        }

        // ToDO: Hier performance checken ob mit amgnitude oder Vector3.Distance
        /// <summary>
        /// function to calculated the heuristic everytime the FTS get a new destination
        /// </summary>
        public void CalculateHeuristic(Vector3 endPoint)
        {
            for(int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Heuristic = (_wayPoints[i].transform.position - endPoint).magnitude;
            }
        }

        /// <summary>
        /// function to reset the path between the Waypoint
        /// it´s needed so the FTS don´t get the wrong way for the next job
        /// </summary>
        public void SetWayPointsBack()
        {
            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].ResetPath();
            }
        }

    }
}