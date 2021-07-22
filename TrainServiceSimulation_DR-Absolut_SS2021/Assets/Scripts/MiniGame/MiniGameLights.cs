using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TrainServiceSimulation.Enums;

namespace TrainServiceSimulation
{
    public class MiniGameLights : MonoBehaviour
    {
        [SerializeField] private Renderer _rend;
        public EMiniGameColors _colors;
    }
}
