using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.Train
{
    public class SimulationStart : MonoBehaviour
    {
        [SerializeField]
        private AppManager _appM;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StartGame"))
            {
                _appM.StartCoroutine(_appM.StartSequence());
            }

        }
    }
}