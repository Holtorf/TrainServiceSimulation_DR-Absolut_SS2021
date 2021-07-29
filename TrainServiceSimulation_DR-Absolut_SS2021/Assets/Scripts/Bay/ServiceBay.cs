using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Enums;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.FTS;

namespace TrainServiceSimulation.Bay
{
    public class ServiceBay : MonoBehaviour
    {
        [SerializeField]
        private EServiceType _eServiceType;

        [SerializeField]
        private FtsManager _ftsManager;

        [SerializeField]
        private int _workingTime;

        private bool _isOccupied;

        private Wagon _wagon;
        private Vector3 _originalPosition;


        public EServiceType EServiceType { get => _eServiceType; set => _eServiceType = value; }
        public int WorkingTime { get => _workingTime; set => _workingTime = value; }
        public bool IsOccupied { get => _isOccupied; set => _isOccupied = value; }
        public Wagon Wagon { get => _wagon; set => _wagon = value; }
        public Vector3 OriginalPosition { get => _originalPosition; set => _originalPosition = value; }

        public IEnumerator RepairWagon(Wagon wagon, Vector3 originalPosition)
        {
            _isOccupied = true;
            Wagon = wagon;
            OriginalPosition = originalPosition;
            wagon.State = EWagonState.WORKING;
            yield return new WaitForSeconds(_workingTime);
            wagon.State = EWagonState.COMPLETED;
        }


        public void ClearServiceBay()
        {
            _isOccupied = false;
            //_wagon = null;
            _originalPosition = Vector3.zero;
        }
    }

}
