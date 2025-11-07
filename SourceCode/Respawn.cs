using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject ball;
    public Ball ballsc;
    public GameObject spawnPoint;
    public AudioSource deadSFX;

    public void RespawnBall() //리스폰 볼이라는 임의의 함수
    {
        ball.transform.position = spawnPoint.transform.position; //볼 위치를 스폰포인트 위치로

        ball.GetComponent<Rigidbody>().Sleep(); //물리작용 및 힘 초기화
        //ball.GetComponent <Rigidbody>().velocity = new Vector3(0, 0, 0);
        //ball.GetComponent<Rigidbody>().isKinematic = true;
        //ball.GetComponent<Rigidbody>().isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            deadSFX.Play();
            RespawnBall();
            ballsc.FallCount();
        }
    }
}
