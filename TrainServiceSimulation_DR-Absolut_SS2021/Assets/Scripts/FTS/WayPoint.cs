using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{
    /// <summary>
    /// Class to manage a Waypoint
    /// </summary>
    public class WayPoint : MonoBehaviour
    {
        /// <summary>
        /// Array of the nearest Waypoints
        /// </summary>
        [SerializeField]
        private WayPoint[] _neighbours;

        /// <summary>
        /// Array of the path costs to the nearest Waypoints
        /// same order than _neighbours[]
        /// </summary>
        //[ReadOnly]
        [SerializeField]
        private double[] _parthCosts;

        /// <summary>
        /// doubles for the path calculation
        /// </summary>
        private double _gValue;
        private double _heuristic;
        private double _fValue = double.MaxValue;

        /// <summary>
        /// The WayPoint from what the FTS was comming;
        /// </summary>
        private WayPoint _parent;

        /// <summary>
        /// Getter and Setter for the variables above
        /// </summary>
        public double GValue { get => _gValue; set => _gValue = value; }
        public double Heuristic { get => _heuristic; set => _heuristic = value; }
        public WayPoint[] Neighbours { get => _neighbours;}
        public double[] ParthCosts { get => _parthCosts; }
        public double FValue { get => _fValue; set => _fValue = value; }
        public WayPoint Parent { get => _parent; set => _parent = value; }

        /// <summary>
        /// function to calculate the path cost between the WayPoints
        /// this is only used in the Editor
        /// </summary>
        public void CalculatePathCost()
        {
            _parthCosts = new double[_neighbours.Length];

            for (int i = 0; i < _neighbours.Length; i++)
            {
                _parthCosts[i] = (_neighbours[i].transform.position - transform.position).magnitude;
            }
        }

        /// <summary>
        /// function to reset all values except the path cost and the neighbours
        /// </summary>
        public void ResetPath()
        {
            _gValue = 0;
            _heuristic = 0;
            _fValue = double.MaxValue;
            _parent = null;
        }

    }

}