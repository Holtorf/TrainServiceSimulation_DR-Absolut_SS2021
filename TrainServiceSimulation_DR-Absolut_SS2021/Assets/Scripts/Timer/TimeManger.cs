using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Class to manage all the time relevant infos
/// </summary>

namespace TrainServiceSimulation.Timer
{
    public class TimeManger : MonoBehaviour
    {
        /// <summary>
        /// Connection where the timer text should be shown in the simulation
        /// </summary>
        [SerializeField] 
        private TextMeshProUGUI _timerText;

        /// <summary>
        /// A flaot that ensure that the timer start with 0 when ever the simulation is started 
        /// </summary>
        private float _startTime;

        /// <summary>
        /// The flaot that give the time 
        /// </summary>
        private float _time;

        /// <summary>
        /// Bool to clarify when the timer is active and running
        /// </summary>        
        private bool _isTimerRunning = false;
        /// <summary>
        /// Bool to clarify when the Pause-Button was used
        /// </summary>
        private bool _isPaused = false;

        /// <summary>
        /// Connection to the Speed slider between the script and the GUI
        /// </summary>
        [SerializeField]
        private Slider _speedSlider;

        /// <summary>
        /// Getter and Setter for _startTime to use it in other classes too
        /// </summary>
        public float StartTime { get => _startTime; set => _startTime = value; }
        
        /// <summary>
        /// Getter and Setter to use privates in other classes
        /// </summary>
        public bool IsTimerRunning { get => _isTimerRunning; set => _isTimerRunning = value; }
        public float Timeing { get => _time; set => _time = value; }

        /// <summary>
        /// Set the time scale of the entire scene at the before defined value in the inspector
        /// </summary>
        private void Awake()
        {
            UnityEngine.Time.timeScale = _speedSlider.value;
        }

        /// <summary>
        /// When the timer is running the Update function set the timer value further on
        /// </summary>
        void Update()
        {
            if (IsTimerRunning)
            {
                Timeing = (UnityEngine.Time.time - StartTime);

                string minutes = ((int)Timeing / 60).ToString();
                string seconds = (Timeing % 60).ToString("f2");

                _timerText.text = minutes + ":" + seconds;
            }
        }

        /// <summary>
        /// Button function to load the entire Scene new
        /// </summary>
        public void RestartScene()
        {
            SceneManager.LoadScene(0);
        }

        /// <summary>
        /// Button function to toggle between pause and play
        /// </summary>
        public void TogglePause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                UnityEngine.Time.timeScale = 0;
            }
            else
            {
                UnityEngine.Time.timeScale = _speedSlider.value;
            }

        }

        /// <summary>
        /// Unity Event that is caled when the Slider ist changed over the dashboard
        /// </summary>
        public void OnSpeedSliderChanged()
        {
            if (!_isPaused)
            {
                UnityEngine.Time.timeScale = _speedSlider.value;
            }   
        }
    }
}

