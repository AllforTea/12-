using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClicker : MonoBehaviour
{
    bool ready_tothrow = true;
    public bool gameStage = true;
    [SerializeField]
    float reload_time;
    [SerializeField]
    GameObject rainbow_ball;
    [SerializeField]
    Vector3 reload_distance;
    GameObject thrown_balls;
    GameController gamecontroller;

    private void Start()
    {
        gamecontroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        thrown_balls = new GameObject("Throwns");
        Reposition_Atstart();
        gamecontroller.Update_BoxTextFirstTime(transform.childCount);
    }
    public void Throw_Ball()
    {
        if (ready_tothrow == true & gameStage==true)
        {
            GameObject thrown_ball = transform.GetChild(0).gameObject;
            thrown_ball.GetComponent<BallShot>().shooted = true;
            thrown_ball.transform.parent = thrown_balls.transform;
            ready_tothrow = false;
            StartCoroutine(tothrow_count());
            reload_balls(1);
            
        }
    }
    public void Create_Rainbowball(int ballcount)
    {
        for(int changed_ballcount = 0; changed_ballcount < ballcount; changed_ballcount++)
        {
            GameObject changed_ball = transform.GetChild(changed_ballcount).gameObject;
            Vector3 change_position = changed_ball.transform.position;
            GameObject created_rainbow = Instantiate(rainbow_ball, change_position, transform.rotation);
            Destroy(changed_ball);
            created_rainbow.transform.parent = transform;
        }
        
    }
    //Toplatýn yeniden konumlanmasý için kod
    public void reload_balls(int birsifir)
    {
        // Toplarý yeniden konumlandýrýyor 1 se ileriye doðru 0 sa geriye doðru
        if (birsifir == 1)
        {
            Debug.Log("BallReload1");
            if (transform.childCount > 0)
            {
                GameObject reloaded_ball;
                reloaded_ball = transform.GetChild(0).gameObject;
                reloaded_ball.GetComponent<BallShot>().reload_position = transform.position;
                if (transform.childCount > 1)
                {
                    reloaded_ball = transform.GetChild(1).gameObject;
                    reloaded_ball.GetComponent<BallShot>().reload_position = transform.position + reload_distance;
                    if (transform.childCount > 2)
                    {
                        reloaded_ball = transform.GetChild(2).gameObject;
                        reloaded_ball.SetActive(true);
                    }
                }

            }
        }
        else
        {
            Debug.Log("BallReload0");
            // Gökkuþaðý topu eklendiðinde onun için arkaya kayan top düzeni
            if (transform.childCount > 1)
            {
                Debug.Log("Relaod0.1");
                GameObject reloaded_ball;
                reloaded_ball = transform.GetChild(1).gameObject;
                reloaded_ball.GetComponent<BallShot>().reload_position = transform.position + reload_distance;
                if (transform.childCount > 2)
                {
                    Debug.Log("Reload0.2");
                    reloaded_ball = transform.GetChild(2).gameObject;
                    reloaded_ball.GetComponent<BallShot>().reload_position = transform.position + reload_distance*2;
                    if (transform.childCount > 3)
                    {
                        Debug.Log("Reload0.3");
                        reloaded_ball = transform.GetChild(3).gameObject;
                        reloaded_ball.SetActive(false);
                    }
                }

            }
        }
        
    }
    IEnumerator tothrow_count()       
    {
        //Oyuncu tekrar topu fýrlatabilmesi için bir süre bekletiyor.
        yield return new WaitForSeconds(reload_time);
        ready_tothrow = true;
    }

    void Reposition_Atstart()
    {
        for(int a = 0; a < transform.childCount; a++)
        {
            if (a < 3)
            {
                GameObject positioning_ball = transform.GetChild(a).gameObject;
                positioning_ball.transform.position = transform.position + reload_distance * a;
                positioning_ball.SetActive(true);
            }
            else
            {
                GameObject positioning_ball = transform.GetChild(a).gameObject;
                positioning_ball.transform.position = transform.position + reload_distance * 2;
            }
           
        }
    }
}
