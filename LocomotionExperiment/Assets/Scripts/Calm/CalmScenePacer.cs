using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class CalmScenePacer : MonoBehaviour
{
    public Text intro;

    public Text title;
    public Text cup;
    public Text skeleton;
    public Text fish;
    public Text beer;
     
    public Text end;

    public Font normalFont;

    public GameObject cam;
    public XRNode rightHandNode;
    public XRNode leftHandNode;
    public XRNode hdmNode;
    public InputDevice rightHand;
    public InputDevice leftHand;
    public InputDevice hmd;

    public int stage = 0;


    public AudioSource src;
    public AudioClip hugeSound;

    public LayerMask everything;
    public LayerMask nothing;

    private static CalmScenePacer _instance;

    public static CalmScenePacer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<CalmScenePacer>();
            }

            return _instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rightHand = InputDevices.GetDeviceAtXRNode(rightHandNode);
        leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
        hmd = InputDevices.GetDeviceAtXRNode(hdmNode);
    }

    // Update is called once per frame
    void Update()
    {
        if (handsOverHead() && stage == 0)
        {
            src.PlayOneShot(hugeSound);

            stage++;
            FirebaseManager.Instance.enableGameTime();

            intro.enabled = false;
            title.enabled = true;
            fish.enabled = true;
            beer.enabled = true;
            skeleton.enabled = true;
            cup.enabled = true;
        } 
        else if (stage == 1)
        {
            CalmObject[] objects = FindObjectsOfType<CalmObject>();
            foreach(CalmObject obj in objects)
            {
                if (obj.gameObject.tag == "Cup")
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
                else
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if (stage == 2)
        {
            skeleton.font = normalFont;
            CalmObject[] objects = FindObjectsOfType<CalmObject>();
            foreach (CalmObject obj in objects)
            {
                if (obj.gameObject.tag == "Skeleton")
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
                else
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if (stage == 3)
        {
            beer.font = normalFont; 
            CalmObject[] objects = FindObjectsOfType<CalmObject>();
            foreach (CalmObject obj in objects)
            {
                if (obj.gameObject.tag == "Beer")
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
                else
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if(stage == 4)
        {
            fish.font = normalFont;
            CalmObject[] objects = FindObjectsOfType<CalmObject>();
            foreach (CalmObject obj in objects)
            {
                if(obj.gameObject.tag == "Fish")
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
                else
                    obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if (stage == 5)
        {
            FirebaseManager.Instance.disableGameTime();
            FirebaseManager.Instance.pushSceneInformation();

            CalmObject[] objects = FindObjectsOfType<CalmObject>();
            foreach (CalmObject obj in objects)
            {
                obj.gameObject.GetComponent<MeshCollider>().enabled = false;
            }

            src.PlayOneShot(hugeSound);

            stage++;
            title.enabled = false;
            fish.enabled = false;
            beer.enabled = false;
            skeleton.enabled = false;
            cup.enabled = false;
            end.enabled = true;
        }
        else if(stage == 6 && handsOverHead())
        {
            FirebaseManager.Instance.gameTime = 0;
            FirebaseManager.Instance.totalFree = 0f;
            FirebaseManager.Instance.totalWalk = 0f;
            FirebaseManager.Instance.totalTeleport = 0;
            FirebaseManager.Instance.totalSnap = 0;

            SceneManager.LoadScene("Stress");
        }
    }

    private bool handsOverHead()
    {
        return hmd.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 hmdPosition)
                    && rightHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 rightPosition)
                    && rightPosition.y > hmdPosition.y
                    && leftHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 leftPosition)
                    && leftPosition.y > hmdPosition.y;
    }
}