using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    [SerializeField] float lifeTime = 3;
    [SerializeField] float speed = 2;
    private float startTime;
    // Start is called before the first frame update
    void Awake(){
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - startTime >= lifeTime){
            Destroy(gameObject);
        }

        transform.position += transform.up * speed * -1 * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag.Equals("Squirrel")){
            Destroy(gameObject);
        }
    }
}
