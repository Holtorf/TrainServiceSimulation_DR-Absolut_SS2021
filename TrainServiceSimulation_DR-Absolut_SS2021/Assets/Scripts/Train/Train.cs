using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainServiceSimulation.Train
{

    public class Train : MonoBehaviour
    {
        
        //
        [SerializeField]
        private List<Wagon> _wagons = new List<Wagon>();
        
        //
        private float _decoupleTime = 1f;

        //Only to check in the inspector
        public bool _finishedDecoupling = false;
        public bool _finishedCoupling = false;

        //function to decouple the diffrent wagoons from the train
        public void Decouple()
        {
            LeanTween.value(0f, 1f, _decoupleTime * _wagons.Count - 1).setOnComplete(() =>
            {
                _finishedDecoupling = true;
            });
            for (int i = 0; i < _wagons.Count - 1; i++)
            {
                float wMove = _wagons[i].transform.position.x - (_wagons.Count - 1 * i);
                LeanTween.moveX(_wagons[i].gameObject, wMove, _decoupleTime).setDelay(i * _decoupleTime);
            }
        }

        //function to couple the diffrent wagoons after the RepairSequenz from the train
        public void Couple()
        {
            LeanTween.value(0f, 1f, _decoupleTime * _wagons.Count - 1).setOnComplete(() =>
            {
                _finishedCoupling = true;
            });
            for (int i = _wagons.Count - 1; i >= 0; i--)
            {
                float wMove = _wagons[i].transform.position.x + (_wagons.Count - 1 * i);
                LeanTween.moveX(_wagons[i].gameObject, wMove, _decoupleTime).setDelay((_wagons.Count - 1) - i * _decoupleTime);
            }
        }
    }

}