using System.Collections;
using TrainServiceSimulation.Enums;
using TrainServiceSimulation.FTS;
using TrainServiceSimulation.MiniGame;
using TrainServiceSimulation.Train;
using UnityEngine;

namespace TrainServiceSimulation.Bay
{
    /// <summary>
    /// The class that controled every Service Bay
    /// Its give the Bay its Type and other informations and indicates how its have to handle 
    /// a wagon when the bay gets one
    /// </summary>
    public class ServiceBay : MonoBehaviour
    {
        [SerializeField]
        private EServiceType _eServiceType;

        [SerializeField]
        private FtsManager _ftsManager;

        [SerializeField]
        private float _workingTime;

        [ReadOnly]
        [SerializeField]
        private float _restWorkingtime;

        private float _workingMultipli = 1f;

        private bool _workingFinished = false;
        private bool _startWorking;

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

        /// <summary>
        /// Contoll when the working time is finished
        /// </summary>
        private void Update()
        {
            if (_startWorking)
            {
                _restWorkingtime = _restWorkingtime*_workingMultipli - 1*Time.deltaTime;
                
                if (_restWorkingtime <= 0)
                {
                    _workingFinished = true;
                    _startWorking = false;
                    return;
                }
            }
        }

        /// <summary>
        /// Coroutine that indicates what the Service Bay is doing when its get a wagon from the FTS
        /// </summary>
        public IEnumerator RepairWagon(Wagon wagon, Vector3 originalPosition)
        {
            if(wagon != null)
            {
                _restWorkingtime = _workingTime;
                _audioSource.Play();
                _startWorking = true;
                _miniGameController.SetActivateLabyrinth();
                _isOccupied = true;
                _wagon = wagon;
                OriginalPosition = originalPosition;
                wagon.State = EWagonState.WORKING;
                yield return new WaitUntil(() => _workingFinished == true);
                wagon.State = EWagonState.COMPLETED;
                _audioSource.Stop();
            }
            
        }

        /// <summary>
        /// Function that cleared the Service Bay after the wagon is get back to the trails
        /// </summary>
        public void ClearServiceBay()
        {
            _workingMultipli = 1f;
            _workingFinished = false;
            _isOccupied = false;
            _wagon = null;
            _originalPosition = Vector3.zero;
            _miniGameController.GameLight.GetComponent<Renderer>().materials[0].color = Color.red;
            _miniGameController.Labyrinths[_miniGameController.Rand].gameObject.SetActive(false);
        }
    }

}
