using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using TrainServiceSimulation.Train;

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

        private int _trainLimit;

        private float _timeLimit = 100;

        public int TrainLimit { get => _trainLimit; set => _trainLimit = value; }
        public float TimeLimit { get => _timeLimit; set => _timeLimit = value; }

        public void TrainLimitChange()
        {
            if (_simulationStart.SimulationIsRunning == false)
            {
                _trainLimit = (int)_trainLimitSlider.value;
                _trainLimitTMPro.text = "Max. Trains: " + _trainLimit;
            }
            
        }

        public void TimeLimitChange()
        {
            if (_simulationStart.SimulationIsRunning == false)
            {
                _timeLimit = (int)_timeLimitSlider.value;
                _timeLimitTMPro.text = "Max. Time:" + _timeLimit;
            }
        }
}
