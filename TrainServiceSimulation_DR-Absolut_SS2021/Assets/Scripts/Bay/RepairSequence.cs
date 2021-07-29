using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TrainServiceSimulation.Train;

namespace TrainServiceSimulation.Bay
{
    public class RepairSequence : MonoBehaviour
    {
        [SerializeField]
        private TrainManager _trainM;

        private UnityEvent _sequenceFinishedEvent;

        private bool _trainReachedDestination = false;
        private bool _trainReachedOrigin = false;
        private bool _maintenanceFinished = false;

        public void Awake()
        {
            _sequenceFinishedEvent = new UnityEvent();
        }

        public void Begin()
        {
            StartCoroutine(PlaySequence());
        }
        
        public void Init()
        {
            _trainReachedDestination = false;
            _trainReachedOrigin = false;
            _maintenanceFinished = false;
            _trainM.InitTrain();
            _trainM.AddListenerTrainReachedDestinationEvent(OnTrainReachedDestination);
            _trainM.AddListenerTrainReachedOriginEvent(OnTrainReachedOrigin);
            _trainM.AddListenerMaintenanceFinishedEvent(OnMaintenanceFinished);
            
        }

        IEnumerator PlaySequence()
        {
            _trainM.MoveTrainToDestination();
            yield return new WaitUntil(() => _trainReachedDestination);
            _trainM.StartMaintenance();
            yield return new WaitUntil(() => _maintenanceFinished);
            _trainM.MoveTrainToOrigin();
            yield return new WaitUntil(() => _trainReachedOrigin);
            _sequenceFinishedEvent.Invoke();

        }

        void OnTrainReachedDestination()
        {
            _trainReachedDestination = true;
        }

        void OnTrainReachedOrigin()
        {
            _trainReachedOrigin = true;
        }

        void OnMaintenanceFinished()
        {
            _maintenanceFinished = true;
        }

        public void AddListenerSequenceFinishedEvent(UnityAction call)
        {
            _sequenceFinishedEvent.AddListener(call);
        }
        public void RemoveListenerSequenceFinishedEvent(UnityAction call)
        {
            _sequenceFinishedEvent.RemoveListener(call);
        }
    }
}