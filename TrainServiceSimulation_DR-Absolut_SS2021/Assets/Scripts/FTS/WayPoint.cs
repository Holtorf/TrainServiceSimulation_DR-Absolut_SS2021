using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.FTS
{

    public class WayPoint : MonoBehaviour
    {
        [SerializeField]
        private WayPoint[] _neighbours;

        //TODO: .HideInInspector
        [SerializeField]
        private double[] _parthCosts;

        private double _gValue;

        private double _heuristic;

        private double _fValue = double.MaxValue;

        private WayPoint _parent;

        public double GValue { get => _gValue; set => _gValue = value; }
        public double Heuristic { get => _heuristic; set => _heuristic = value; }
        public WayPoint[] Neighbours { get => _neighbours;}
        public double[] ParthCosts { get => _parthCosts; }
        public double FValue { get => _fValue; set => _fValue = value; }
        public WayPoint Parent { get => _parent; set => _parent = value; }

        public void CalculatePathCost()
        {
            _parthCosts = new double[_neighbours.Length];

            for (int i = 0; i < _neighbours.Length; i++)
            {
                _parthCosts[i] = (_neighbours[i].transform.position - transform.position).magnitude;
            }
        }

        public void ResetPath()
        {
            _gValue = 0;
            _heuristic = 0;
            _fValue = double.MaxValue;
            _parent = null;
        }

    }

}