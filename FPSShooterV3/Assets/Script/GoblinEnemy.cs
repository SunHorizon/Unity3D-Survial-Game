using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinEnemy : MonoBehaviour {


    NavMeshAgent nm;
    Animator anim;
    GameObject target;

    Transform Player;
    Transform Player1;

    float DeadTimer;

    float AttackTime;
    float time;
    
    bool Dead;
    Vector3 pos;
    float doDamage;
    public float health;

    public RectTransform heathBar;
    float healthScale;

    int check;

    public GameObject arrow;
    public Transform arrowSpawn;


    int DeadCounter;
    public AudioSource aSource;
    public AudioClip attackSnd;
    public AudioClip RunningStnd;
    public AudioClip DeadStd;


    // Use this for initialization
    void Start () {

        check = 0;
        if (!nm)
        {
            nm = GetComponent<NavMeshAgent>();

        }
        if (!heathBar)
        {
            Debug.Log("No heathBar found.");

        }
        if (DeadTimer == 0)
        {
            DeadTimer = 5;

        }

        if (doDamage <= 0)
        {
            doDamage = 10;

        }
        if (AttackTime <= 0)
        {
            AttackTime = 1.20f;

        }
        if (time <= 0)
        {
            time = 1.20f;

        }
        if (health <= 0)
        {
            health = 40;

        }

        if (!anim)
        {
            anim = GetComponent<Animator>();

        }

        //Player = transform.Find("Player");

        Player = GameObject.FindGameObjectWithTag("Player").transform;
        if (GameManager.player == true)
        {
            Player1 = GameObject.FindGameObjectWithTag("Player1").transform;
        }
        healthScale = heathBar.sizeDelta.x / health;
        //nm.SetDestination(Player.transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.player == false)
        {
            if (!Character.DeadCheck && !Character.FinishedCheck && !OptionManager.optionManager && !PauseManager.PausedCheck)
            {
                transform.LookAt(Player);
                if (!Dead && Vector3.Distance(Player.position, this.transform.position) < 40)
                {

                    if (Vector3.Distance(Player.position, this.transform.position) > nm.stoppingDistance + 0.9)
                    {
                        //nm.isStopped = false;             
                        nm.SetDestination(Player.transform.position);
                        anim.SetBool("IsWalking", true);
                        anim.SetBool("IsAttacking", false);
                        AttackTime = time;
                        if (aSource != null)
                        {
                            if (aSource.isPlaying == false)
                            {
                                playSingleleSound(RunningStnd);
                                aSource.loop = true;
                            }
                        }
                    }
                    else
                    {
                        anim.SetBool("IsAttacking", true);
                        anim.SetBool("IsWalking", false);
                        AttackTime -= Time.fixedDeltaTime;
                        if (AttackTime <= 0)
                        {
                            AttackPlayer();
                            AttackTime = time;
                            playSingleleSound(attackSnd);
                            aSource.loop = false;
                            anim.SetBool("IsAttacking", false);
                        }
                    }
                }
                else
                {
                    nm.SetDestination(transform.position);
                    if (aSource.clip != null)
                    {
                        if (aSource.clip.name == "WalkGround")
                        {
                            stopSound();
                        }
                    }
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsAttacking", false);
                }


                if (health <= 0)
                {
                    Die();
                }

                if (Character.playerhit == true)
                {
                    Character.Health -= doDamage;
                    Character.playerhit = false;
                }
            }
            else
            {
                aSource.Stop();
            }

        }
        else
        {
            if (((!Character1.DeadCheck && !Character1.FinishedCheck) && (!Character.DeadCheck && !Character.FinishedCheck)) && !OptionManager.optionManager && !PauseManager.PausedCheck)
            {
                if (!Dead && (Vector3.Distance(Player.position, this.transform.position) < 40 || Vector3.Distance(Player1.position, this.transform.position) < 40))
                {
                    transform.LookAt(Player1);
                    if (Vector3.Distance(Player.position, this.transform.position) > Vector3.Distance(Player1.position, this.transform.position))
                    {
                        if (Vector3.Distance(Player1.position, this.transform.position) > nm.stoppingDistance + 0.9)
                        {
                            //nm.isStopped = false;             
                            nm.SetDestination(Player1.transform.position);
                            anim.SetBool("IsWalking", true);
                            anim.SetBool("IsAttacking", false);
                            AttackTime = time;
                            if (aSource != null)
                            {
                                if (aSource.isPlaying == false)
                                {
                                    playSingleleSound(RunningStnd);
                                    aSource.loop = true;
                                }
                            }
                        }
                        else
                        {
                            anim.SetBool("IsAttacking", true);
                            anim.SetBool("IsWalking", false);
                            AttackTime -= Time.fixedDeltaTime;
                            if (AttackTime <= 0)
                            {
                                AttackPlayer2();
                                AttackTime = time;
                                playSingleleSound(attackSnd);
                                aSource.loop = false;
                                anim.SetBool("IsAttacking", false);
                            }
                        }
                    }
                    else
                    {
                        transform.LookAt(Player);
                        if (Vector3.Distance(Player.position, this.transform.position) > nm.stoppingDistance + 0.9)
                        {
                            //nm.isStopped = false;             
                            nm.SetDestination(Player.transform.position);
                            anim.SetBool("IsWalking", true);
                            anim.SetBool("IsAttacking", false);
                            AttackTime = time;
                            if (aSource != null)
                            {
                                if (aSource.isPlaying == false)
                                {
                                    playSingleleSound(RunningStnd);
                                    aSource.loop = true;
                                }
                            }
                        }
                        else
                        {
                            anim.SetBool("IsAttacking", true);
                            anim.SetBool("IsWalking", false);
                            AttackTime -= Time.fixedDeltaTime;
                            if (AttackTime <= 0)
                            {
                                AttackPlayer();
                                AttackTime = time;
                                playSingleleSound(attackSnd);
                                aSource.loop = false;
                                anim.SetBool("IsAttacking", false);
                            }
                        }
                    }         
                }
                else
                {
                    nm.SetDestination(transform.position);
                    if (aSource.clip != null)
                    {
                        if (aSource.clip.name == "WalkGround")
                        {
                            stopSound();
                        }
                    }
                    anim.SetBool("IsWalking", false);
                    anim.SetBool("IsAttacking", false);
                }

                if (health <= 0)
                {
                    Die();
                }

                if (Character.playerhit == true)
                {
                    Character.Health -= doDamage;
                    Character.playerhit = false;
                }
                if (Character1.playerhit == true)
                {
                    Character1.Health -= doDamage;
                    Character1.playerhit = false;
                }
            }
            else
            {
                aSource.Stop();
            }

        }
    }

    public void AttackPlayer()
    {
        if (!Dead && Character.DeadCheck == false)
        {
            Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        }
    }
    public void AttackPlayer2()
    {
        if (!Dead && Character1.DeadCheck == false)
        {
            Instantiate(arrow, arrowSpawn.position, arrowSpawn.rotation);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        heathBar.sizeDelta = new Vector3(health * healthScale, heathBar.sizeDelta.y);
    }

    void Die()
    {
        DeadCounter++;
        if (DeadCounter == 1)
        {
            playSingleleSound(DeadStd);
            aSource.loop = false;
        }
        nm.SetDestination(transform.position);
        anim.SetBool("Dead", true);
        Dead = true;
        DeadTimer -= Time.fixedDeltaTime;
        if (DeadTimer < 0)
        {
            Destroy(gameObject);
        }
    }

    public void playSingleleSound(AudioClip clip)
    {
        aSource.clip = clip;
        aSource.Play();
    }

    public void stopSound()
    {
        aSource.Stop();
    }
}
