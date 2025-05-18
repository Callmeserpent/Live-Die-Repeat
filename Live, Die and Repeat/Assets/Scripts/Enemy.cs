using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{   
    AudioManager audioManager;

    //Experience
    public int expValue = 1;

    //Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Chase
    private Animator anim;

    //Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {   
        //Check player in range
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
                chasing = true;

                if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength && 
                    (this.name == "TreasureTrap" || this.name == "Slime" || this.name == "Slug" || this.name == "ZombieElite")){
                    chasing = true;
                    Chase();
                }

            if (chasing)
            {
                if (!collidingWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);       
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }

        //Check for overlaps
        collidingWithPlayer = false;
        boxCollider.OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player" )
            {
                collidingWithPlayer = true;
            }

            //Clean up the array
            hits[i] = null;
        }
    }

    protected override void  Death()
    {
        Destroy(gameObject);
        GameManager.instance.GainExp(expValue);
        GameManager.instance.ShowText("+" + expValue + " exp", 20, Color.cyan, transform.position, Vector3.up *20, 0.7f);
        Debug.Log(gameObject + "has been eliminated!");
        audioManager.PlaySFX(audioManager.die);
    }

    private void Chase()
   {    
        anim.SetTrigger("chasing");
   }
}
