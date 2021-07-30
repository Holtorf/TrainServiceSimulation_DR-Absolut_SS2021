using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TrainServiceSimulation.Train;

/// <summary>
/// Class to change with how many wagons the next train should be spawn
/// </summary>

namespace TrainServiceSimulation.UI
{
    public class TrainNumberChanger : MonoBehaviour
    {
        /// <summary>
        /// Connection to the right Slider for the class
        /// </summary>
        [SerializeField]
        private Slider _trainSlider;

        /// <summary>
        /// Connection to the Train Manager to change the train number there
        /// </summary>
        [SerializeField]
        private TrainManager _trainM;

        /// <summary>
        /// Unity Event that is caled when the Slider ist changed over the dashboard
        /// </summary>
        public void OnTrainSliderChanged()
        {

            _trainM.TrainNumber = (int)_trainSlider.value;
         
        }

    }
}

