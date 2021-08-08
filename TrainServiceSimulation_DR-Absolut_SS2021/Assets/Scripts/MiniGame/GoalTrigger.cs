using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Bay;

namespace TrainServiceSimulation.MiniGame
{
    /// <summary>
    /// Class that changes the WorkingMultiplier and the color of the GameLight when the Marble
    /// get to the end of the Labyrinth
    /// </summary>
    public class GoalTrigger : MonoBehaviour
    {
        
        [SerializeField]
        private MiniGameController _miniGameController;

        [SerializeField]
        private ServiceBay _serviceBay;

        [SerializeField]
        private LabyrinthController _labyrinthController;

        /// <summary>
        /// On Trigger Enter Event that is activate when the marble get in to the Goal of a labyrinth
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("MiniGame"))
            {
                _serviceBay.WorkingMultipli = 0.5f;
                _miniGameController.GameLight.GetComponent<Renderer>().materials[0].color = Color.green;
            }
        }


    }
}

