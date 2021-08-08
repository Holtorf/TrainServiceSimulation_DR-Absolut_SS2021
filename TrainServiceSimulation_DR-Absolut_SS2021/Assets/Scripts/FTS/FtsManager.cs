using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.Bay;
using TMPro;


namespace TrainServiceSimulation.FTS
{

    /// <summary>
    /// Main Class for the FTS
    /// Its controll the function with the FTS is finding its next job, what the FTS have to do when its drive from
    /// the trail to the bays and the other way around, that the FTS should do when there are no Jobs at the moment 
    /// and how the train take the wagon with it 
    /// </summary>
    public class FtsManager : MonoBehaviour
    {
        /// <summary>
        /// Connection to the other classes 
        /// </summary>
        [SerializeField]
        private BayActivater _bayActivater;
        [SerializeField]
        private FtsMovement _ftsMovement;
        [SerializeField]
        private GameObject _fts;
        [SerializeField]
        private AppManager _appManager;
        
        private ServiceBay _serviceBay;

        /// <summary>
        /// the coordination of the Waypoint at the FTS start
        /// </summary>
        [SerializeField]
        private WayPoint _startPoint;

        /// <summary>
        /// local variables to save the train and the wagon in the scene
        /// </summary>
        //[ReadOnly]
        //[SerializeField]
        private Wagon _wagon;
        private Trains _train;

        private Vector3 _bayPosition;
        private Vector3 _wagonOriginalPosition;

        /// <summary>
        /// bool to check if there is atrain in the scene
        /// was needed because the FTS sometimes searched for wagon when there was none
        /// </summary>
        private bool _betweenTrains;

        /// <summary>
        /// Update function to activate the TakeWagonWithYou() every time at a frame when the FTS has a wagon as child
        /// and to stop all coroutines when the Fts is at the start position and to search afte a new job at the same time
        /// </summary>
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

        /// <summary>
        /// Class that startet the FTS the first time and get the train for SearchNextJob() 
        /// </summary>
        public void InitFtsManager(Trains train)
        {
            _train = train;

            _betweenTrains = false;
            SearchNextJob();
        }

        /// <summary>
        /// Coroutine that defined what the FTS is doing step by step to get a wagon from the trails to a bay
        /// </summary>
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

        /// <summary>
        /// Coroutine that defined what the FTS is doing step by step to get a wagon from a bay the the trails
        /// </summary>
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

        /// <summary>
        /// Function that searched the next job for the FTS
        /// for that its going first through every Wagon in train an is looking if the State is PENDING and if 
        /// there is a bay with the right Type. When the answer is yes its start the DriveToBay Coroutine. 
        /// When the answer is no its go to all bays and is looking if any wagon has the State COMPLETED to bring 
        /// it to the trails. If that is No to the FTS is driving back to the start position
        /// </summary>
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

        /// <summary>
        /// Function to give the wagon the same position of the FTS
        /// it is called every frame in the Update() 
        /// </summary>
        public void TakeWagonWithYou()
        {
            if(_wagon.State != Enums.EWagonState.NONE)
            {
                _wagon.transform.position = _fts.transform.position;
            }
            
        }

        /// <summary>
        /// Function to drive the FTS back to its original position
        /// </summary>
        private void GetBackToStart()
        {
            _ftsMovement.DriveToDestination(_startPoint.transform.position);
            ClearFTS();
        }

        /// <summary>
        /// Function to set all FTS relevant variables back
        /// </summary>
        private void ClearFTS()
        {
            _wagon = null;
            _wagonOriginalPosition = Vector3.zero;
            _serviceBay = null;
            _bayPosition = Vector3.zero;
        }

        /// <summary>
        /// Function to set all variables back
        /// </summary>
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
