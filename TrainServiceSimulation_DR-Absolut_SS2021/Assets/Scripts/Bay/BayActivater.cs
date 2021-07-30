using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TrainServiceSimulation.Enums;

namespace TrainServiceSimulation.Bay
{
    /// <summary>
    /// Class that managed all the Bays in the scene
    /// </summary>
    public class BayActivater : MonoBehaviour
    {
        /// <summary>
        /// Array of all Bays inside the scene
        /// </summary>
        [SerializeField]
        private ServiceBay[] _bays;

        /// <summary>
        /// Connection to the Slider for the bays on the dashboard
        /// </summary>
        [SerializeField]
        private Slider _baySlider;

        /// <summary>
        /// A int do define the how many bays are needed to be activated
        /// </summary>
        private int _bayCount;

        public int BayCount { get => _bayCount; set => _bayCount = value; }
        public ServiceBay[] Bays { get => _bays; set => _bays = value; }

        /// <summary>
        /// function to change the active Bays in the scene
        /// </summary>
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

        /// <summary>
        /// function to change the value of _bayCount through the slider 
        /// </summary>
        public void OnValueChanged()
        {
            _bayCount = (int)_baySlider.value;
            BayCountChange();
        }

        /// <summary>
        /// function for the FTS to look if the bay is free and the type of 
        /// the bay is the same as the wagon
        /// </summary>
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

