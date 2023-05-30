using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 4);
    }

   void FixedUpdate()
    {
        rigid.velocity=new Vector2(nextMove,rigid.velocity.y);
        //몬스터 앞 체크
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove,
            rigid.velocity.y);
        Debug.DrawRay(frontVec, Vector3.down,new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if(rayHit.collider == null)
        {
            //nextMove =nextMove*(-1);
            //CancelInvoke();
            //Invoke("Think", 4);
        }
    }

    void Think()
    {
        nextMove=Random.Range(-1,2);

        Invoke("Think", 4); //재귀함수
    }
}
