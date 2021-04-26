using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class ArmSwing : MonoBehaviour
{
    private CharacterController character;
    public XRRig rig;
    public float gravity = -9.81f;
    private float fallingSpeed;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    public GameObject cam;
    public XRNode rightHandNode;
    public XRNode leftHandNode;
    public InputDevice rightHand;
    public InputDevice leftHand;

    public float speed = 5f;

    public bool isMoving = false;
    public float movingTime = 0f;

    public ContinousMovement continousMovement;

    private void Start()
    {
        character = GetComponent<CharacterController>();

        rightHand = InputDevices.GetDeviceAtXRNode(rightHandNode);
        leftHand = InputDevices.GetDeviceAtXRNode(leftHandNode);
    }

    private void Update()
    {
        if (isMoving && movingTime > 0f && FirebaseManager.Instance.timerEnabled == false) //If player is moving until hitting the confetti, save last move
        {
            FirebaseManager.Instance.addWalkAction(movingTime);
            FirebaseManager.Instance.pushSceneInformation();
            isMoving = false;
            movingTime = 0f;
        }

        if (primaryButtonsArePressed() && handsHaveVelocity() && FirebaseManager.Instance.timerEnabled == true)
        {
            isMoving = true;
            movingTime += Time.deltaTime;
        }

        if ((!primaryButtonsArePressed() || !handsHaveVelocity()) && isMoving && movingTime > 0f)
        {
            isMoving = false;
            FirebaseManager.Instance.addWalkAction(movingTime);
            movingTime = 0f;
        }
    }

    void FixedUpdate()
    {
        float yRotation = cam.transform.eulerAngles.y;
        cam.transform.eulerAngles = new Vector3(0, yRotation, 0);

        if (primaryButtonsArePressed()
            && Time.timeSinceLevelLoad > 1f
            && handsHaveVelocity())
        {
            if(continousMovement != null)
                continousMovement.enabled = false;

            CapsuleFollowHeadSet();

            character.Move(cam.transform.forward * speed * Time.fixedDeltaTime);

            if (CheckIfGrounded())
            {
                fallingSpeed = 0f;
            }
            else
            {
                fallingSpeed += gravity * Time.fixedDeltaTime;
            }

            character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        }

        if (continousMovement != null)
            continousMovement.enabled = true;
    }

    private void CapsuleFollowHeadSet()
    {
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }

    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        return Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit info, rayLength, groundLayer);
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
