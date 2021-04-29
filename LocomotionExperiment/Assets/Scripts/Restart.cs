using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public XRNode leftHandNode;
    public InputDevice leftHand;
    bool menuPressign = false;
    // Start is called before the first frame update
    void Start()
    {
        leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
    }

    // Update is called once per frame
    void Update()
    {
        leftHand.TryGetFeatureValue(CommonUsages.menuButton, out bool menuPressign);
        if(menuPressign)
        {
            FirebaseManager.Instance.gameTime = 0;
            FirebaseManager.Instance.totalFree = 0f;
            FirebaseManager.Instance.totalWalk = 0f;
            FirebaseManager.Instance.totalTeleport = 0;
            FirebaseManager.Instance.totalSnap = 0;

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
