using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.MiniGame
{
    /// <summary>
    /// Class to save the position of the marble and labyrinth and put it back at the awake of it
    /// </summary>
    public class LabyrinthController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _marble;

        private Vector3 _labyrintStartPosition;
        private Vector3 _marbleStartPosition;

        /// <summary>
        /// Saved the position of the marble and the labyrinth
        /// </summary>
        private void Awake()
        {
            _marbleStartPosition = _marble.transform.position;
            _labyrintStartPosition = gameObject.transform.position;
        }

        private void OnEnable()
        {
            ResetLabyrinth();
        }

        /// <summary>
        /// reset the marble and labyrinth position
        /// </summary>
        public void ResetLabyrinth()
        {
            _marble.transform.position = _marbleStartPosition;
            gameObject.transform.position = _labyrintStartPosition;
        }



    }
}

