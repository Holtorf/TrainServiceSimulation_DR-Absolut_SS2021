using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Enums;

namespace TrainServiceSimulation.Train
{
    public class Wagon : MonoBehaviour
    {
        [SerializeField]
        private Renderer _renderer;
        [SerializeField]
        private EWagonState _state;
        [SerializeField]
        private EServiceType _type;

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

        public void InitWagon()
        {
            State = EWagonState.NONE;
            int rand = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(EServiceType)).Length);
            Type = (EServiceType)rand;
            _renderer = GetComponentInChildren<Renderer>();
        }

    }

}
