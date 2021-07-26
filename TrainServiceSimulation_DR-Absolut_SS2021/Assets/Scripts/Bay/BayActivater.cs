using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TrainServiceSimulation.Bay
{
    public class BayActivater : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _bays;

        [SerializeField]
        private Slider _baySlider;
        
        private int _bayCount;

        public int BayCount { get => _bayCount; set => _bayCount = value; }

        public void BayCountChange()
        {
            for(int i = 0; i <= BayCount-1; i++)
            {
                _bays[i].SetActive(true);
            }
            for(int j = BayCount; j <= _bays.Length-1; j++)
            {
                _bays[j].SetActive(false);
            }
        }

        public void OnValueChanged()
        {
            BayCount = (int)_baySlider.value;
            BayCountChange();
        }
    }
}

