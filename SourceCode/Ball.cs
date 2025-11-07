using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //코인을 먹으면 코인이 비활성화되므로, 플레이어인 볼 스크립트에서 관리
using TMPro;

public class Ball : MonoBehaviour
{
    public GameObject target;

    Rigidbody rb;
    float moveSpeed = 0.2f;
    float jumpForce = 7;
    bool isGrounded;

    public TextMeshProUGUI scoreText;
    public float score;

    public TextMeshProUGUI fallcountText; //추락횟수 기록 TMP
    public float fallcount;

    public float maxSpeed;

    public GameObject nextStage;
    public float goalScore;

    public GameObject youSuck;
    public float maxFallCount;

    public GameObject retryStage;

    /* 사운드 추가 (7월 18일) */
    public AudioSource jumpSFX;

    // Start is called before the first frame update
    void Start() //시작할 때 한 번 발동
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); //공 오브젝트의 리지드바디에 접근

    }

    // Update is called once per frame
    void FixedUpdate() //게임 중에 계속해서 발동
    {
        float h = Input.GetAxis("Horizontal"); //Horizontal 이 갖고 있는 수치를 가져온다.
        float v = Input.GetAxis("Vertical"); //Vertical 이 갖고 있는 수치를 가져온다.

        //rb.velocity = new Vector3(h * moveSpeed, rb.velocity.y, v * moveSpeed); //방향으로 가속을 넣어줌
        rb.AddForce(new Vector3(h * moveSpeed, 0, v * moveSpeed), ForceMode.Impulse); //일정한 힘으로 밀기 (7월 17일 사용취소)
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(target.transform.forward * moveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(target.transform.forward * -moveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(target.transform.right * -moveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(target.transform.right * moveSpeed);
        }
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f); //땅에 있는 상태의 정의
        Debug.DrawRay(transform.position, Vector3.down); //레이저를 맵에 표시(씬에 생성)
                                                         //Debug.DrawRay(transform.position, new Vector3(0, -1,0));

        //(230714 추가)
        isGrounded = Physics.Raycast(this.gameObject.transform.position, Vector3.down, out RaycastHit hit, 1f);
        //RaycastHit: 레이저를 쐈을 때 만난 콜라이더의 정보 (bool type = false, true)

        //bool hitbox = (isGrounded) ? hit.transform.CompareTag("Ground") : false; //isGrounded가 true면 앞에꺼, false묜 뒤에꺼
        //isGrounded (레이캐스트가 쐈을때 있느냐 없느냐)
        //만약에 없을때 = false; ?(hit.transform.CompareTag("Ground") : false } >> false를 반환
        //만약에 있을때 = true; ?(hit.transform.CompareTag("Ground") : false } >> hit.transform.CompareTag("Ground") //태그가 "그라운드"인지 확인 

        /*if (hitbox == true)  //hit.transform.CompareTag가 "Ground"일 때
        {
            GameObject Ground = hit.transform.gameObject; //만난 놈이 그라운드라는 놈이다.
            this.transform.parent = Ground.transform; //공의 부모가 그라운드가 됨.
        }

        //if (hitbox == false)  //hit.transform.CompareTag가 "Ground"가 아닐 때
        //{
        //    this.transform.parent = null; //공의 부모가 그라운드가 됨.
        //}
        */

        if (Input.GetButtonDown("Jump") && isGrounded) //"점프뛸 때와 땅에 있을 때"
        {
            jumpSFX.Play(); //점프 소리 추가 (7월 18일)
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //rb.AddForce(new Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (score == goalScore)
        {
            nextStage.SetActive(true);
        }

        if (fallcount == maxFallCount)
        {
            youSuck.SetActive(true);
            retryStage.SetActive(true);
        }
    }

    public void PlusScore()
    {
        score++; //score에 +1
        scoreText.text = $"Score : {score}";
    }

    public void FallCount()
    {
        fallcount++;
        fallcountText.text = $"Fall Count : {fallcount}";
    }
}
