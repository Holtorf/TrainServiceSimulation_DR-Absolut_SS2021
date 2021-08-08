using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.Train
{
    /// <summary>
    /// Class that has the list of the wagons for the train and handled the coupling and decoupling of the wagons
    /// </summary>
    public class Trains : MonoBehaviour
    {


        [SerializeField]
        private List<Wagon> wagons = new List<Wagon>();

        private float _decoupleTime = 1f;

        [ReadOnly]
        [SerializeField]
        private bool _finishedDecoupling = false;
        [ReadOnly]
        [SerializeField]
        private bool _finishedCoupling = false;

        public bool FinishedDecoupling { get => _finishedDecoupling; set => _finishedDecoupling = value; }
        public bool FinishedCoupling { get => _finishedCoupling; set => _finishedCoupling = value; }
        public List<Wagon> Wagons { get => wagons; set => wagons = value; }

        /// <summary>
        /// function to decouple the diffrent wagoons from the train
        /// </summary>
        public void Decouple()
        {
            LeanTween.value(0f, 1f, _decoupleTime * Wagons.Count - 1).setOnComplete(() =>
            {
                FinishedDecoupling = true;
            });
            for (int i = 0; i < Wagons.Count - 1; i++)
            {
                float wMove = Wagons[i].transform.position.x - (Wagons.Count - 1 * i);
                LeanTween.moveX(Wagons[i].gameObject, wMove, _decoupleTime).setDelay(i * _decoupleTime);
            }
        }

        /// <summary>
        /// function to couple the diffrent wagoons after the RepairSequenz from the train
        /// </summary>
        public void Couple()
        {
            LeanTween.value(0f, 1f, _decoupleTime * Wagons.Count - 1).setOnComplete(() =>
            {
                FinishedCoupling = true;
            });
            for (int i = Wagons.Count - 1; i >= 0; i--)
            {
                float wMove = Wagons[i].transform.position.x + (Wagons.Count - 1 * i);
                LeanTween.moveX(Wagons[i].gameObject, wMove, _decoupleTime).setDelay((Wagons.Count - 1) - i * _decoupleTime);
            }
        }
    }

}