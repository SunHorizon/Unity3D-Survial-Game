﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossEnemy : MonoBehaviour {


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
    //public GameObject Apple;
    //public GameObject Tv;

    public RectTransform heathBar;
    float healthScale;


    int DeadCounter;
    public AudioSource aSource;
    public AudioClip attackSnd;
    public AudioClip RunningStnd;
    public AudioClip DeadStd;

    // Use this for initialization
    void Start () {

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
            doDamage = 35;

        }
        if (AttackTime <= 0)
        {
            AttackTime = 1.20f;

        }
        if (time <= 0)
        {
            time = 2.0f;

        }
        if (health <= 0)
        {
            health = 150;

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
        nm.SetDestination(Player.transform.position);
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.player == false)
        {
            if (!Character.DeadCheck && !Character.FinishedCheck && !OptionManager.optionManager && !PauseManager.PausedCheck)
            {
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
                            aSource.loop = false;
                            playSingleleSound(attackSnd);
                            Debug.Log(aSource);
                            Debug.Log(Character.DeadCheck);
                            AttackPlayer();
                            AttackTime = time;
                            anim.SetBool("IsAttacking", false);
                        }

                    }
                }
                else
                {
                    nm.SetDestination(transform.position);
                    if (aSource.clip != null)
                    {
                        if (aSource.clip.name == "BossWalking")
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
            }
            else
            {
                aSource.Stop();
            }
        }
        else
        {
            
            if (( (!Character1.DeadCheck && !Character1.FinishedCheck) && (!Character.DeadCheck && !Character.FinishedCheck)  ) && !OptionManager.optionManager && !PauseManager.PausedCheck)
            {
                Debug.Log("PLayer");
                if (!Dead && (Vector3.Distance(Player.position, this.transform.position) < 40 || Vector3.Distance(Player1.position, this.transform.position) < 40))
                {
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
                                aSource.loop = false;
                                playSingleleSound(attackSnd);
                                AttackPlayer2();
                                AttackTime = time;
                                anim.SetBool("IsAttacking", false);
                            }

                        }
                    }
                    else
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
                                aSource.loop = false;
                                playSingleleSound(attackSnd);
                                AttackPlayer();
                                AttackTime = time;
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
                        if (aSource.clip.name == "BossWalking")
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
            }
            else
            {
                Debug.Log("Stop Playing");
                aSource.Stop();
            }
        }

    }

    public void AttackPlayer()
    {
        Debug.Log("Attack");
        if (!Dead && Character.DeadCheck == false)
        {
            Character.Health -= doDamage;
        }
    }

    public void AttackPlayer2()
    {
        Debug.Log("Attack");
        if (!Dead && Character1.DeadCheck == false)
        {
            Character1.Health -= doDamage;
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
        anim.SetBool("Death", true);
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
