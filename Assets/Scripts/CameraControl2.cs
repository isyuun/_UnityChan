using UnityEngine;
using System.Collections;
using System;

public class CameraControl2 : MonoBehaviour
{

    GameObject cameraParent;

    Vector3 defaultPosition;    // 초기 좌표 저장
    Quaternion defaultRotation; // 초기 각도 저장
    float defaultZoom;      // 초기 줌 저장

    /*const */
    float speedFov = 30.0f;
    /*const */
    float speedPos = 5.0f;
    /*const */
    float FOV_MIN = 30.0f, FOV_MAX = 110.0f;
    /*const */
    float POS_MIN = -15.0f, POS_MAX = -2.0f;

    // Use this for initialization
    void Start()
    {

        // 카메라의 부모를 얻는다
        cameraParent = GameObject.Find("CameraParent");

        // 기본 위치를 저장한다
        defaultPosition = Camera.main.transform.position;
        defaultRotation = cameraParent.transform.rotation;
        defaultZoom = Camera.main.fieldOfView;

        POS_MAX = defaultPosition.z;
        FOV_MIN = defaultZoom;
    }

    // Update is called once per frame
    void Update()
    {

        // 카메라 이동
        if (Input.GetMouseButton(0))
        {

            Camera.main.transform.Translate(Input.GetAxisRaw("Mouse X") / 10, Input.GetAxisRaw("Mouse Y") / 10, 0);
        }

        // 카메라 회전
        if (Input.GetMouseButton(1))
        {

            cameraParent.transform.Rotate(Input.GetAxisRaw("Mouse Y") * 10, Input.GetAxisRaw("Mouse X") * 10, 0);
        }

        FovInOut(true);
        PosInOut(false);

        // 카메라 위치 초기화
        if (Input.GetMouseButton(2))
        {

            Camera.main.transform.position = defaultPosition;
            cameraParent.transform.rotation = defaultRotation;
            Camera.main.fieldOfView = defaultZoom;
        }
    }

    /// <summary>
    ///  인, 줌 아웃
    /// </summary>
    private void FovInOut(bool foward = true)
    {
        // 역방향
        float direct = speedFov;

        // 순방향
        if (foward)
        {
            direct = -speedFov;
        }

        Vector3 pos = Camera.main.transform.position;
        if (pos.z >= POS_MIN && pos.z <= POS_MAX)
        {
            Camera.main.fieldOfView += (direct * Input.GetAxis("Mouse ScrollWheel"));
        }


        //카메라FOV.최소.초기화
        if (Camera.main.fieldOfView < FOV_MIN)
        {
            Camera.main.fieldOfView = FOV_MIN;
        }
        //카메라FOV.최대.초기화
        if (Camera.main.fieldOfView > FOV_MAX)
        {
            Camera.main.fieldOfView = FOV_MAX;
        }

    }

    /// <summary>
    /// 카메라FOV.z축.이동
    /// </summary>
    private void PosInOut(bool foward = true)
    {
        // 역방향
        float direct = -speedPos;

        // 순방향
        if (foward)
        {
            direct = speedPos;
        }


        float fov = Camera.main.fieldOfView;
        if (fov >= FOV_MIN && fov <= FOV_MAX)
        {
            Camera.main.transform.Translate(0, 0, (direct * Input.GetAxis("Mouse ScrollWheel")));
        }

        Vector3 pos = Camera.main.transform.position;
        //카메라FOV.최소.초기화
        if (pos.z < POS_MIN)
        {
            pos.z = POS_MIN;
            Camera.main.transform.position = pos;
        }
        //카메라FOV.최대.초기화
        if (pos.z > POS_MAX)
        {
            pos.z = POS_MAX;
            Camera.main.transform.position = pos;
        }

    }
}

