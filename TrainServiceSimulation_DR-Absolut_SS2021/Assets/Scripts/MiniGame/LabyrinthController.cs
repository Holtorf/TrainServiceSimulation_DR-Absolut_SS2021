using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.MiniGame
{
    public class LabyrinthController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _marble;

        private Vector3 _marbleStartPosition;

        private void Awake()
        {
            _marbleStartPosition = _marble.transform.position;
        }

        public void ResetLabyrinth()
        {
            _marble.transform.position = _marbleStartPosition;
        }



    }
}

