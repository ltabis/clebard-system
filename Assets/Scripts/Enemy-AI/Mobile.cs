using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mobile : Entity
{
    /*public Material charmedMaterial;
   public Material aggressiveMaterial;
   public Material initialMaterial;
   public Material scaredMaterial;*/

    protected NavMeshAgent agent;
    protected Animator anim;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    protected Vector3 walkPoint;
    protected bool walkPointSet;
    public float walkPointRange;

    public float sightRange;
    protected bool playerInSightRange;

    protected bool isCharmed;
    protected float timeCharmed;
    protected bool isCharmedMoving = false;

    protected bool isScared;
    protected float timeScared;

    protected float moveTimer = 5f;
    protected float noStuck = 4f;

    public float walkSpeed = 3.5f;
    public float trotSpeed = 5f;
    public float runSpeed = 8f;

    //public ParticleSystem charmedParticle;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (isScared)
            Scared();
        else if (isCharmed)
            Charmed();
        else if (!playerInSightRange)
            Patroling();
        else if (playerInSightRange)
            ChasePlayer();
    }

    protected void Charmed()
    {
        Trot();
        //CharmParticle();
        walkPointSet = false;
        if (timeCharmed > 0)
            timeCharmed -= Time.deltaTime;
        if (timeCharmed <= 0)
            isCharmed = false;
        if (!isCharmedMoving)
            agent.SetDestination(player.position);
        if (Input.GetMouseButtonDown(0))
        {
            Run();
            isCharmedMoving = true;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }
    }

    protected void Scared()
    {
        walkPointSet = false;
        if (timeScared > 0)
            timeScared -= Time.deltaTime;
        if (timeScared <= 0)
        {
            isScared = false;
            return;
        }
        Flee();
    }

    protected virtual void Patroling()
    {
        moveTimer -= Time.deltaTime;
        noStuck -= Time.deltaTime;
        if (!walkPointSet || noStuck <= 0f)
            SearchWalkPoint();

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpooint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            if (!RandomBehaviour())
                Idle();
            agent.velocity = Vector3.zero;
        }
        if (moveTimer <= 0)
        {

            if (walkPointSet)
            {
                agent.SetDestination(walkPoint);
                Walk();
            }
            moveTimer = Random.Range(4f, 6f);
        }
    }

    protected void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
            noStuck = 8f;
        }
    }

    protected void ChasePlayer()
    {
        Trot();
        walkPointSet = false;
        agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    protected void Flee()
    {
        anim.Play("Run");
        Vector3 fleeDirection = transform.position - player.position;
        Vector3 newPos = transform.position + fleeDirection;
        agent.SetDestination(newPos);
    }

    protected bool RandomBehaviour()
    {
        if (Random.Range(0, 6) == 3)
        {
            Sit();
            return true;
        }
        return false;
    }

    public void SetIsScared(bool scare)
    {
        isScared = scare;
    }

    public void SetTimeScared(float scaretime)
    {
        timeScared = scaretime;
    }

    public void SetIsCharmed(bool charm)
    {
        isCharmed = charm;
    }

    public void SetTimeCharmed(float charmedtime)
    {
        timeCharmed = charmedtime;
    }

    protected void Idle()
    {
        anim.Play("Idle");
        //anim.CrossFade("Idle", 0.25f);
    }

    protected void Walk()
    {
        anim.Play("Walk");
        //anim.Play("Walk");
        agent.speed = walkSpeed;
    }

    protected virtual void Trot()
    {
        anim.Play("Trot");
        agent.speed = trotSpeed;
    }

    protected void Run()
    {
        //anim.SetFloat("Run", 4);
        //anim.SetTrigger("Run");
        anim.Play("Run");
        //anim.Play("Run");
        agent.speed = runSpeed;
    }

    protected void Sit()
    {
        //anim.CrossFade("SitIdle", 0.25f);
    }

    protected void CharmParticle()
    {
      //  charmedParticle.Play();
    }
}
