using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

public class FollowCamera : MonoBehaviour
{
    public Camera cam = null;
    public GameObject targetObject = null;

    public float distance = 6.0f;
    public float zoomDistance = 1.0f;
    public float lerp = 10.0f;
    private float nowDistance;
    private float targetDistance = 0.0f;

    private float targetZoomCenterY = 2.0f;

    public float yMinLimit = -20.0f;
    public float yMaxLimit = 80.0f;
    public Vector3 vector = new Vector3(0.0f, 0.0f, 0.0f);
    public Vector2 center;
    public float height;
    public bool viewMode = false;

    public float multiple = 150.0f;


    public bool mouseLock = true;

    public void SetMouseLock(bool mouseLock)
    {
        this.mouseLock = mouseLock;

        if (mouseLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
            Cursor.lockState = CursorLockMode.None;

        Cursor.visible = !mouseLock;
    }

    static FollowCamera instance = null;
    public static FollowCamera Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        if (cam == null)
        {
            cam = GetComponent<Camera>();
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, min, max);
    }

    void Start()
    {
        SetMouseLock(true);
        var angles = transform.eulerAngles;

        vector.x = angles.y;
        vector.y = angles.x;

        targetDistance = nowDistance = distance;
    }

    public void SetZoom(bool zoomMode)
    {
        if (zoomMode)
        {
            targetDistance = zoomDistance;
            targetZoomCenterY = 1.0f;
        }
        else
        {
            targetDistance = distance;
            targetZoomCenterY = 2.0f;
        }
    }

    public void Punch(float power)
    {
        vector.x -= power * Time.fixedDeltaTime;

        int sign = Random.Range(-1, 2);
        vector.y += sign * power * Time.fixedDeltaTime * 0.05f;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            multiple -= 10.0f;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            multiple += 10.0f;

        if (targetObject == null)
        {
            return;
        }

        if (mouseLock)
        {
            vector.y += Input.GetAxis("Mouse X") * multiple * Time.fixedDeltaTime;
            vector.x -= Input.GetAxis("Mouse Y") * multiple * Time.fixedDeltaTime;

            nowDistance = Mathf.Lerp(nowDistance, targetDistance, Time.fixedDeltaTime * lerp);
            center.y = Mathf.Lerp(center.y, targetZoomCenterY, Time.fixedDeltaTime * lerp);

            vector.x = ClampAngle(vector.x, yMinLimit, yMaxLimit);

            transform.rotation = Quaternion.Euler(vector);
            transform.position = Vector3.Lerp(transform.position, transform.rotation * new Vector3(center.x, center.y, -nowDistance) + targetObject.transform.position + new Vector3(0.0f, height, 0.0f), Time.fixedDeltaTime * lerp * 10);
        }

        int layer = 1 << 8;

        RaycastHit hit;

        if (Physics.Raycast(targetObject.transform.position + new Vector3(0.0f, 0.5f, 0.0f), transform.position - targetObject.transform.position, out hit, nowDistance, layer))
        {
            transform.position = hit.point;
        }
    }
}
