using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TrainServiceSimulation.Train;

namespace TrainServiceSimulation.UI
{

    /// <summary>
    /// Function that indicates when the simulation has to stop
    /// </summary>
    public class SimulationLimitation : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _trainLimitTMPro;
        [SerializeField]
        private TextMeshProUGUI _timeLimitTMPro;

        [SerializeField]
        private SimulationStart _simulationStart;

        [SerializeField]
        private Slider _trainLimitSlider;

        [SerializeField]
        private Slider _timeLimitSlider;

        private int _wagonLimit;

        private float _timeLimit = 100;

        public int WagonLimit { get => _wagonLimit; set => _wagonLimit = value; }
        public float TimeLimit { get => _timeLimit; set => _timeLimit = value; }

        /// <summary>
        /// Slider Event that changed the limit when the simulation has to stop after a number of wagons
        /// </summary>
        public void TrainLimitChange()
        {
            if (_simulationStart.SimulationIsRunning == false)
            {
                _wagonLimit = (int)_trainLimitSlider.value;
                _trainLimitTMPro.text = "Max. Wagons: " + _wagonLimit;
            }

        }

        /// <summary>
        /// Slider Event that changed the limit when the simulation has to stop after a time
        /// </summary>
        public void TimeLimitChange()
        {
            if (_simulationStart.SimulationIsRunning == false)
            {
                _timeLimit = (int)_timeLimitSlider.value;
                _timeLimitTMPro.text = "Max. Time:" + _timeLimit;
            }
        }
    }
}
