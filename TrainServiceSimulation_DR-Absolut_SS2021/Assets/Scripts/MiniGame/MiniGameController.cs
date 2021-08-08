using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Bay;


namespace TrainServiceSimulation.MiniGame
{
    /// <summary>
    /// Class that enable one of the labyrinths inside the bay
    /// </summary>
    public class MiniGameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] _labyrinths;

        [SerializeField]
        private GameObject _gameLight;

        private int _rand;

        public GameObject GameLight { get => _gameLight; set => _gameLight = value; }
        public int Rand { get => _rand; set => _rand = value; }
        public GameObject[] Labyrinths { get => _labyrinths; set => _labyrinths = value; }

        /// <summary>
        /// set the random variable and activate one of the labyrinth
        /// </summary>
        public void SetActivateLabyrinth()
        {
           Rand = UnityEngine.Random.Range(0, Labyrinths.Length);
           Labyrinths[Rand].gameObject.SetActive(true);
        }

    }
}

