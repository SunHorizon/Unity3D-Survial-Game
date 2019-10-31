using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class Character : MonoBehaviour
{

    CharacterController player;
    public float speed, roatationSpeed;
    public Camera cha;
    public float MoveV, MoveH, verVel, rotX, rotY, jumpspeed;
    public float jumpForce;
    Vector3 MoveDirection;
    bool hasJumped, isCrouch;
    Animator anim;
    public GameObject Guns;
    Animator otherAnimator;


    bool water;
    bool Lava;
    public static float Health;
    int intHealth;

    public static bool FinishedCheck;
    public static bool DeadCheck;

    public Text HealthText;
    public static bool SafeZone;

    public static bool playerhit;
    public static bool BompHit;
    public static bool GameKey;

    // Audio Stuff
    public AudioSource aSource;
    public AudioClip LevelMusic;
    public AudioClip walkSnd;
    public AudioClip walkWaterSnd;
    public AudioClip hurtSnd;
    public AudioClip JumpSnd;

    public Transform PlayerOneSpawn;
    float jumpTimer;
    int walkcounter;
    public static int playerKills;

    static public bool takingDamage;

    // Use this for initialization
    void Start()
    {
        AudioManager.instance.PlayMusic(LevelMusic, 0.3f);
        AudioManager.instance.sfxMusic.loop = true;

        SafeZone = false;
        playerhit = false;
        BompHit = false;
        Health = 100.0f;
        player = GetComponent<CharacterController>();

        if (speed <= 0)
        {
            speed = 6.0f;
            Debug.Log("Speed not set on " + name + "Defaulting to " + speed);
        }
   
        if (Health <= 0)
        {
            Health = 100.0f;
            intHealth = (int) Health;
            HealthText.text = intHealth.ToString();
            Debug.Log("Health not set on " + name + "Defaulting to " + Health);
        }
  
        if (roatationSpeed <= 0)
        {
            roatationSpeed = 3.0f;
            Debug.Log("roatationSpeed not set on " + name + "Defaulting to " + roatationSpeed);
        }

        if (jumpspeed <= 0)
        {
            jumpspeed = 4f;
            Debug.Log("jumpspeed not set on " + name + "Defaulting to " + jumpspeed);
        }

        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No Animator Found on " + name);
        }

        otherAnimator = Guns.GetComponent<Animator>();
        if (!otherAnimator)
        {
            Debug.Log("No Animator Found on " + name);
        }
    }

    // Update is called once per frame
    void Update()
    {     
        CheckLava();
        intHealth = (int)Health;
        Movement();
        if (Input.GetButtonDown("Jump"))
        {
            hasJumped = true;
            //playSingleleSound(JumpSnd);
            stopSound();
        }

        if (Input.GetButtonDown("Crouch") && !water && !Lava)
        {
            if (isCrouch == false)
            {
                player.height = player.height / 2;
                speed /= 2;
                isCrouch = true;
                Debug.Log("Crouch true");
            }
            else
            {
                player.height = player.height * 2;
                speed = 6;
                isCrouch = false;
            }

        }

        ApplyGrivity();


        if (Input.GetKeyDown(KeyCode.F))
        {
            FinishedCheck = true;
            GameKey = true;
        }

        if(CheckKills.playerkills1 <= 5)
        {
            if (Health <= 0)
            {
                Instantiate(this, PlayerOneSpawn.position, PlayerOneSpawn.rotation);
                CheckKills.playerkills2++;
                Destroy(gameObject);
            }
        }
      
        if (takingDamage == true)
        {
            aSource.volume = 0.5f;
            playSingleleSound(hurtSnd);
            aSource.loop = false;
            takingDamage = false;
           
        }
        if (aSource.clip != null)
        {
            if (aSource.clip.name == "HurtSound")
            {
                if (aSource.isPlaying == false)
                {
                    
                    walkcounter = 0;
                }
            }
        }
        HealthText.text = intHealth.ToString() +"%";
   
    }

    void Movement()
    {

        // moving using WASD
        MoveV = Input.GetAxis("Vertical") * speed;
        MoveH = Input.GetAxis("Horizontal") * speed;

        // moving using mouse
        rotX = Input.GetAxis("Mouse X") * roatationSpeed;
        rotY -= Input.GetAxis("Mouse Y") * roatationSpeed;
        rotY = Mathf.Clamp(rotY, -53.0f, 48.0f);

        MoveDirection = new Vector3(MoveH, verVel, MoveV);
   
        if (Time.deltaTime == 0)
        {
            transform.Rotate(0, 0, 0);
        }
        else
        {
            // rotating the player
            transform.Rotate(0, rotX, 0);
        }

        // rotating the player with the camera
        MoveDirection = transform.TransformDirection(MoveDirection);

        if (Time.deltaTime == 0)
        {
            cha.transform.localRotation = Quaternion.Euler(0, 0f, 0f);
        }
        else
        {
            // rotating the camrea
            cha.transform.localRotation = Quaternion.Euler(rotY, 0f, 0f);
        }

        // moving the player
        player.Move(MoveDirection * Time.deltaTime);

        //Setting the animation for running/walking
        otherAnimator.SetFloat("Running", MoveV);
        otherAnimator.SetFloat("Strafe", MoveH);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
            walkcounter++;
        }
        else
        {        
            if(aSource.clip != null)
            {
                if(aSource.clip.name == "WalkGround" || aSource.clip.name == "WalkWater")
                {
                    stopSound();
                }
            }
            walkcounter = 0;
        }
        if (walkcounter == 1)
        {
            if(water == true)
            {
                aSource.volume = 0.2f;
                playSingleleSound(walkWaterSnd);
                aSource.loop = true;
            }
            else
            {
                aSource.volume = 0.1f;
                playSingleleSound(walkSnd);
                aSource.loop = true;
            }
        
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

    private void OnTriggerEnter(Collider c)
    {
        // Check if player is in water
        if (c.gameObject.tag == "Water")
        {
            water = true;
            CheckWater();
        }
        // Check if player not it water
        if (c.gameObject.tag == "NoWater")
        {
            water = false;
            CheckWater();
        }
        if (c.gameObject.tag == "Lava")
        {
            Lava = true;

        }
        if (c.gameObject.tag == "NoLava")
        {
            Lava = false;
        }
        if (c.gameObject.name == "Apple(Clone)")
        {
            Health = 100;
            Debug.Log("Health Fully Restored");
            Destroy(c.gameObject);
        }

        if (c.gameObject.name == "TV(Clone)")
        {
            Gun.Damage *= 2;
            Debug.Log("Double Damage Active ");
            StartCoroutine(stopDoubleDamage());
            Destroy(c.gameObject);
        }
        if (c.gameObject.name == "Bullet(Clone)")
        {
            Gun.TotalAmmo += 20;
            Debug.Log("Added Ammo");
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "Arrow")
        {
            playerhit = true;
            takingDamage = true;
            Destroy(c.gameObject);
        }
        
        if (c.gameObject.tag == "Mine") 
        {
            BompHit = true;
            Health -= 5;
        }
        if (c.gameObject.tag == "Key")
        {         
            GameKey = true;
            Destroy(c.gameObject);
        }
    }

    // checking if player is standing on lava
    void CheckLava()
    {
        if (Lava == true && DeadCheck == false)
        {
            Health -= 0.10f;
           
        }
    }

    // checking if the player is in water
    void CheckWater()
    {
        if (water == true)
        {
            walkcounter = 0;
            speed = 3;
        }
        if (water == false)
        {
            walkcounter = 0;
            speed = 6;
        }
    }

    // applying grivity
    void ApplyGrivity()
    {

        if (player.isGrounded == true)
        {
            if (hasJumped == false)
            {
                verVel = Physics.gravity.y;
            }
            else
            {
                verVel = jumpspeed;
            }
            if(BompHit == true)
            {
                verVel = jumpspeed + 10;
            }
        }
        else
        {
            verVel += Physics.gravity.y * Time.deltaTime;
            verVel = Mathf.Clamp(verVel, -50f,  jumpspeed);
            hasJumped = false;
            BompHit = false;
            walkcounter = 0;
        }
    }

    public void TakeDamage(float amount)
    {
        Health -= amount;    
    }

    IEnumerator stopDoubleDamage()

    {
        yield return new WaitForSeconds(5);
        Gun.Damage /= 2;
        Debug.Log("Double Damage Deactivate");

    }

}

