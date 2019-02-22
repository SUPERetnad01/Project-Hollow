using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private const float Y_ANGLE_MIN = 0.0f;
    [SerializeField]
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 10.0f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    //private float sensitivityX = 4.0f;
    //private float sensitivityY = 1.0f;

    private void Awake()
    {
        camTransform = transform;
    }

    private void Update()
    {
        CamPan();
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        camTransform.LookAt(lookAt.position);
    }

    /// <summary>
    /// Panning the camera
    /// </summary>
    void CamPan()
    {
        currentX += Input.GetAxis("Look Horizontal");
        currentY += Input.GetAxis("Look Vertical");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }
}
