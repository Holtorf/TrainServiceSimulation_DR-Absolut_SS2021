using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    /// <summary>
    /// Class that implement the A* Algorithm for the 
    /// FTS system
    /// </summary>
    public class AStar : MonoBehaviour
    {

        [SerializeField]
        private SearchGraphManager _searchGraphManager;

        /// <summary>
        /// Array of the Waypoints for the FindPath function
        /// </summary>
        private WayPoint[] _wayPoints;

        /// <summary>
        /// List with the waypoints that give the A* ALgorithm the 
        /// best way to the goal
        /// </summary>
        private List<WayPoint> _openList;
        /// <summary>
        /// HashSet with all the waypoints that are not optimal for 
        /// the way to the goal
        /// </summary>
        private HashSet<WayPoint> _closedList;

        /// <summary>
        /// Liste of positions from the Waypoints that the FTS needed 
        /// to get the shortest way to the goal of it
        /// </summary>
        private List<Vector3> _shortestPath;

        public List<Vector3> ShortestPath { get => _shortestPath; set => _shortestPath = value; }

        /// <summary>
        /// To initiate the diffrent variables 
        /// </summary>
        private void Awake()
        {
            _wayPoints = GetComponentsInChildren<WayPoint>();

            _openList = new List<WayPoint>();
            _closedList = new HashSet<WayPoint>();
            _shortestPath = new List<Vector3>();

        }

        /// <summary>
        /// The main function of the class
        /// its get the start point and the end point from the FTS-Manager to calculate the best way
        /// through the hall for the FTS
        /// For that the function is going from Waypoint to Waypoint and is saving the way between 
        /// them that has the lowest cost.
        /// </summary>
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

