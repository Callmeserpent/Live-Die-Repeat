using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
   //Damage Struct
   public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20};
   public float[] pushForce = {2f, 2.1f, 2.2f, 2.3f, 2.4f, 2.5f, 2.6f, 2.7f, 2.8f, 2.9f, 3f, 3.1f, 3.2f, 3.3f, 3.4f, 3.5f, 3.6f, 3.8f, 3.9f, 4f};

   //Upgrade
   public int weaponLevel = 0;
   private SpriteRenderer spriteRenderer;

   //Swing
   public Animator anim;
   private float cooldown = 0.5f;
   public static Weapon instance;
   public bool isAttacking = false;
   private float lastSwing; 

   AudioManager audioManager;
   
   private void Awake()
{
    if (instance == null)
    {
        instance = this;  // Assign the weapon instance
    }
    else
    {
        Destroy(gameObject);  // Prevent duplicates
        return;
    }

    audioManager = GameObject.FindGameObjectWithTag("Audio")?.GetComponent<AudioManager>();
    spriteRenderer = GetComponent<SpriteRenderer>();
    anim = GetComponent<Animator>();

    if (anim == null)
    {
        Debug.LogError("Animator is missing from Weapon!");
    }
}


   protected override void Start()
   {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
   }

   protected override void Update()
   {
    base.Update();

    if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
    {    
          Swing();
          audioManager.PlaySFX(audioManager.slash);
    }
   }

   protected override void OnCollide(Collider2D coll)
   {    
        if (coll.tag == "Fighter")
        {   
            if (coll.name == "Player")
                return;
            
            //Create new damage obj then send to fighter we hit
            Damage dmg = new Damage()
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log("Attacking " + coll.name);     
        }
   }

private void Swing()
{
    if (Time.time - lastSwing > cooldown)
    {
        lastSwing = Time.time;
        isAttacking = true;
        anim.SetTrigger("Swing"); // Use SetTrigger instead of Play()

        StartCoroutine(ResetAttack());
    }
}

// Reset isAttacking after cooldown
IEnumerator ResetAttack()
{
    yield return new WaitForSeconds(cooldown);
    isAttacking = false;
    Debug.Log("Attack reset, ready to swing again.");
}


   public void UpgradeWeapon()
   {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //Change stats %%
   }

   public void SetWeaponLevel(int level)
   {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
   }
}
