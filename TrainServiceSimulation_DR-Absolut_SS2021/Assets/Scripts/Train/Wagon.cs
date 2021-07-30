using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Enums;

namespace TrainServiceSimulation.Train
{
    /// <summary>
    /// Class to give each wagon a state and a service type
    /// </summary>
    public class Wagon : MonoBehaviour
    {
        /// <summary>
        /// Connection to the right material of each wagon
        /// </summary>
        [SerializeField]
        private Renderer _renderer;

        /// <summary>
        /// Connection to the enums type and state
        /// </summary>
        private EWagonState _state;
        private EServiceType _type;

        /// <summary>
        /// function to declare what should happen with the wagon in each state
        /// </summary>
        public EWagonState State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                switch (_state)
                {
                    case EWagonState.NONE:
                        break;
                    case EWagonState.PENDING:
                        break;
                    case EWagonState.WORKING:
                        break;
                    case EWagonState.COMPLETED:
                        break;
                }
            }
        }

        /// <summary>
        /// function to declare what should happen with each wagon in each service type
        /// </summary>
        public EServiceType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                switch (_type)
                {
                    case EServiceType.CLEANING:
                        _renderer.materials[0].color = Color.white;
                        break;
                    case EServiceType.ELECTRONIC:
                        _renderer.materials[0].color = Color.yellow;
                        break;
                    case EServiceType.INTERIOR:
                        _renderer.materials[0].color = Color.green;
                        break;
                    case EServiceType.PROBLEM:
                        _renderer.materials[0].color = Color.red;
                        break;
                    case EServiceType.QUALITY:
                        _renderer.materials[0].color = Color.blue;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// function to initiate a wagon
        /// </summary>
        public void InitWagon()
        {
            State = EWagonState.NONE;
            int rand = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EServiceType)).Length);
            Type = (EServiceType)rand;
            _renderer = GetComponentInChildren<Renderer>();
        }


    }

}
