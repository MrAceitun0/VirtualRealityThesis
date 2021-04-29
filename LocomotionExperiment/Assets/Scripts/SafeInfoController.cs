using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeInfoController : MonoBehaviour
{
    public Text tp;
    public Text walk;
    public Text free;
    public Text calm;
    public Text stress;

    // Start is called before the first frame update
    void Start()
    {
        tp.text = FirebaseManager.Instance.teleportInfo;
        walk.text = FirebaseManager.Instance.walkInfo;
        free.text = FirebaseManager.Instance.freeInfo;
        calm.text = FirebaseManager.Instance.calmInfo;
        stress.text = FirebaseManager.Instance.stressInfo;
    }
}
