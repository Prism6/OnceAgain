using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //   ȰȭǹǷ, ÷̾  ũƮ 
using TMPro;

public class Ball : MonoBehaviour
{
    [Header("플레이어 설정")]
    public GameObject target;
    public float moveSpeed = 0.2f;
    public float jumpForce = 7;

    [Header("게임 규칙")]
    public float goalScore;
    public float maxFallCount;

    [Header("UI 요소")]
    public TextMeshProUGUI scoreText;
    public float fallcountText;

    [Header("씬 오브젝트")]
    public GameObject nextStage;
    public GameObject youSuck;
    public GameObject retryStage;

    [Header("오디오")]
    public AudioSource jumpSFX;


    // ----- 게임 진행 중 내부적으로 관리되는 변수들 -----
    public float score;
    public float fallcount;


    // ----- 스크립트 내부에서만 사용되는 비공개 변수들 -----
    private Rigidbody rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start() //    ߵ
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); // Ʈ ٵ 

    }

    // Update is called once per frame
    void FixedUpdate() // ߿ ؼ ߵ
    {
        float h = Input.GetAxis("Horizontal"); //Horizontal   ִ ġ ´.
        float v = Input.GetAxis("Vertical"); //Vertical   ִ ġ ´.

        //rb.velocity = new Vector3(h * moveSpeed, rb.velocity.y, v * moveSpeed); //  ־
        rb.AddForce(new Vector3(h * moveSpeed, 0, v * moveSpeed), ForceMode.Impulse); //  б (7 17 )
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
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1f); // ִ  
        Debug.DrawRay(transform.position, Vector3.down); // ʿ ǥ( )
                                                         //Debug.DrawRay(transform.position, new Vector3(0, -1,0));

        //(230714 ߰)
        isGrounded = Physics.Raycast(this.gameObject.transform.position, Vector3.down, out RaycastHit hit, 1f);
        //RaycastHit:     ݶ̴  (bool type = false, true)

        //bool hitbox = (isGrounded) ? hit.transform.CompareTag("Ground") : false; //isGrounded true տ, false ڿ
        //isGrounded (ĳƮ  ִ )
        //࿡  = false; ?(hit.transform.CompareTag("Ground") : false } >> false ȯ
        //࿡  = true; ?(hit.transform.CompareTag("Ground") : false } >> hit.transform.CompareTag("Ground") //±װ "׶" Ȯ 

        /*if (hitbox == true)  //hit.transform.CompareTag "Ground" 
        {
            GameObject Ground = hit.transform.gameObject; //  ׶ ̴.
            this.transform.parent = Ground.transform; // θ ׶尡 .
        }

        //if (hitbox == false)  //hit.transform.CompareTag "Ground" ƴ 
        //{
        //    this.transform.parent = null; // θ ׶尡 .
        //}
        */

        if (Input.GetButtonDown("Jump") && isGrounded) //"    "
        {
            jumpSFX.Play(); // Ҹ ߰ (7 18)
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
        score++; //score +1
        scoreText.text = $"Score : {score}";
    }

    public void FallCount()
    {
        fallcount++;
        fallcountText.text = $"Fall Count : {fallcount}";
    }
}