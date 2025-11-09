using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject ball;
    public GameObject spawnPoint;
    public AudioSource deadSFX;

    public void RespawnBall() //공을 리스폰시키는 함수
    {
        ball.transform.position = spawnPoint.transform.position; //공의 위치를 스폰포인트의 위치로

        ball.GetComponent<Rigidbody>().Sleep(); //물리적 효과를 받는 공을 초기화
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
            GameManager.Instance.FallCount();
        }
    }
}