using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : Collidable
{
   protected override void OnCollide(Collider2D coll)
{   
    if (coll.CompareTag("Player"))
    {
        Player player = coll.GetComponent<Player>();
        
        if (player != null && player.isAlive)  // Only call Trap() if the player is alive
        {
            player.Trap();
        }
    }
}
}
