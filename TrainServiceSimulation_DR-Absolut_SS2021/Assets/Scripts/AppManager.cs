using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;
using TrainServiceSimulation.FTS;

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

        private bool _gameStarted = false;

        public bool GameStarted { get => _gameStarted; set => _gameStarted = value; }

        private void Awake()
        {
            StartCoroutine(StartSequence());
        }

        IEnumerator StartSequence()
        {
            _repairS.AddListenerSequenceFinishedEvent(OnRepairSequenceFinished);
            //yield return new WaitUntil(() => GameStarted == true);
            yield return new WaitForSeconds(5f); 
            _repairS.Init();
            _repairS.Begin();
        }

        public void OnRepairSequenceFinished()
        {
            StopCoroutine(StartSequence());
            _trainM.DestroyTrain();
            
            StartCoroutine(StartSequence());
        }
    }
}

