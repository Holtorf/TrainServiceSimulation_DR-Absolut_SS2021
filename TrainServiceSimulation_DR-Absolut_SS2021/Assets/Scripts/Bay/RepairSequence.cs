using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TrainServiceSimulation.Train;

namespace TrainServiceSimulation.Bay
{
    /// <summary>
    /// Class that overlook and start the entire repai sequence for a train
    /// </summary>
    public class RepairSequence : MonoBehaviour
    {
        [SerializeField]
        private TrainManager _trainM;

        private UnityEvent _sequenceFinishedEvent;

        private bool _trainReachedDestination = false;
        private bool _trainReachedOrigin = false;
        private bool _maintenanceFinished = false;

        /// <summary>
        /// initiate  the sequenceFinished event
        /// </summary>
        public void Awake()
        {
            _sequenceFinishedEvent = new UnityEvent();
        }

        /// <summary>
        /// This function started the coroutine after the AppManger startet this function
        /// </summary>
        public void Begin()
        {
            StartCoroutine(PlaySequence());
        }

        /// <summary>
        /// set a few bools to false, started the InitTrain() from the TrainManager and initiate the lsitener to the TrainManager
        /// </summary>
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

        /// <summary>
        /// The coroutine that starts the function one after another to repair the train
        /// </summary>
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

        /// <summary>
        /// little events to check where the train is in the repair sequence
        /// </summary>
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

        /// <summary>
        /// Add und Remover of the Listener for the event
        /// </summary>
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