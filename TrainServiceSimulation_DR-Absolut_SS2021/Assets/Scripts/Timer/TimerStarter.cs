using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation;

namespace TrainServiceSimulation
{
    /// <summary>
    /// Class to start the timer after a Trigger Enter
    /// </summary>
    
    public class TimerStarter : MonoBehaviour
    {
        /// <summary>
        /// Connection to the TimeManager for the bool IsTimmerRunning && the float StartTime
        /// </summary>
        [SerializeField]
        private TimeManger _timeM;

        /// <summary>
        /// OnTriggerEnter function to change values
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("StartGame"))
            {
                _timeM.IsTimerRunning = true;
                _timeM.StartTime = Time.time;
            }
        }
    }

}
