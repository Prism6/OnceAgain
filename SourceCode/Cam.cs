using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public GameObject target;
    float rotateSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        target.transform.position = player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //transform.position = player.transform.position + offset;
        CamRotate(); // 7월 17일 추가
    }

    void CamRotate()
    {
        if (Input.GetKey(KeyCode.K))
        {
            target.transform.Rotate(Vector3.up, -45f * rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.L))
        {
            target.transform.Rotate(Vector3.up, 45f * rotateSpeed * Time.deltaTime);
        }
    }
}
