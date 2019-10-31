using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class Gun : MonoBehaviour {


    public static float Damage;
    public float range;

    public Camera fpsCam;
    public ParticleSystem MuzzleFlash;
    public GameObject ImplactEfect;

    static public float TotalAmmo;
    public float clipAmmo;
    public float ReloadTime;
    public bool isReloading;
    public float fullAmmo;
    public float currentReloadTime;

    public Text clipAmmpText;
    public Text TotalAmmoText;
    Animator anim;

    bool FireCheck;
    public float fireWaitTime;
    float shoot;


    // sound stuff
    public AudioSource aSource;
    public AudioClip shootSnd;
    public AudioClip reloadSnd;

    void Start () {

        FireCheck = true;
        isReloading = false;
        TotalAmmo = 0;
        if (TotalAmmo <= 0)
        {
            TotalAmmo = 90.0f;
            clipAmmo = 30;
            clipAmmpText.text = clipAmmo.ToString();
            TotalAmmoText.text = TotalAmmo.ToString();
            Debug.Log("Ammo not set on " + name + "Defaulting to " + TotalAmmo);
        }
        if (fullAmmo <= 0)
        {
            fullAmmo = 30.0f;
            Debug.Log("fullAmmo not set on " + name + "Defaulting to " + fullAmmo);
        }

        if (ReloadTime <= 0)
        {
            ReloadTime = 2.0f;
            Debug.Log("ReloadTime not set on " + name + "Defaulting to " + ReloadTime);
        }

        if (Damage <= 0)
        {
            Damage = 10;
        }
        if (fireWaitTime <= 0)
        {
            fireWaitTime = 0.3f;
            Debug.Log("fireWaitTime not set defaulting to " + fireWaitTime);
        }
        if (range <= 0)
        {
            range = 100;
        }


        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No Animator Found on " + name);
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.deltaTime == 0)
        {
            if (Input.GetButtonDown("Fire1") && FireCheck == true)
            {

            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && FireCheck == true && !isReloading && clipAmmo > 0)
            {
                Debug.Log("Checking Fire");
                Fire();
                clipAmmpText.text = clipAmmo.ToString();
                TotalAmmoText.text = TotalAmmo.ToString();
                StartCoroutine(waitfire());
            }
        }


        if (Input.GetKeyDown(KeyCode.R) && !isReloading && clipAmmo < 30 && TotalAmmo > 0)
        {
            currentReloadTime = ReloadTime;
            isReloading = true;
            Reload();
        }
        if (isReloading)
        {
            anim.SetBool("Reloading", true);
            currentReloadTime -= Time.fixedDeltaTime;
            if (currentReloadTime <= 0.0f)
            {
                anim.SetBool("Reloading", false);
                clipAmmpText.text = clipAmmo.ToString();
                TotalAmmoText.text = TotalAmmo.ToString();
                isReloading = false;
            }
        }

    }

    void Reload()
    {
        shoot = fullAmmo - clipAmmo;
        playSingleleSound(reloadSnd);
        if (TotalAmmo < shoot)
        {
            clipAmmo += TotalAmmo;
            TotalAmmo = 0;
            //fullAmmo = 0;
        }
        else
        {
            clipAmmo += shoot;
            TotalAmmo -= shoot;
        }

    }
    void Fire()
    {
        playSingleleSound(shootSnd);
        MuzzleFlash.Play();
        clipAmmo--;
        RaycastHit hit;
        
        if(Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Enemy enemy = hit.transform.GetComponent<Enemy>();

            GoblinEnemy GoblinEnemy = hit.transform.GetComponent<GoblinEnemy>();

            BompEnemy BompEnemy = hit.transform.GetComponent<BompEnemy>();

            BossEnemy BossEnemy = hit.transform.GetComponent<BossEnemy>();

            if(GameManager.player == true)
            {
                Character1 Player2 = hit.transform.GetComponent<Character1>();
                if (Player2 != null)
                {
                    Player2.TakeDamage(Damage);
                }
            }

          
            if (BossEnemy != null)
            {
                BossEnemy.TakeDamage(Damage);
            }

            if (BompEnemy != null)
            {
                BompEnemy.TakeDamage(Damage);
            }

            if (GoblinEnemy != null)
            {
                GoblinEnemy.TakeDamage(Damage);
            }

            if (enemy!= null)
            {
                enemy.TakeDamage(Damage);
            }

            GameObject ImplactGameObejct = Instantiate(ImplactEfect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(ImplactGameObejct, 0.5f);
        }
        anim.SetTrigger("Shoot");
    }


    IEnumerator waitfire()
    {

        FireCheck = false;
        yield return new WaitForSeconds(fireWaitTime);
        FireCheck = true;
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
