using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TrainServiceSimulation
{
    public class TimeManger : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI _timerText;

        private float _startTime;
        
        private bool _isTimerRunning = false;
        private bool _isPaused = false;

        [SerializeField]
        private Slider _speedSlider;

        public float StartTime { get => _startTime; set => _startTime = value; }
        public bool IsTimerRunning { get => _isTimerRunning; set => _isTimerRunning = value; }

        void Update()
        {
            if (IsTimerRunning)
            {
                float t = Time.time - StartTime;

                string minutes = ((int)t / 60).ToString();
                string seconds = (t % 60).ToString("f2");

                _timerText.text = minutes + ":" + seconds;
            }
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(0);
        }

        public void TogglePause()
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }

        }

        public void OnSpeedSliderChanged()
        {
            if (!_isPaused)
            {
                Time.timeScale = _speedSlider.value;
            }   
        }
    }
}

