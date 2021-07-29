using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    public class AStar : MonoBehaviour
    {

        [SerializeField]
        private SearchGraphManager _searchGraphManager;

        private WayPoint[] _wayPoints;

        private List<WayPoint> _openList;
        private HashSet<WayPoint> _closedList;

        private List<Vector3> _shortestPath;

        public List<Vector3> ShortestPath { get => _shortestPath; set => _shortestPath = value; }

        private void Awake()
        {
            _wayPoints = GetComponentsInChildren<WayPoint>();

            _openList = new List<WayPoint>();
            _closedList = new HashSet<WayPoint>();
            _shortestPath = new List<Vector3>();

        }

        public void FindPath(WayPoint startPoint, WayPoint endPoint)
        {
            _searchGraphManager.SetWayPointsBack();
            _openList.Clear();
            _closedList.Clear();
            
            _searchGraphManager.CalculateHeuristic(endPoint.transform.position);
            WayPoint currentPoint = startPoint;
            WayPoint currentNeighbour;

            while (currentPoint != null && currentPoint != endPoint)
            {
                for (int i = 0; i < currentPoint.Neighbours.Length; i++)
                {

                    currentNeighbour = currentPoint.Neighbours[i];
                    
                    if (_closedList.Contains(currentPoint.Neighbours[i]))
                    {
                        continue;
                    }
                    currentNeighbour.GValue = currentPoint.GValue + currentPoint.ParthCosts[i];
                    double  newFValue = currentNeighbour.GValue + currentNeighbour.Heuristic;

                    if (newFValue < currentNeighbour.FValue)
                    {
                        currentNeighbour.FValue = newFValue;
                        currentNeighbour.Parent = currentPoint;

                        if (!_openList.Contains(currentNeighbour))
                        {
                            _openList.Add(currentNeighbour);
                        }

                    }

                }

                _closedList.Add(currentPoint);
                
                if (_openList.Count != 0)
                {
                    currentPoint = _openList[0];
                    _openList.Remove(currentPoint);
                }
                else
                {
                    currentPoint = null;
                }

                _openList.Sort((wp1, wp2) => wp1.FValue.CompareTo(wp2.FValue));

            }

            _shortestPath.Clear();
            DrawCorridor(endPoint);

        }

        private void DrawCorridor(WayPoint wayPoint)
        {
            _shortestPath.Insert(0,wayPoint.transform.position);
            if (wayPoint.Parent != null)
            {
                DrawCorridor(wayPoint.Parent);
            }
        } 

    }
}

