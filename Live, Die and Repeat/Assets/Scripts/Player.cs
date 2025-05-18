using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{   
    AudioManager audioManager;
    private SpriteRenderer spriteRenderer;
    public bool isAlive = true;
    private float walkSoundCooldown = 0.5f;  // Half a second between steps
    private float nextWalkSoundTime = 0f;


    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected override void Start()
    {   
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg)
    {   
        if (!isAlive)
            return;

        base.ReceiveDamage(dmg);
        GameManager.instance.OnHitpointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        if (isAlive){
            Vector3 movement = new Vector3(x, y, 0);  // Declare movement vector
            UpdateMotor(movement);
        
            if (movement != Vector3.zero && Time.time >= nextWalkSoundTime)
            {
            float walkPitch = 0.2f + Mathf.Abs(x + y) * 0.1f;  // Adjust speed based on movement intensity
            audioManager.PlaySFX(audioManager.walk, walkPitch);
            nextWalkSoundTime = Time.time + walkSoundCooldown;
            }
        }
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    public void OnLevelUp()
    {
        maxHitpoint++;
        hitpoint = maxHitpoint;
    }

    public void SetLevel(int level)
    {
        for (int i = 0; i < level; i++)
            OnLevelUp();
    }

    public void Heal(int healingAmount)
    {   
        if(hitpoint == maxHitpoint)
            return;

        hitpoint += healingAmount;
        if (hitpoint > maxHitpoint)
            hitpoint = maxHitpoint;
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up * 30, 0.5f);
        GameManager.instance.OnHitpointChange();
        audioManager.PlaySFX(audioManager.heal);
    }

    protected override void Death()
    {
        GameManager.instance.deathMenuAnim.SetTrigger("show");
        isAlive = false;
    }

    public void Respawn()
    {
        Heal(maxHitpoint);
        isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }

public void Trap()
{
    if (!isAlive) return;  // Prevent multiple triggers

    isAlive = false;
    Debug.Log("Player trapped, showing death menu.");
    GameManager.instance.deathMenuAnim.SetTrigger("show");
}

}