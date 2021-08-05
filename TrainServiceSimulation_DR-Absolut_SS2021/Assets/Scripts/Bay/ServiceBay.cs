using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Enums;
using TrainServiceSimulation.Train;
using TrainServiceSimulation.FTS;
using TrainServiceSimulation.MiniGame;
using UnityEngine.Audio;

namespace TrainServiceSimulation.Bay
{
    public class ServiceBay : MonoBehaviour
    {
        [SerializeField]
        private EServiceType _eServiceType;

        [SerializeField]
        private FtsManager _ftsManager;

        [SerializeField]
        private float _workingTime;

        private float _workingMultipli = 1f;

        [SerializeField]
        private MiniGameController _miniGameController;

        [SerializeField]
        private AudioSource _audioSource;

        [ReadOnly]
        [SerializeField]
        private bool _isOccupied;

        [ReadOnly]
        [SerializeField]
        private Wagon _wagon;

        private Vector3 _originalPosition;


        public EServiceType EServiceType { get => _eServiceType; set => _eServiceType = value; }
        public bool IsOccupied { get => _isOccupied; set => _isOccupied = value; }
        public Wagon Wagon { get => _wagon; set => _wagon = value; }
        public Vector3 OriginalPosition { get => _originalPosition; set => _originalPosition = value; }
        public float WorkingMultipli { get => _workingMultipli; set => _workingMultipli = value; }

        public IEnumerator RepairWagon(Wagon wagon, Vector3 originalPosition)
        {
            if(wagon != null)
            {
                _audioSource.Play();
                _miniGameController.SetActivateLabyrinth();
                _isOccupied = true;
                _wagon = wagon;
                OriginalPosition = originalPosition;
                wagon.State = EWagonState.WORKING;
                yield return new WaitForSeconds(_workingTime*_workingMultipli);
                wagon.State = EWagonState.COMPLETED;
                _audioSource.Stop();
            }
            
        }


        public void ClearServiceBay()
        {
            _workingMultipli = 1f;
            _isOccupied = false;
            _wagon = null;
            _originalPosition = Vector3.zero;
            _miniGameController.GameLight.GetComponent<Renderer>().materials[0].color = Color.red;
            _miniGameController.Labyrinths[_miniGameController.Rand].gameObject.SetActive(false);
        }
    }

}
