using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_control : MonoBehaviour
{
    public float moveSpeed = 0.005f;
    public Rigidbody2D rb;
    private float HorizontalMove;
    private float VerticalMove;
    private Animator myAnim;
    private bool get_key = false;
    public GameObject key;
    public GameObject door;
    public float noteOffset = 1f;

    public GameObject npc1Note1;
    public GameObject npc1Note2;
    public GameObject npc2Note1;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        npc1Note1.SetActive(false);
        npc1Note2.SetActive(false);
        npc2Note1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        if (Input.GetKey(KeyCode.W))
        {
            myAnim.SetBool("moving", true);
            myAnim.SetInteger("direction", 2);
            position.y += moveSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            myAnim.SetBool("moving", true);
            myAnim.SetInteger("direction", 1);
            position.y -= moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            myAnim.SetBool("moving", true);
            myAnim.SetInteger("direction", 4);
            position.x -= moveSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            myAnim.SetBool("moving", true);
            myAnim.SetInteger("direction", 3);
            position.x += moveSpeed;
        }
        else
        {
            myAnim.SetBool("moving", false);
        }

        position.z = position.y;

        transform.position = position;
    }

    void FixedUpdate()
    {


    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name.Equals("key"))
        {
            get_key = true;
            Destroy(key);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if ((collision.gameObject.name.Equals("npc2")) || collision.gameObject.Equals(npc1Note2) || collision.gameObject.Equals(npc1Note1))
            {
                if (!get_key)
                {
                    npc1Note2.SetActive(false);
                    npc1Note1.SetActive(true);

                    npc1Note1.transform.position = transform.position + new Vector3(0f, noteOffset, 0f);
                }
                else
                {

                    npc1Note1.SetActive(false);
                    npc1Note2.SetActive(true);

                    npc1Note2.transform.position = transform.position + new Vector3(0f, noteOffset, 0f);
                }

            }
            else if (collision.gameObject.name.Equals("npc"))
            {


                npc2Note1.SetActive(true);

                npc2Note1.transform.position = transform.position + new Vector3(0f, noteOffset, 0f);


            }
        }


    }



    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name.Equals("door") && get_key)
        {
            Destroy(door);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Finish"))
        {

            SceneManager.LoadScene("Start");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(npc2Note1))
        {
            npc2Note1.SetActive(false);
        }
        else if (collision.gameObject.Equals(npc1Note1))
        {
            npc1Note1.SetActive(false);
        }
        else if (collision.gameObject.Equals(npc1Note2))
        {
            npc1Note2.SetActive(false);
        }
    }
}

