using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mobile : Entity
{

    protected NavMeshAgent agent;
    protected Animator anim;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

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

    //New AI
    protected float timer;
    public float wanderRadius = 12f;
    public float wanderTimer = 3f;

    private void Awake()
    {
        //player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        timer = wanderTimer;
    }

    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        if (isScared)
            Scared();
        else if (isCharmed)
            Charmed();
        else //if (!playerInSightRange)
            Patroling();
        //else if (playerInSightRange)
            //ChasePlayer();
    }

    protected void Charmed()
    {
        Trot();
        //CharmParticle();
        timer += wanderTimer;
        if (timeCharmed > 0)
            timeCharmed -= Time.deltaTime;
        if (timeCharmed <= 0)
            isCharmed = false;
        if (!isCharmedMoving)
            agent.SetDestination(player.position);
        print("player pos : " + player.position);
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
        timer += wanderTimer;
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
        timer += Time.deltaTime;

        if (timer >= wanderTimer)
        {
            Walk();
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            timer = 0;
        }
        else if (agent.remainingDistance <= 0)
            Idle();
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    protected void ChasePlayer()
    {
        Trot();
        timer += wanderTimer;
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
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", false);
        //anim.CrossFade("Idle", 0.25f);
    }

    protected void Walk()
    {
        anim.SetBool("isWalking", true);
        anim.SetBool("isRunning", false);
        agent.speed = walkSpeed;
    }

    protected virtual void Trot()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
        agent.speed = trotSpeed;
    }

    protected virtual void Run()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isRunning", true);
        agent.speed = runSpeed;
    }

    protected virtual void Sit()
    {
        //anim.CrossFade("SitIdle", 0.25f);
    }

    protected void CharmParticle()
    {
      //  charmedParticle.Play();
    }
}
