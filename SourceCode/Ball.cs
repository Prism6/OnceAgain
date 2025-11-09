using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("플레이어 설정")]
    public GameObject target;
    public float moveSpeed = 0.2f;
    public float jumpForce = 7;

    [Header("오디오")]
    public AudioSource jumpSFX;

    // 스크립트 내부에서만 사용되는 비공개 변수들
    private Rigidbody rb;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start() // 시작할 때 한 번 발동
    {
        rb = this.gameObject.GetComponent<Rigidbody>(); //공 오브젝트의 리지드바디에 접근

    }

    // Update is called once per frame
    void FixedUpdate() // 게임 중에 계속해서 발동 (개선안)
    {
        // 1. 키보드 및 게임패드의 상/하/좌/우 입력을 받습니다. (-1.0f ~ 1.0f 사이의 값)
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // 2. 'target'(아마도 카메라)의 방향을 기준으로 움직일 방향 벡터를 계산합니다.
        //    - target의 정면 방향(forward)으로 v(상하) 만큼
        //    - target의 오른쪽 방향(right)으로 h(좌우) 만큼
        Vector3 moveDirection = (target.transform.forward * v) + (target.transform.right * h);

        // 3. Rigidbody에 지속적인 힘(ForceMode.Force)을 가하여 구슬을 밉니다.
        //    - .normalized를 통해 대각선으로 이동할 때 더 빨라지는 것을 방지합니다.
        //    - 이 방식은 무거운 물체를 꾸준히 미는 듯한 관성을 만들어 냅니다.
        rb.AddForce(moveDirection.normalized * moveSpeed, ForceMode.Force);
    }

    private void Update()
    {
        // 1. 바닥 감지: 공의 바로 아래로 Raycast를 쏘아 땅에 닿았는지 확인합니다.
        // out RaycastHit hit 변수를 통해 부딪힌 대상의 정보를 얻을 수 있습니다. (현재는 사용되지 않음)
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f);

        // 디버깅용: Scene 뷰에서 Raycast를 시각적으로 표시합니다. (길이와 색상 지정)
        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.red);

        // 2. 점프: 땅에 닿아있고 "Jump" 버튼(기본값: 스페이스바)을 누르면 점프합니다.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpSFX.Play();
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}