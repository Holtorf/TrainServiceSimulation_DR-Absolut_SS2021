using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    public class SearchGraphManager : MonoBehaviour
    {

        [SerializeField, HideInInspector]
        private WayPoint[] _wayPoints;

        public void CalculatePathCost()
        {
            _wayPoints = GetComponentsInChildren<WayPoint>();
            foreach (WayPoint wayPoint in _wayPoints)
            {
                wayPoint.CalculatePathCost();
            }
        }

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
        public void CalculateHeuristic(Vector3 endPoint)
        {
            for(int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].Heuristic = (_wayPoints[i].transform.position - endPoint).magnitude;
            }
        }

        public void SetWayPointsBack()
        {
            for (int i = 0; i < _wayPoints.Length; i++)
            {
                _wayPoints[i].ResetPath();
            }
        }

    }
}