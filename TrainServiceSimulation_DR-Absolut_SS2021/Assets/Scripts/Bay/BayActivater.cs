using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TrainServiceSimulation.Enums;

namespace TrainServiceSimulation.Bay
{
    public class BayActivater : MonoBehaviour
    {
        [SerializeField]
        private ServiceBay[] _bays;

        [SerializeField]
        private Slider _baySlider;
        
        private int _bayCount;

        public int BayCount { get => _bayCount; set => _bayCount = value; }
        public ServiceBay[] Bays { get => _bays; set => _bays = value; }

        public void BayCountChange()
        {
            for(int i = 0; i < BayCount; i++)
            {
                _bays[i].gameObject.SetActive(true);
            }
            for(int j = BayCount; j < _bays.Length; j++)
            {
                _bays[j].gameObject.SetActive(false);
            }
        }

        public void OnValueChanged()
        {
            _bayCount = (int)_baySlider.value;
            BayCountChange();
        }

        public bool TryGetBay(EServiceType type, out ServiceBay serviceBay)
        {
            for (int i = 0; i < _bays.Length; i++)
            {
                if (_bays[i].isActiveAndEnabled)
                {
                    if (_bays[i].EServiceType == type && !_bays[i].IsOccupied)
                    {
                        serviceBay = _bays[i];
                        return true;
                    }
                }
                
            }

            serviceBay = null;
            return false;

        }
    }
}

