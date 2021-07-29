using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.Train
{

    public class Trains : MonoBehaviour
    {

        //
        [SerializeField]
        private List<Wagon> wagons = new List<Wagon>();

        //
        private float _decoupleTime = 1f;

        //Only to check in the inspector
        [SerializeField]
        private bool _finishedDecoupling = false;
        [SerializeField]
        private bool _finishedCoupling = false;

        public bool FinishedDecoupling { get => _finishedDecoupling; set => _finishedDecoupling = value; }
        public bool FinishedCoupling { get => _finishedCoupling; set => _finishedCoupling = value; }
        public List<Wagon> Wagons { get => wagons; set => wagons = value; }

        //function to decouple the diffrent wagoons from the train
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

        //function to couple the diffrent wagoons after the RepairSequenz from the train
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