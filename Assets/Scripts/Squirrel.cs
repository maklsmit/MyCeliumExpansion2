using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Squirrel : MonoBehaviour
{
    public AudioSource runSound;
    public AudioSource digSound;

    private Animator anim;

    private SquirrelAnim animState = SquirrelAnim.IDLE;

    private GameObject targetMushroom;

    public float height = 2f;

    public float movementSpeed = 1f;

    public float digTime = 10f;

    public float range = 10f;
    public float digRange = 0.5f;

    private Rigidbody rb;

    private MushroomRoot root;

    private bool shooing = false;

    private Vector3 lastVelocity = Vector3.zero;

    void Start()
    {
        this.anim = GetComponent<Animator>();
        this.rb = GetComponent<Rigidbody>();
        root = GameObject.FindGameObjectWithTag("MushroomRoot").GetComponent<MushroomRoot>();
    }

    void Update()
    {
        if(!shooing)
        {
            if(targetMushroom == null)
            {
                Debug.Log("No target mushroom, finding one");
                GameObject[] mushrooms = GameObject.FindGameObjectsWithTag("Mushroom");

                List<GameObject> activeMushrooms = new List<GameObject>(mushrooms.Length);
                foreach(GameObject goj in mushrooms)
                {
                    if(goj.activeInHierarchy && goj.name != "FirstMushroom")
                    {
                        activeMushrooms.Add(goj);
                    }
                }

                if(activeMushrooms.Count > 2)
                {
                    Debug.Log("At least 2 potential targets");

                    activeMushrooms.Sort((m1, m2) => {
                        return (transform.position - m1.transform.position).sqrMagnitude > (transform.position - m2.transform.position).sqrMagnitude ? 1 : -1;
                    });

                    float distance = (activeMushrooms[0].transform.position - transform.position).magnitude;

                    Debug.Log($"Closest Distance is {distance}");

                    if(distance <= range)
                    {
                        targetMushroom = activeMushrooms[0];

                        // set velocity to target
                        Vector3 velocityToTarget = (targetMushroom.transform.position - transform.position).normalized * movementSpeed;
                        Debug.Log($"going to target in {velocityToTarget} direction");

                        // set animation based on velocity
                        if(velocityToTarget.x > 0)
                        {
                            anim.SetTrigger("RunRight");
                            animState = SquirrelAnim.RUN_RIGHT;
                            runSound.Play();
                            digSound.Stop();
                        }
                        else
                        {
                            anim.SetTrigger("RunLeft");
                            animState = SquirrelAnim.RUN_LEFT;
                            runSound.Play();
                            digSound.Stop();
                        }

                        rb.velocity = velocityToTarget;
                        lastVelocity = velocityToTarget;
                    }
                    else
                    {
                        if(animState != SquirrelAnim.IDLE)
                        {
                            Debug.Log("No target in range, idle");

                            rb.velocity = Vector3.zero;
                            anim.SetTrigger("Idle");
                            digSound.Stop();
                            runSound.Stop();
                            animState = SquirrelAnim.IDLE;
                        }
                    }
                }
            }
            else
            {
                Debug.Log($"Going to target {targetMushroom.transform.position}");

                float distance = (targetMushroom.transform.position - transform.position).magnitude;
                if(distance <= digRange)
                {
                    Debug.Log($"Digging target {targetMushroom.transform.position}");

                    if(animState != SquirrelAnim.DIG_LEFT && animState != SquirrelAnim.DIG_RIGHT)
                    {
                        StartCoroutine(DigMushroom());
                    }
                }
            }
        }
    }

    private IEnumerator DigMushroom()
    {
        rb.velocity = Vector3.zero;

        if(animState == SquirrelAnim.RUN_LEFT)
        {
            anim.SetTrigger("DigLeft");
            animState = SquirrelAnim.DIG_LEFT;
        }
        else
        {
            anim.SetTrigger("DigRight");
            animState = SquirrelAnim.DIG_RIGHT;
        }
        runSound.Stop();
        digSound.Play();
        Debug.Log($"Digging start {targetMushroom.transform.position}");


        yield return new WaitForSeconds(digTime);

        if(targetMushroom != null)
        {
            targetMushroom.SetActive(false);
            targetMushroom = null;
        }

        Debug.Log($"Digging end {targetMushroom?.transform?.position}");
        digSound.Stop();

        anim.SetTrigger("Idle");
        animState = SquirrelAnim.IDLE;
    }

    private void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + height;
        transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Broom") && shooing == false)
        {
            Debug.Log("Encountered Broom!");
            shooing = true;
            targetMushroom = null;
            rb.velocity = lastVelocity * -1.25f;
            if(rb.velocity.x > 0)
            {
                anim.SetTrigger("RunRight");
                animState = SquirrelAnim.RUN_RIGHT;
            }
            else
            {
                anim.SetTrigger("RunLeft");
                animState = SquirrelAnim.RUN_LEFT;
            }
            digSound.Stop();
            runSound.Play();
            StartCoroutine(StopFleeing());
        }
    }

    private IEnumerator StopFleeing()
    {
        yield return new WaitForSeconds(7.5f);
        shooing = false;
        rb.velocity = Vector3.zero;
        anim.SetTrigger("Idle");
        animState = SquirrelAnim.IDLE;
        runSound.Stop();
    }
}

public enum SquirrelAnim
{
    IDLE,
    RUN_LEFT,
    RUN_RIGHT,
    DIG_LEFT,
    DIG_RIGHT
}
