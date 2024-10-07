using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Rotater : MonoBehaviour
{
    [SerializeField]
    bool rotation_change = false;
    [SerializeField]
    float change_time = 0;
    [SerializeField]
    float speed1;
    [SerializeField]
    float speed2;
    GameController gamecontroller;
    public float rotation_speed;
    int child_count;
    public bool gameStage = true;
    // Start is called before the first frame update
    void Start()
    {
        gamecontroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rotation_speed = speed1;
        child_count = transform.childCount;
        gamecontroller.Update_BoxTextFirstTime(child_count);
        Debug.Log(child_count);
        for(int child_num = 0; child_num < child_count; child_num++)
        {
            Debug.Log("Change Child Rotate" + child_num.ToString());
            transform.GetChild(child_num).rotation = Quaternion.Euler(360 * child_num / child_count, 90, 0);
        }
        if (rotation_change == true)
        {
            StartCoroutine(ChangeTheWei());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStage == true)
        {
            transform.Rotate(0, 0, rotation_speed * Time.deltaTime);
        }
        
    }
    IEnumerator ChangeTheWei()
    {
        yield return new WaitForSeconds(change_time);
        rotation_speed = speed2;
        yield return new WaitForSeconds(change_time);
        rotation_speed = speed1;
        StartCoroutine(ChangeTheWei());
    }
}
