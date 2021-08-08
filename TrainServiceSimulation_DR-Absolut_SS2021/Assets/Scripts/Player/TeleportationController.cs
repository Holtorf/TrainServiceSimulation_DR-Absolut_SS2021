using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using TrainServiceSimulation.Enums;

namespace TrainServiceSimulation.Player
{
    /// <summary>
    /// Class that define the teleportation of the XR Interaction Toolkit better
    /// </summary>
    public class TeleportationController : MonoBehaviour
    {

        /// <summary>
        /// How much the stick must be moved to activate the line
        /// </summary>
        [SerializeField, Tooltip("How much the stick must be moved to activate the line"), Range(0.1f, 0.95f)]
        private float deadzone = 0.5f;

        /// <summary>
        /// What kind of hand is it
        /// </summary>
        [SerializeField, Tooltip("What kind of hand is it")]
        private HandTypes handType = HandTypes.LEFT;

        /// <summary>
        /// The input device that controls that hand
        /// </summary>
        private InputDevice inputDevice;

        /// <summary>
        /// The Line
        /// </summary>
        private XRInteractorLineVisual lineVisual;

        /// <summary>
        /// Search for an fitting input device and the line visual
        /// </summary>
        private void Start()
        {
            InputDeviceCharacteristics inputDeviceCharacteristics = InputDeviceCharacteristics.HeldInHand
            | InputDeviceCharacteristics.Controller
            | (handType == HandTypes.LEFT ? InputDeviceCharacteristics.Left : InputDeviceCharacteristics.Right);

            List<InputDevice> inputDevices = new List<InputDevice>();
            InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, inputDevices);

            if (inputDevices.Count > 0)
            {
                inputDevice = inputDevices[0];
            }

            lineVisual = GetComponent<XRInteractorLineVisual>();
        }

        /// <summary>
        /// Checks whether the input is higher than the deadzone and shows the line accordingly
        /// </summary>
        private void Update()
        {
            inputDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 axisInput);

            lineVisual.enabled = axisInput.y > deadzone;
        }


    }
}
