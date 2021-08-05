using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;
using TMPro;

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
        [SerializeField]
        private AppManager _appManager;
        
        private ServiceBay _serviceBay;

        [SerializeField]
        private WayPoint _startPoint;

        [ReadOnly]
        [SerializeField]
        private Wagon _wagon;

        private Vector3 _bayPosition;
        private Vector3 _wagonOriginalPosition;

        private Trains _train;
        private bool _betweenTrains;


        private void Update()
        {
            if (_wagon != null && !_betweenTrains)
            {
                TakeWagonWithYou();
            }
            
            if(_fts.transform.position == _startPoint.transform.position)
            {
                StopAllCoroutines();
                SearchNextJob();
            }
        }

        public void InitFtsManager(Trains train)
        {
            _train = train;

            _betweenTrains = false;
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

            _appManager.WagonCount++;
            _wagon = null;
            bays.ClearServiceBay();
            SearchNextJob();
        }

        public void SearchNextJob()
        {
            if(_train != null)
            {
                ClearFTS();
                foreach (Wagon wagon in _train.Wagons)
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

                GetBackToStart();
                
            }
        }

        public void TakeWagonWithYou()
        {
            if(_wagon.State != Enums.EWagonState.NONE)
            {
                _wagon.transform.position = _fts.transform.position;
            }
            
        }

        private void GetBackToStart()
        {
            _ftsMovement.DriveToDestination(_startPoint.transform.position);
            ClearFTS();
        }

        private void ClearFTS()
        {
            _wagon = null;
            _wagonOriginalPosition = Vector3.zero;
            _serviceBay = null;
            _bayPosition = Vector3.zero;
        }

        public void ClearAll()
        {
            _train = null;
            _wagon = null;
            _wagonOriginalPosition = Vector3.zero;
            _serviceBay = null;
            _bayPosition = Vector3.zero;
            _betweenTrains = true;
        }

    }

}
