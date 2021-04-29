using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class SnapRecorder : MonoBehaviour
{
    public XRNode inputSource;
    InputDevice device;
    float snapTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FirebaseManager.Instance.timerEnabled)
        {
            snapTime -= Time.fixedDeltaTime;
            Vector2 inputAxis;
            device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
            if ((inputAxis.x < -0.75f || inputAxis.x > 0.75f) && snapTime < 0f)
            {
                FirebaseManager.Instance.addSnapAction();
                snapTime = 0.51f;
            }
        }
    }
}
