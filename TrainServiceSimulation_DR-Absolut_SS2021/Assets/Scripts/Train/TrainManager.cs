using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TrainServiceSimulation;
using TrainServiceSimulation.Bay;

namespace TrainServiceSimulation.Train
{
    public class TrainManager : MonoBehaviour
    {
        [SerializeField] 
        private TimeManger _timeM;

        [SerializeField] 
        private List<Train> _trains = new List<Train>();
        [SerializeField]
        private Transform _trainOrigin;
        [SerializeField]
        private Transform _trainDestination;

        [SerializeField]
        private float _trainMoveTime = 10f;

        private Train _currentTrain;

        private int _trainNumber = 0;

        //[SerializeField, Tooltip("ServiceTypOrder: Cleaning,Electric,Interior,Problems,Quality")] 
        //private BayManager[] _bays;

        private UnityEvent _trainReachedDestinationEvent;
        private UnityEvent _trainReachedOriginEvent;
        private UnityEvent _maintenanceFinishedEvent;
        private UnityEvent _startWorkingEvent;

        public void Awake()
        {
            _trainReachedDestinationEvent = new UnityEvent();
            _trainReachedOriginEvent = new UnityEvent();
            _maintenanceFinishedEvent = new UnityEvent();
            _startWorkingEvent = new UnityEvent();
        }

        public int TrainNumber { get => _trainNumber; set => _trainNumber = value; }

        public void InitTrain()
        {
            _currentTrain = Instantiate(_trains[TrainNumber], _trainOrigin.position, Quaternion.identity);
            foreach (Wagon _wagon in _currentTrain.wagons)
            {
                _wagon.InitWagon();
            }
        }

        public void StartMaintenance()
        {
            StartCoroutine(StartMaintenanceSequence());
        }

        IEnumerator StartMaintenanceSequence()
        {
            _currentTrain.Decouple();
            yield return new WaitUntil(() => _currentTrain.FinishedDecoupling);
            foreach (Wagon _wagon in _currentTrain.wagons)
            {
                _wagon.State = Enums.EWagonState.PENDING;
            }

            _startWorkingEvent.Invoke();
            yield return new WaitUntil(() => _currentTrain.wagons.All<Wagon>(x => x.State == Enums.EWagonState.COMPLETED));


            _currentTrain.Couple();
            yield return new WaitUntil(() => _currentTrain.FinishedCoupling);
            foreach (Wagon wagon in _currentTrain.wagons)
            {
                wagon.State = Enums.EWagonState.NONE;
            }
            _maintenanceFinishedEvent.Invoke();
        }

        public void MoveTrainToDestination()
        {
            LeanTween.move(_currentTrain.gameObject, _trainDestination.position, _trainMoveTime).setEaseInOutExpo().setOnComplete(() =>
            {
                _trainReachedDestinationEvent.Invoke();
            });
        }

        public void MoveTrainToOrigin()
        {
            LeanTween.move(_currentTrain.gameObject, _trainOrigin.position, _trainMoveTime).setEaseInOutExpo().setOnComplete(() =>
            {
                _trainReachedOriginEvent.Invoke();
            });
        }

        public void AddListenerTrainReachedDestinationEvent(UnityAction call)
        {
            _trainReachedDestinationEvent.AddListener(call);
        }
        public void RemoveListenerTrainReachedDestinationEvent(UnityAction call)
        {
            _trainReachedDestinationEvent.RemoveListener(call);
        }

        public void AddListenerTrainReachedOriginEvent(UnityAction call)
        {
            _trainReachedOriginEvent.AddListener(call);
        }
        public void RemoveListenerTrainReachedOriginEvent(UnityAction call)
        {
            _trainReachedOriginEvent.RemoveListener(call);
        }

        public void AddListenerMaintenanceFinishedEvent(UnityAction call)
        {
            _maintenanceFinishedEvent.AddListener(call);
        }
        public void RemoveListenerMaintenanceFinishedEvent(UnityAction call)
        {
            _maintenanceFinishedEvent.RemoveListener(call);
        }

        public void AddListenerStartWorking(UnityAction call)
        {
            _startWorkingEvent.AddListener(call);
        }
        public void RemoveListenerStartWorking(UnityAction call)
        {
            _startWorkingEvent.RemoveListener(call);
        }
    }
}