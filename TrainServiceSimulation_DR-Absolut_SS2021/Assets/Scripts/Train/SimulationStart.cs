using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.Train
{
    public class SimulationStart : MonoBehaviour
    {
        [SerializeField]
        private AppManager _appM;

        private bool _simulationIsRunning = false;

        public bool SimulationIsRunning { get => _simulationIsRunning; set => _simulationIsRunning = value; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StartGame"))
            {
                _appM.StartCoroutine(_appM.StartSequence());
                _simulationIsRunning = true;
            }

        }
    }
}