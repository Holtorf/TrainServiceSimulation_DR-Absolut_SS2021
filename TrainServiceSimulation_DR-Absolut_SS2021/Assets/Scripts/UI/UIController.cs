using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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


        public void OnTrainSliderChanged()
        {

            _trainM.TrainNumber = (int)_trainSlider.value;
            Debug.Log(_trainSlider.value);
         
        }

    }
}

