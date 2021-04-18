using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ArmSwing : MonoBehaviour
{
    public GameObject cam;
    public XRNode rightHandNode;
    public XRNode leftHandNode;
    public InputDevice rightHand;
    public InputDevice leftHand;

    public float speed = 5f;

    private void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(rightHandNode);
        leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
    }

    void Update()
    {
        float yRotation = cam.transform.eulerAngles.y;
        cam.transform.eulerAngles = new Vector3(0, yRotation, 0);

        if (primaryButtonsArePressed() 
            && Time.timeSinceLevelLoad > 1f 
            && handsHaveVelocity())
        {
            transform.position += cam.transform.forward * speed * Time.deltaTime;
        }
    }

    private bool primaryButtonsArePressed()
    {
        return rightHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValueRight)
                    && primaryButtonValueRight
                    && leftHand.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValueLeft)
                    && primaryButtonValueLeft;
    }

    private bool handsHaveVelocity()
    {
        return rightHand.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 rightVelocity)
            && rightVelocity.magnitude > 0.1f
            && leftHand.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 leftVelocity)
            && leftVelocity.magnitude > 0.1f;
    }
}
