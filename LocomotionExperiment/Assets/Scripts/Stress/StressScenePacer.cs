﻿using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class StressScenePacer : MonoBehaviour
{
    public Text intro;

    public Text title;
    public Text hacha;
    public Text escudo;
    public Text sierra;
    public Text saco;

    public Text timeName;
    public Text timer;
    public Image gold;
    public Text goldName;
    public Image silver;
    public Text silverName;
    public Image bronze;
    public Text bronzeName;

    public Sprite goldSprite;
    public Sprite silverSprite;
    public Sprite bronzeSprite;

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

    private static StressScenePacer _instance;

    public static StressScenePacer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<StressScenePacer>();
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
        float totalTime = FirebaseManager.Instance.gameTime;
        int minutes = Mathf.FloorToInt(totalTime / 60F);
        int seconds = Mathf.FloorToInt(totalTime - minutes * 60);
        if(minutes == 1)
        {
            timer.text = minutes + " minute\n" + seconds + " seconds"; 
        }
        else if (minutes >= 2)
        {
            timer.text = minutes + " minutes\n" + seconds + " seconds";
        }
        else
        {
            timer.text = seconds + " seconds";
        }


        if (handsOverHead() && stage == 0)
        {
            src.PlayOneShot(hugeSound);

            stage++;
            FirebaseManager.Instance.enableGameTime();

            intro.enabled = false;
            title.enabled = true;
            sierra.enabled = true;
            saco.enabled = true;
            escudo.enabled = true;
            hacha.enabled = true;

            timeName.enabled = true;
            timer.enabled = true;
        }
        else if (stage == 1)
        {
            foreach (BattleAxeController obj in FindObjectsOfType<BattleAxeController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
            }

            foreach (ShieldController obj in FindObjectsOfType<ShieldController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (SawController obj in FindObjectsOfType<SawController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (BagController obj in FindObjectsOfType<BagController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if (stage == 2)
        {
            escudo.font = normalFont;
            foreach (BattleAxeController obj in FindObjectsOfType<BattleAxeController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (ShieldController obj in FindObjectsOfType<ShieldController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
            }

            foreach (SawController obj in FindObjectsOfType<SawController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (BagController obj in FindObjectsOfType<BagController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if (stage == 3)
        {
            sierra.font = normalFont;
            foreach (BattleAxeController obj in FindObjectsOfType<BattleAxeController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (ShieldController obj in FindObjectsOfType<ShieldController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (SawController obj in FindObjectsOfType<SawController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
            }

            foreach (BagController obj in FindObjectsOfType<BagController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }
        }
        else if (stage == 4)
        {
            saco.font = normalFont;
            foreach (BattleAxeController obj in FindObjectsOfType<BattleAxeController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (ShieldController obj in FindObjectsOfType<ShieldController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (SawController obj in FindObjectsOfType<SawController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (BagController obj in FindObjectsOfType<BagController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = everything;
            }
        }
        else if (stage == 5)
        {
            FirebaseManager.Instance.disableGameTime();
            FirebaseManager.Instance.pushSceneInformation();

            foreach (BattleAxeController obj in FindObjectsOfType<BattleAxeController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (ShieldController obj in FindObjectsOfType<ShieldController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (SawController obj in FindObjectsOfType<SawController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            foreach (BagController obj in FindObjectsOfType<BagController>())
            {
                obj.gameObject.GetComponent<XRGrabInteractable>().interactionLayerMask = nothing;
            }

            src.PlayOneShot(hugeSound);

            stage++;

            title.enabled = false;
            sierra.enabled = false;
            saco.enabled = false;
            escudo.enabled = false;
            hacha.enabled = false;
            end.enabled = true;

            gold.enabled = false;
            silver.enabled = true;
            if(Mathf.FloorToInt(FirebaseManager.Instance.gameTime) <= 120)
            {
                silver.sprite = goldSprite;
                end.text = "Congratulations, you have obtained the gold medal.\nThank you so much for playing.";
            } 
            else if (Mathf.FloorToInt(FirebaseManager.Instance.gameTime) > 120
                && Mathf.FloorToInt(FirebaseManager.Instance.gameTime) <= 240)
            {
                silver.sprite = silverSprite;
                end.text = "Congratulations, you have obtained the silver medal.\nThank you so much for playing.";
            } 
            else if (FirebaseManager.Instance.gameTime > 240)
            {
                silver.sprite = bronzeSprite;
                end.text = "Congratulations, you have obtained the bronze medal.\nThank you so much for playing.";
            }
            bronze.enabled = false;
            goldName.enabled = false;
            silverName.enabled = false;
            bronzeName.enabled = false;

            timeName.enabled = true;
            timer.enabled = true;
        }
        else if (stage == 6 && handsOverHead())
        {
            SceneManager.LoadScene("Info");
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