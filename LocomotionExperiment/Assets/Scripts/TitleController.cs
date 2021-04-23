using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    CanvasGroup canvasGroup;
    bool connect = false;

    public GameObject cam;
    public XRNode rightHandNode;
    public XRNode leftHandNode;
    public XRNode hdmNode;
    public InputDevice rightHand;
    public InputDevice leftHand;
    public InputDevice hmd;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("startAnimation", 38f);
        Invoke("enableConnect", 42f);

        rightHand = InputDevices.GetDeviceAtXRNode(rightHandNode);
        leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
        hmd = InputDevices.GetDeviceAtXRNode(hdmNode);
    }

    // Update is called once per frame
    void Update()
    {
        if (connect 
            && hmd.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 hmdPosition)
            && rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition)
            && rightPosition.y > hmdPosition.y
            && leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition)
            && leftPosition.y > hmdPosition.y)
        {
            SceneManager.LoadScene("TObjects");
        }
    }

    private void startAnimation()
    {
        GetComponent<Animator>().SetTrigger("play");
    }

    private void enableConnect()
    {
        connect = true;
    }
}
