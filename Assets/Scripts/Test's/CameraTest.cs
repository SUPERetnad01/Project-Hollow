using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private const float Y_ANGLE_MIN = 0.0f;
    [SerializeField]
    private const float Y_ANGLE_MAX = 50.0f;

    public bool lockCursor;
    public Transform lookAt;
    public float dstFromTarget = 5f;
    public float dstFactor = .8f;

    public float rotationSmoothTime = .12f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    public Vector2 distanceMinMax = new Vector2(1f, 4f);
    Vector3 dollyDir;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;

    private void Awake()
    {
        //camTransform = transform;
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void LateUpdate()
    {
        CamPan();
    }

    /// <summary>
    /// Panning the camera
    /// </summary>
    void CamPan()
    {
        currentX += Input.GetAxis("Look Horizontal") * sensitivityX ;
        currentY += Input.GetAxis("Look Vertical") * sensitivityY;
        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(currentY, currentX), ref rotationSmoothVelocity, rotationSmoothTime);
        transform.eulerAngles = currentRotation;

        transform.position = lookAt.position - transform.forward * dstFromTarget;
    }
}
