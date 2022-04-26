using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [Header("Setting")]
    [SerializeField]
    private Vector3 touchStart;
    [SerializeField]
    private float minZoom = 1f;
    [SerializeField]
    private float maxZoom = 7f;
    [SerializeField]
    private float zoomMultiplier = 2f;

    [Header("Camera")]
    [SerializeField] private Camera cam;
    [SerializeField] private CinemachineVirtualCamera cineCam;
    [SerializeField] private Collider2D confiner;

    private void Awake()
    {
        if (!cam)
            cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (!cineCam)
            cineCam = GetComponent<CinemachineVirtualCamera>();

        if (!confiner)
            confiner = cineCam.GetComponent<CinemachineConfiner>().m_BoundingShape2D;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            touchStart = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if(Input.GetMouseButton(1))
        {     
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = direction + cineCam.transform.position;
            targetPosition.x = Mathf.Clamp(targetPosition.x, confiner.bounds.min.x, confiner.bounds.max.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, confiner.bounds.min.y, confiner.bounds.max.y);
            cineCam.transform.position = targetPosition;
        }

        ZoomCamera(Input.GetAxis("Mouse ScrollWheel") * zoomMultiplier);
    }

    void ZoomCamera(float increment)
    {
        cineCam.m_Lens.OrthographicSize = Mathf.Clamp(cineCam.m_Lens.OrthographicSize - increment, minZoom, maxZoom);
    }
}
