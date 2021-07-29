using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;

namespace TrainServiceSimulation
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField]
        private TrainManager _trainM;
        [SerializeField]
        private RepairSequence _repairS;

        private bool _gameStarted = false;

        public bool GameStarted { get => _gameStarted; set => _gameStarted = value; }

        public void Start()
        {
            StartCoroutine(StartSequence());
        }

        IEnumerator StartSequence()
        {
            //_repairS.AddListenerSequenceFinishedEvent(OnRepairSequenceFinished);
            //yield return new WaitUntil(() => GameStarted == true);
            yield return new WaitForSeconds(1f); 
            Debug.Log("GameStarted");
            _repairS.Init();
            _repairS.Begin();
        }

        public void OnRepairSequenceFinished()
        {
            Debug.Log("RepairSequenceFinished");
            _trainM.DestroyTrain();
            StartCoroutine(StartSequence());
        }
    }
}

