using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplode : MonoBehaviour {

    private GameObject mine;
    public ParticleSystem Explosion;
    private float time;
    private bool CheckPlayer;

    public AudioSource aSource;
    public AudioClip Explo;


    // Use this for initialization
    void Start () {
        aSource.clip = Explo;
        CheckPlayer = false;
        time = 1.3f;
        Explosion.Stop();
    }

    void OnTriggerEnter(Collider c)
    {
         if(c.tag == "Player")
         {
            aSource.Play();
            Explosion.Play();          
            CheckPlayer = true;
        
         }   
    }
    

    // Update is called once per frame
    void Update () {

		if(CheckPlayer == true)
        {
            time -= Time.deltaTime;
            mine = GameObject.FindGameObjectWithTag("landMine");
            Destroy(mine);
        }
        if(time < 0)
        {
            Destroy(gameObject);         
        }
    
    }
}
