using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;

namespace TrainServiceSimulation.FTS
{
    public class FtsManager : MonoBehaviour
    {
        [SerializeField]
        private BayActivater _bayActivater;
        [SerializeField]
        private FtsMovement _ftsMovement;
        [SerializeField]
        private GameObject _fts;
        
        private ServiceBay _serviceBay;

        [SerializeField]
        private WayPoint _startPoint;

        private Wagon _wagon;

        private Vector3 _bayPosition;
        private Vector3 _wagonOriginalPosition;

        private Trains _train;


        private void Update()
        {
            if (_wagon != null)
            {
                TakeWagonWithYou();
            }

            if(_fts.transform.position == _startPoint.transform.position)
            {
                SearchNextJob();
            }
        }

        public void InitFtsManager(Trains train)
        {
            _train = train;

            SearchNextJob();
        }

        IEnumerator DriveToBay(Wagon wagon)
        {
            _ftsMovement.DriveToDestination(wagon.transform.position);
            yield return new WaitUntil(() => _ftsMovement.IsMoving == false);
            _wagon = wagon;

            _ftsMovement.DriveToDestination(_bayPosition);
            yield return new WaitUntil(() => _ftsMovement.IsMoving == false);
            wagon.transform.position = _bayPosition;

            _serviceBay.StartCoroutine(_serviceBay.RepairWagon(_wagon, _wagonOriginalPosition));
            _wagon = null;
            SearchNextJob();
        }

        IEnumerator DriveToTrials(ServiceBay bays)
        {
            
            _ftsMovement.DriveToDestination(bays.Wagon.transform.position);
            yield return new WaitUntil(() => _ftsMovement.IsMoving == false);
            _wagon = bays.Wagon;
            _ftsMovement.DriveToDestination(bays.OriginalPosition);
            
            yield return new WaitUntil(() => _ftsMovement.IsMoving == false);
            bays.Wagon.transform.position = bays.OriginalPosition;
            bays.Wagon.State = Enums.EWagonState.NONE;
            bays.ClearServiceBay();
            _wagon = null;
            SearchNextJob();
        }

        public void SearchNextJob()
        {
            foreach (Wagon wagon in _train.wagons)
            {
                if (wagon.State == Enums.EWagonState.PENDING)
                {
                    if (_bayActivater.TryGetBay(wagon.Type, out ServiceBay serviceBay))
                    {
                        _wagonOriginalPosition = wagon.transform.position;
                        _serviceBay = serviceBay;
                        _bayPosition = serviceBay.transform.position;
                        StartCoroutine(DriveToBay(wagon));
                        return;
                    }
                }
            }

            foreach (ServiceBay bays in _bayActivater.Bays)
            {
                if (bays.Wagon != null && bays.Wagon.State == Enums.EWagonState.COMPLETED)
                {
                    StartCoroutine(DriveToTrials(bays));
                    return;
                }
            }

            //_ftsIsWorking = false;
            _ftsMovement.DriveToDestination(_startPoint.transform.position);
        }

        public void TakeWagonWithYou()
        {
            _wagon.transform.position = _fts.transform.position;
        }

    }

}
