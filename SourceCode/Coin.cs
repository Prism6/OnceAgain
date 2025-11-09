using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;

    public AudioSource collectCoinSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime); //코인이 빙글빙글 돌도록 Z축을 기준으로 회전시킵니다.
        // transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) //트리거에 무언가 닿았을 때 발동 (닿은 콜라이더의 정보를 other에 저장)
    {
        if (other.gameObject.CompareTag("Player")) //닿은 콜라이더의 태그가 "Player"일 때
        {
            collectCoinSFX.Play(); //코인 획득 사운드 재생
            Debug.Log("콜라이더 충돌 감지");
            GameManager.Instance.PlusScore(); //GameManager의 점수 증가 함수 호출

            this.gameObject.SetActive(false); //코인 비활성화

        }
    }
}