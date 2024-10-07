using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    Rotater rotater;
    BallClicker ballclicker;
    [SerializeField]
    TextMeshProUGUI box_counter;
    [SerializeField]
    TextMeshProUGUI rainbow_counter;
    [SerializeField]
    TextMeshProUGUI level_counter;
    [SerializeField]
    GameObject HighWhite;
    int rainbow_count;
    int alive_boxcount;
    public bool rainbow_inuse=false;
    int level;
    int level_turu;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        rotater = GameObject.FindGameObjectWithTag("Rotater").GetComponent<Rotater>();
        ballclicker = GameObject.FindGameObjectWithTag("BallClicker").GetComponent<BallClicker>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        level = SceneManager.GetActiveScene().buildIndex;
        rainbow_count = PlayerPrefs.GetInt("rainbow", 25);
        rainbow_counter.text = rainbow_count.ToString();
        level_turu = PlayerPrefs.GetInt("levelrego", 0);
        level_counter.text = (level_turu * SceneManager.sceneCountInBuildSettings + level + 1).ToString();
    }

    public void Update_BoxTextFirstTime(int alivebox)
    {
        alive_boxcount = alivebox;
        box_counter.text = alivebox.ToString();
    }

    void Update_BoxText()
    {
        box_counter.text = alive_boxcount.ToString();
    }
    public void Use_ColorfulAbility()
    {
        if (rainbow_inuse == false)
        {
            rainbow_count--;
            rainbow_counter.text = rainbow_count.ToString();
            ballclicker.Create_Rainbowball(alive_boxcount);
            rainbow_inuse = true;
        }
    }

    public void HitBox_AfterMath(bool truehit)
    {
        if (truehit == true)
        {
            alive_boxcount--;
            Update_BoxText();
            if (alive_boxcount == 0)
            {
                ballclicker.gameStage = false;
                StartCoroutine(TrueEnd());
            }
        }
        else
        {
            //finishGame
            ballclicker.gameStage = false;
            StartCoroutine(FalseEnd());
        }
    }
    IEnumerator TrueEnd()
    {
        PlayerPrefs.SetInt("rainbow", rainbow_count);
        int max_levelcount = SceneManager.sceneCountInBuildSettings-1;
        if (level == max_levelcount)
        {
            PlayerPrefs.SetInt("level", 1);
            PlayerPrefs.SetInt("levelrego", level_turu+1);
        }
        else
        {
            PlayerPrefs.SetInt("level", level + 1);
        }
        
        HighWhite.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level+1);
    }

    IEnumerator FalseEnd()
    {
        rotater.gameStage = false;
        PlayerPrefs.SetInt("rainbow", rainbow_count);
        HighWhite.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(level);
    }

    public void BallClick_Button()
    {
        ballclicker.Throw_Ball();
    }
}
