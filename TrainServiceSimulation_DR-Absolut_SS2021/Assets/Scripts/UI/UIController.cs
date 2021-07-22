using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public Slider TrainSlider { get => _trainSlider; set => _trainSlider = value; }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

