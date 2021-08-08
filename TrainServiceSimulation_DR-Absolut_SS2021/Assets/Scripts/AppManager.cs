using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;
using TrainServiceSimulation.FTS;
using TrainServiceSimulation.UI;
using TrainServiceSimulation.Timer;

namespace TrainServiceSimulation
{
    /// <summary>
    /// The Main Class for the Simulation
    /// it has all connection to the managers an started and end the simulation
    /// </summary>
    public class AppManager : MonoBehaviour
    {
        /// <summary>
        /// Connection to other classes
        /// </summary>
        [SerializeField]
        private TrainManager _trainM;
        [SerializeField]
        private RepairSequence _repairS;
        [SerializeField]
        private FtsManager _ftsManager;
        [SerializeField]
        private SimulationLimitation _simulationLimitation;
        [SerializeField]
        private TimeManger _timeManager;

        /// <summary>
        /// Connection to the Train Count text to change it at the end
        /// of every Train lifecyrcle
        /// </summary>
        [SerializeField]
        private TextMeshProUGUI _trainCounterTMPro;

        private int _wagonCount;

        [ReadOnly]
        [SerializeField]
        private bool _useTime = false;

        [ReadOnly]
        [SerializeField]
        private bool _useTrain = false;
        private bool _noActiveTrain = false;

        public bool UseTime { get => _useTime; set => _useTime = value; }
        public bool UseTrain { get => _useTrain; set => _useTrain = value; }
        public int WagonCount { get => _wagonCount; set => _wagonCount = value; }

        /// <summary>
        /// Update is called once per frame.
        /// Check for the time limitation, if the time is over the limitation it will stop
        /// the simulation.
        /// </summary>
        private void Update()
        {
            if(_timeManager.Timeing >= _simulationLimitation.TimeLimit*60 && _useTime == true)
            {
                _timeManager.IsTimerRunning = false;
                Time.timeScale = 0;
                StopAllCoroutines();
                _trainCounterTMPro.text = "Number of Repaired Wagons:" + WagonCount;
            }
        }

        /// <summary>
        /// Coroutine that starts the entire Sequence
        /// it can only do something when no train is in the scene and the user
        /// has checked one of the to limitations
        /// </summary>
        public IEnumerator StartSequence()
        {
            if (!_noActiveTrain)
            {
                _noActiveTrain = true;
                _repairS.AddListenerSequenceFinishedEvent(OnRepairSequenceFinished);

                if (UseTime)
                {

                    if (_timeManager.Timeing <= _simulationLimitation.TimeLimit*60)
                    {
                        yield return new WaitForSeconds(1f);
                        _repairS.Init();
                        _repairS.Begin();
                    }

                }
                else if (UseTrain)
                {
                    if (WagonCount < _simulationLimitation.WagonLimit)
                    {
                        yield return new WaitForSeconds(1f);
                        _repairS.Init();
                        _repairS.Begin();
                    }
                    else
                    {
                        _timeManager.IsTimerRunning = false;
                        StopAllCoroutines();
                    }

                }
            }
            

        }

        /// <summary>
        /// last event of ever train lifecyrcle 
        /// its activate the destroy function set the wagon count up, stop the start coroutine 
        /// and started it at the end of the function
        /// </summary>
        public void OnRepairSequenceFinished()
        {
            _noActiveTrain = false;
            StopCoroutine(StartSequence());
            _trainM.DestroyTrain();
            _trainCounterTMPro.text = "Number of Repaired Wagons:" + WagonCount;
            StartCoroutine(StartSequence());
        }
    }
}

