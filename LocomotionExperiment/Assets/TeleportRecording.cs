using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportRecording : MonoBehaviour
{
    public void recordTeleport()
    {
        FirebaseManager.Instance.addTeleportAction();
    }
}
