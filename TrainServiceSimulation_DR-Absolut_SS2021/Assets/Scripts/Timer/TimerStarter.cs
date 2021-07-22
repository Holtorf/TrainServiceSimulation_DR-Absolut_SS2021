using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation;

namespace TrainServiceSimulation
{

    public class NewBehaviourScript : MonoBehaviour
    {
        [SerializeField]
        private TimeManger _timeM;

        private void OnTriggerEnter(Collider StartGame)
        {
            _timeM.IsTimerRunning = true;
            _timeM.StartTime = Time.time;

        }
    }

}
