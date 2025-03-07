using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AiScript : MonoBehaviour
{
    
    public NavMeshAgent agent;
    public Transform player;
    public GameObject pMushroom;
    public Transform mushroom;

    public LayerMask whatIsGround, whatIsPlayer, whatIsMushroom;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    public int numAttacks;
    public int attackCycles;
    bool alreadyAttacked;

    public float sightRange, attackRange;
    public bool mushroomInSightRange, mushroomInAttackRange, playerInSightRange;

    private void Awake(){

        player = GameObject.Find("Player").transform;
        
        agent = GetComponent<NavMeshAgent>();
        
    }

    private void Update(){

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        mushroomInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsMushroom);
        mushroomInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsMushroom);

        if(!mushroomInSightRange && !mushroomInAttackRange) Patroling();
        if(mushroomInSightRange && !mushroomInAttackRange){

            pMushroom = GameObject.FindWithTag("Mushroom");
            mushroom = pMushroom.transform;

            goingToMushroom();

        }
        if(mushroomInAttackRange && mushroomInSightRange) AttackMushroom();
        if(playerInSightRange) Retreat();

    }

    private void Patroling(){

        if(!walkPointSet) SearchWalkPoint();

        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }

    }

    private void SearchWalkPoint(){

       float randomZ = Random.Range(-walkPointRange, walkPointRange); 
       float randomX = Random.Range(-walkPointRange, walkPointRange);

       walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

       if(Physics.Raycast(walkPoint, -transform.up, 10f, whatIsGround)){

        walkPointSet = true;

       }

    }

    private void goingToMushroom(){

        Debug.Log("Going to mushroom");
        agent.SetDestination(mushroom.position);

    }


    private void AttackMushroom(){

        agent.SetDestination(transform.position);
        transform.LookAt(mushroom);

        if(!alreadyAttacked){

            // Additional attack code here
            Debug.Log("Attacking mushroom");
            numAttacks++;

            if(numAttacks == attackCycles){

                numAttacks = 0;
                pMushroom.SetActive(false);
                mushroom = null;
                pMushroom = null;
                return;

            }

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);

        }

    }

    private void Retreat(){

        Debug.Log("Retreating");
        agent.SetDestination(-player.position);

    }

    // DESTROY MUSHROOM
    // GET ATTACKED AND SHOOED AWAY

    private void ResetAttack(){

        alreadyAttacked =false;

    }

    private void OnDrawGizmosSelected(){

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }


}
