using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;
    public Ball ballScript;

    public AudioSource collectCoinSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime); //거꾸로 돌리고 싶으면 -값을 입력한다.
        // transform.Rotate(new Vector3(0, 0, 60) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) //트리거에 들어왔을 때 발동(부딪히는 콜라이더의 이름을 other로 지정)
    {
        if (other.gameObject.CompareTag("Player")) //부딪힌 콜라이더의 태그가 "Player일 때"
        {
            collectCoinSFX.Play();//코인 소리 추가 (7월 18일)
            Debug.Log("콜라이더 충돌 감지");
            ballScript.PlusScore(); //PlusScore는 여기에 호출해야 함

            this.gameObject.SetActive(false);

        }
    }
}
