using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float distance = 4f;
    public float height = 2f;

    public float mouseSensitivity = 80f;
    public float rotationSmooth = 8f;

    float yaw;
    float pitch;

    void Start()
    {
        if (target == null) return;

        yaw = transform.eulerAngles.y;
        pitch = transform.eulerAngles.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (target == null) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        Quaternion targetRot = Quaternion.Euler(pitch, yaw, 0);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSmooth * Time.deltaTime);

        // Posisi kamera
        Vector3 offset = transform.rotation * new Vector3(0, height, -distance);
        transform.position = target.position + offset;
    }
}
