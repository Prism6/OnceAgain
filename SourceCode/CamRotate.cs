using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    public GameObject target;

    float rotateSpeed = 4;

    public float Yaxis;
    public float Xaxis;
    private float rotSensitive = 3f;
    private float dis = 4f;
    private float smoothTime = 0.12f;

    private Vector3 targetRotation;
    private Vector3 currentVel;

    // Start is called before the first frame update
    void Start()
    {
        target.transform.position = player.transform.position;
        //offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Yaxis = Yaxis + Input.GetAxis("Mouse X") * rotSensitive;
        //Xaxis = Xaxis - Input.GetAxis("Mouse Y") * rotSensitive;

        targetRotation = Vector3.SmoothDamp(targetRotation, new Vector3(Xaxis, Yaxis), ref currentVel, smoothTime);
        //타겟 로테이션에서 뉴벡터의 로테이션 값까지 속도는 커렌트벨만큼. 거기까지 걸리는 시간이 스무스 타임
        //부드럽게 카메라가 도는 듯한 느낌을 부여
        target.transform.eulerAngles = targetRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.transform.position + offset;
    }
}
