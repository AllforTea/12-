using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShot : MonoBehaviour
{
    BallClicker ballclicker;
    GameController gamecontroller;
    Rigidbody rb;
    [SerializeField]
    float speed;
    public bool shooted = false;
    
    public Vector3 reload_position;
    [SerializeField]
    float reload_speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ballclicker = GameObject.FindGameObjectWithTag("BallClicker").GetComponent<BallClicker>();
        gamecontroller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        reload_position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shooted == true)
        {
            rb.MovePosition(rb.position + Vector3.up * speed * Time.deltaTime);
        }else if (transform.position!=reload_position)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.position = Vector3.MoveTowards(transform.position, reload_position, reload_speed * Time.deltaTime);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        string col_tag = collision.gameObject.tag;
        MakeBall_Untachable();
        if (gameObject.tag == "Rainbow")
        {
            if (col_tag == "Rotater")
            {
                BoxHit(false,collision.gameObject);
            }
            else
            {
                //Doðru kutu
                BoxHit(true,collision.gameObject);
            }

        }
        else
        {
            if (collision.gameObject.CompareTag(gameObject.tag))
            {
                BoxHit(true,collision.gameObject);
            }
            else
            {
                BoxHit(false,collision.gameObject);
            }
        }
    }

    void BoxHit(bool truehit,GameObject collision)
    {
        if (truehit == true)
        {
            GameObject particle = collision.transform.parent.GetChild(2).gameObject;
            particle.transform.parent = transform.parent;
            particle.SetActive(true);
            collision.transform.parent.gameObject.SetActive(false);
            gamecontroller.HitBox_AfterMath(true);
            this.gameObject.SetActive(false);

        }
        else
        {
            gamecontroller.HitBox_AfterMath(false);
        }
    }

    void MakeBall_Untachable()
    {
        speed = 0;
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        rb.isKinematic = false;
    }
}
