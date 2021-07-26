using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TrainServiceSimulation.Train;

namespace TrainServiceSimulation.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField]
        private Slider _speedSlider;
        [SerializeField]
        private Slider _trainSlider;
        [SerializeField]
        private Slider _baySlider;

        [SerializeField]
        private TrainManager _trainM;

        public Slider TrainSlider { get => _trainSlider; set => _trainSlider = value; }

        public void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            if(_trainSlider.value == 1)
            {
                _trainM.TrainNumber = 0;
            }
            else if(_trainSlider.value == 2)
            {
                _trainM.TrainNumber = 1;
            }
            else if (_trainSlider.value == 3)
            {
                _trainM.TrainNumber = 2;
            }
            else if (_trainSlider.value == 4)
            {
                _trainM.TrainNumber = 3;
            }
        }
    }
}

