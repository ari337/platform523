using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
   
    public int totalPoint;  //전체점수
    public int stagePoint;  //스테이지 상의 점수 
    public int stageIndex;  //스테이지 번호 
    public int health;
    public PlayerMove player;
    public GameObject[] Stages;

    public Image[] UIHealth;
    public Text UIPoint;
    public Text  UIStage;
    public GameObject RestartBtn;

    private void Update()
    {
        UIPoint.text = (totalPoint + stagePoint).ToString();
    }



    //점수 와 스테이지 를 관리 한다.

    public void NextStage()
    {
        if (stageIndex < Stages.Length - 1)
        {
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            PlayerReposition();

            UIStage.text = "STAGE" + stageIndex + 1;
        }
        else
        {
            //게임 클리어
            Time.timeScale = 0;

            //결과 UI
            Debug.Log("게임 클리어");

            //재시작 버튼 UI
           
            Text btnText=RestartBtn.GetComponentInChildren<Text>();
            btnText.text = "Game Clear";
            RestartBtn.SetActive(true);

        }
        totalPoint += stagePoint;
        stagePoint = 0;
    }

    public void HealthDown()
    {
        if (health > 1)
        {
            health--;
            UIHealth[health].color = new Color(1, 0, 0, 0.4f);
        }
        else
        {
            //All Health UI
            UIHealth[0].color = new Color(1, 0, 0, 0.4f);

            //플레이어 다이 이팩트
            player.OnDie();
            //결과 UI
            Debug.Log("You 디졌다.");
            //다시시작 버튼 UI
            RestartBtn.SetActive(true);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(health > 1)
            {
                //플레이어 리스폰
                PlayerReposition();
            }
            HealthDown();
        }    

        //플레이어 리스폰 함수 만들기
    
       

       
    }
    void PlayerReposition()
    {
        player.transform.position = new Vector3(-7, 1, -1);
        player.velocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
