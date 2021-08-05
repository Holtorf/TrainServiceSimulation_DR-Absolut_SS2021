using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Bay;

namespace TrainServiceSimulation.MiniGame
{
    public class GoalTrigger : MonoBehaviour
    {
        
        [SerializeField]
        private MiniGameController _miniGameController;

        [SerializeField]
        private ServiceBay _serviceBay;

        [SerializeField]
        private LabyrinthController _labyrinthController;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MiniGame"))
            {
                _serviceBay.WorkingMultipli = 0.5f;
                _labyrinthController.ResetLabyrinth();
                _miniGameController.GameLight.GetComponent<Renderer>().materials[0].color = Color.green;
            }
        }


    }
}

