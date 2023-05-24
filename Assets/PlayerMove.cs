using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public float maxSpeed;
    SpriteRenderer spriteRenderer;
    Animator anim;
    //점프
    public float jumpPower;




    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rigid.freezeRotation = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    //즉각적인 키 입력, 키보드에 손을 뗐을때
    private void Update()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.5f, rigid.velocity.y);
        }

        if (Input.GetButtonUp("Horizontal"))
        {
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal")==-1);
        }

        //애니메이션  멈추었을때
        if(rigid.velocity.normalized.x == 0)
        {
            anim.SetBool("isWalk", false);
        }
        else
        {
            anim.SetBool("isWalk", true);
        }

        if (Input.GetButtonDown("Jump"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJump", true);
        }
    }


    private void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");

        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed)
        {
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if (rigid.velocity.x < maxSpeed * (-1))
        {
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        }
        if (rigid.velocity.y < 0)
        {
            // 레이 캐스트
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1,
                      LayerMask.GetMask("Platform"));

            //빔에 맞았는지

            if (rayHit.collider != null)
            {   //플레이어의 절반크기 만큼이여야 바닥에 닿은거
                if (rayHit.distance < 0.5f)
                {
                    anim.SetBool("isJump", false);
                    Debug.Log(rayHit.collider.name);
                }
            }
        }
       
    }
}