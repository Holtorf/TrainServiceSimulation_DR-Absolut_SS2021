using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.Train
{

    public class Train : MonoBehaviour
    {
        
        //
        public List<Wagon> wagons = new List<Wagon>();
        
        //
        private float _decoupleTime = 1f;

        //Only to check in the inspector
        private bool _finishedDecoupling = false;
        private bool _finishedCoupling = false;

        public bool FinishedDecoupling { get => _finishedDecoupling; set => _finishedDecoupling = value; }
        public bool FinishedCoupling { get => _finishedCoupling; set => _finishedCoupling = value; }

        //function to decouple the diffrent wagoons from the train
        public void Decouple()
        {
            LeanTween.value(0f, 1f, _decoupleTime * wagons.Count - 1).setOnComplete(() =>
            {
                FinishedDecoupling = true;
            });
            for (int i = 0; i < wagons.Count - 1; i++)
            {
                float wMove = wagons[i].transform.position.x - (wagons.Count - 1 * i);
                LeanTween.moveX(wagons[i].gameObject, wMove, _decoupleTime).setDelay(i * _decoupleTime);
            }
        }

        //function to couple the diffrent wagoons after the RepairSequenz from the train
        public void Couple()
        {
            LeanTween.value(0f, 1f, _decoupleTime * wagons.Count - 1).setOnComplete(() =>
            {
                FinishedCoupling = true;
            });
            for (int i = wagons.Count - 1; i >= 0; i--)
            {
                float wMove = wagons[i].transform.position.x + (wagons.Count - 1 * i);
                LeanTween.moveX(wagons[i].gameObject, wMove, _decoupleTime).setDelay((wagons.Count - 1) - i * _decoupleTime);
            }
        }
    }

}