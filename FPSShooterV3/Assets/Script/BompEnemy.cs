using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BompEnemy : MonoBehaviour {


    NavMeshAgent nm;
    Animator anim;


    Transform Player;
    Transform Player1;

    float DeadTimer;

    float AttackTime;
    float time;

    bool Dead;
    float doDamage;
    public float health;

    public RectTransform heathBar;
    float healthScale;

    public ParticleSystem Explosion;

    bool destoryObjectCheck;
    public float destoryObjectTimer;
    int DamageCounter;


    public AudioSource aSource;
    public AudioClip attackSnd;
    public AudioClip RunningStnd;


    // Use this for initialization
    void Start () {

        destoryObjectCheck = false;
        Explosion.Stop();
        DamageCounter = 0;
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
        if (destoryObjectTimer <= 0)
        {
            destoryObjectTimer = 0.9f;

        }
        if (doDamage <= 0)
        {
            doDamage = 20;

        }
        if (AttackTime <= 0)
        {
            AttackTime = 1.20f;

        }
        if (time <= 0)
        {
            time = 0.3f;

        }
        if (health <= 0)
        {
            health = 20;

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
                if (Vector3.Distance(Player.position, this.transform.position) < 40)
                {

                    if (!Dead && Vector3.Distance(Player.position, this.transform.position) > nm.stoppingDistance)
                    {
                        nm.SetDestination(Player.transform.position);
                        anim.SetBool("IsRunning", true);
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
                        nm.SetDestination(transform.position);
                        anim.SetBool("IsRunning", false);
                        AttackTime -= Time.fixedDeltaTime;
                        Debug.Log(AttackTime);
                        if (AttackTime <= 0)
                        {
                            AttackPlayer();
                            AttackTime = time;
                        }
                    }
                }
                else
                {
                    nm.SetDestination(transform.position);
                    if (aSource.clip != null)
                    {
                        if (aSource.clip.name == "BombRunnung")
                        {
                            stopSound();
                        }
                    }
                    anim.SetBool("IsRunning", false);
                }
                if (destoryObjectCheck == true)
                {
                    destoryObjectTimer -= Time.fixedDeltaTime;
                }
                if (destoryObjectTimer <= 0)
                {
                    Destroy(gameObject);
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
            if (((!Character1.DeadCheck && !Character1.FinishedCheck) && (!Character.DeadCheck && !Character.FinishedCheck)) && !OptionManager.optionManager && !PauseManager.PausedCheck)
            {
                if (!Dead && (Vector3.Distance(Player.position, this.transform.position) < 40 || Vector3.Distance(Player1.position, this.transform.position) < 40))
                {
                    if (Vector3.Distance(Player.position, this.transform.position) > Vector3.Distance(Player1.position, this.transform.position))
                    {
                        if (Vector3.Distance(Player1.position, this.transform.position) > nm.stoppingDistance)
                        {
                            nm.SetDestination(Player1.transform.position);
                            anim.SetBool("IsRunning", true);
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
                            nm.SetDestination(transform.position);
                            anim.SetBool("IsRunning", false);
                            AttackTime -= Time.fixedDeltaTime;
                            Debug.Log(AttackTime);
                            if (AttackTime <= 0)
                            {
                                AttackPlayer2();
                                AttackTime = time;
                            }
                        }
                    }
                    else
                    {
                        if (!Dead && Vector3.Distance(Player.position, this.transform.position) > nm.stoppingDistance)
                        {
                            nm.SetDestination(Player.transform.position);
                            anim.SetBool("IsRunning", true);
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
                            nm.SetDestination(transform.position);
                            anim.SetBool("IsRunning", false);
                            AttackTime -= Time.fixedDeltaTime;
                            Debug.Log(AttackTime);
                            if (AttackTime <= 0)
                            {
                                AttackPlayer();
                                AttackTime = time;
                            }
                        }
                    }
            
                }
                else
                {
                    nm.SetDestination(transform.position);
                    if (aSource.clip != null)
                    {
                        if (aSource.clip.name == "BombRunnung")
                        {
                            stopSound();
                        }
                    }
                    anim.SetBool("IsRunning", false);
                }
                if (destoryObjectCheck == true)
                {
                    destoryObjectTimer -= Time.fixedDeltaTime;
                }
                if (destoryObjectTimer <= 0)
                {
                    Destroy(gameObject);
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
      
    }

    public void AttackPlayer()
    {
        Debug.Log("Attack");
        if (!Dead && Character.DeadCheck == false)
        {
            if(DamageCounter == 0)
            {
                Character.Health -= doDamage;
                nm.velocity = Vector3.zero;
                playSingleleSound(attackSnd);
                Explosion.Play();
                destoryObjectCheck = true;
            }          
            DamageCounter++;
        }
    }

    public void AttackPlayer2()
    {
        Debug.Log("Attack");
        if (!Dead && Character1.DeadCheck == false)
        {
            if (DamageCounter == 0)
            {
                Character1.Health -= doDamage;
                nm.velocity = Vector3.zero;
                playSingleleSound(attackSnd);
                Explosion.Play();
                destoryObjectCheck = true;
            }
            DamageCounter++;
        }
    }


    public void TakeDamage(float amount)
    {
        if(DamageCounter == 0)
        {
            health -= amount;
            heathBar.sizeDelta = new Vector3(health * healthScale, heathBar.sizeDelta.y);
        }
      
    }

    void Die()
    {
        stopSound();
        nm.SetDestination(transform.position);
        anim.SetBool("Dead", true);
        Dead = true;
        DeadTimer -= Time.fixedDeltaTime;
        //Debug.Log(DeadTimer);
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
