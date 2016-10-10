using UnityEngine;
using System.Collections;

public class RotateCharacterController : MonoBehaviour
{
    private float XSensitivity = 2.0f;
    private float YSensitivity = 2.0f;
    private bool ClampVerticalRotation = true;
    private float MinimumX = -90.0f;
    private float MaximumX = 90.0f;
    private bool Smooth;
    private float SmoothTime = 5.0f;
    private Quaternion CharacterRotation;
    private Quaternion CameraRotation;

    void Start ()
    {
        CharacterRotation = transform.localRotation;
        CameraRotation = Camera.main.transform.localRotation;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update ()
    {
        RotateView();
    }

    void RotateView()
    {
        float RotationY = Input.GetAxis("Mouse X") * XSensitivity;
        //Debug.Log(RotationY);
        float RotationX = Input.GetAxis("Mouse Y") * YSensitivity;

        CharacterRotation *= Quaternion.Euler(0f, RotationY, 0f);
        CameraRotation *= Quaternion.Euler(-RotationX, 0f, 0f);

        if (ClampVerticalRotation)
            CameraRotation = ClampRotationAroundXAxis(CameraRotation);

        if (Smooth)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, CharacterRotation,
                SmoothTime * Time.deltaTime);
            Camera.main.transform.localRotation = Quaternion.Slerp(Camera.main.transform.localRotation, CameraRotation,
                SmoothTime * Time.deltaTime);
        }
        else
        {
            transform.localRotation = CharacterRotation;
            Camera.main.transform.localRotation = CameraRotation;
        }
    }

    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
