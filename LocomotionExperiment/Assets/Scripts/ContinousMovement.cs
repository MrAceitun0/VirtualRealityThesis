using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovement : MonoBehaviour
{
    public float speed = 1f;
    public XRNode inputSource;
    private Vector2 inputAxis;
    private CharacterController character;
    public XRRig rig;

    public float gravity = -9.81f;
    private float fallingSpeed;
    public LayerMask groundLayer;

    public float additionalHeight = 0.2f;

    public bool isMoving = false;
    public float movingTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        if (isMoving && movingTime > 0f && FirebaseManager.Instance.timerEnabled == false) //If player is moving until hitting the confetti, save last move
        {
            FirebaseManager.Instance.addFreeAction(movingTime);
            FirebaseManager.Instance.pushSceneInformation();
            isMoving = false;
            movingTime = 0f;
        }

        if (inputAxis.magnitude > 0f && FirebaseManager.Instance.timerEnabled == true)
        {
            isMoving = true;
            movingTime += Time.deltaTime;
        }

        if(inputAxis.magnitude == 0f && isMoving && movingTime > 0f)
        {
            isMoving = false;
            FirebaseManager.Instance.addFreeAction(movingTime);
            movingTime = 0f;
        }
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadSet();

        Quaternion yaw = Quaternion.Euler(0f, rig.cameraGameObject.transform.eulerAngles.y, 0f);
        Vector3 direction = yaw * new Vector3(inputAxis.x, 0, inputAxis.y);
        character.Move(direction * speed * Time.fixedDeltaTime);

        if(CheckIfGrounded())
        {
            fallingSpeed = 0f;
        }
        else
        {
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }

        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
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
}
