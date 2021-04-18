using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LocomotionController : MonoBehaviour
{
    public XRController rightTeleportRay;
    public InputHelpers.Button activationButton;
    public float activationThreshold = 0.1f;

    public bool enableRightTP { get; set; } = true;

    // Update is called once per frame
    void Update()
    {
        if(rightTeleportRay && enableRightTP)
        {
            rightTeleportRay.gameObject.SetActive(CheckIfActivated(rightTeleportRay));
        }
    }

    private bool CheckIfActivated(XRController controller)
    {
        InputHelpers.IsPressed(controller.inputDevice, activationButton, out bool isActivated, activationThreshold);
        return isActivated;
    }
}
