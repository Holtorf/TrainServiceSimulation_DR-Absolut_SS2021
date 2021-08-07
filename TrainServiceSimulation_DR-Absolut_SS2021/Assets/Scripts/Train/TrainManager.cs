using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using TrainServiceSimulation;
using TrainServiceSimulation.Bay;
using TrainServiceSimulation.FTS;

namespace TrainServiceSimulation.Train
{
    public class TrainManager : MonoBehaviour
    {
        [SerializeField] 
        private TimeManger _timeM;
        [SerializeField]
        private FtsMovement _ftsMovement;
        [SerializeField]
        private FtsManager _ftsManager;
        [SerializeField]
        private AppManager _appManager;

        [SerializeField] 
        private List<Trains> _trains = new List<Trains>();
        [SerializeField]
        private Transform[] _trainOrigin;
        [SerializeField]
        private Transform[] _trainDestination;

        private int _randOrigin;

        [SerializeField]
        private float _trainMoveTime = 10f;

        //[ReadOnly]
        //[SerializeField]
        private Trains _currentTrain;

        private int _trainNumber = 0;

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
            _randOrigin = Random.Range(0, _trainOrigin.Length);
            _currentTrain = Instantiate(_trains[TrainNumber], _trainOrigin[_randOrigin].position, Quaternion.identity);
            foreach (Wagon _wagon in _currentTrain.Wagons)
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
            foreach (Wagon _wagon in _currentTrain.Wagons)
            {
                _wagon.State = Enums.EWagonState.PENDING;
            }
            yield return new WaitForSeconds(1f);

            _ftsManager.InitFtsManager(_currentTrain);

            _startWorkingEvent.Invoke();
            yield return new WaitUntil(() => _currentTrain.Wagons.All<Wagon>(x => x.State == Enums.EWagonState.NONE));

            _currentTrain.Couple();
            yield return new WaitUntil(() => _currentTrain.FinishedCoupling);
            _ftsManager.StopAllCoroutines();
            _maintenanceFinishedEvent.Invoke();
        }

        public void MoveTrainToDestination()
        {
            LeanTween.move(_currentTrain.gameObject, _trainDestination[_randOrigin].position, _trainMoveTime).setEaseInOutExpo().setOnComplete(() =>
            {
                _trainReachedDestinationEvent.Invoke();
            });
        }

        public void MoveTrainToOrigin()
        {
            LeanTween.move(_currentTrain.gameObject, _trainOrigin[_randOrigin].position, _trainMoveTime).setEaseInOutExpo().setOnComplete(() =>
            {
                _trainReachedOriginEvent.Invoke();
            });
        }

        public void DestroyTrain()
        {
            Destroy(_currentTrain.gameObject);
            _ftsManager.ClearAll();
            _appManager.StopAllCoroutines();
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

    }
}