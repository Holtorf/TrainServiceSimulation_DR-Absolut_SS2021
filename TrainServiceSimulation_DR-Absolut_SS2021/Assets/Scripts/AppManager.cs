using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;
using TrainServiceSimulation.FTS;
using TrainServiceSimulation.UI;

namespace TrainServiceSimulation
{
    public class AppManager : MonoBehaviour
    {
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

        public IEnumerator StartSequence()
        {
            if (!_noActiveTrain)
            {
                _noActiveTrain = true;
                _repairS.AddListenerSequenceFinishedEvent(OnRepairSequenceFinished);
                Debug.Log("I got Started");

                if (UseTime)
                {
                    Debug.Log("Ich bin in der Zeit schleife");

                    if (_timeManager.Timeing <= _simulationLimitation.TimeLimit*60)
                    {
                        yield return new WaitForSeconds(1f);
                        _repairS.Init();
                        _repairS.Begin();
                    }

                }
                else if (UseTrain)
                {
                    if (WagonCount < _simulationLimitation.TrainLimit)
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

