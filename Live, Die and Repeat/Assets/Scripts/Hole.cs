using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : Collidable
{
    protected override void OnCollide(Collider2D coll)
    {   
        if (coll.name != "Player")
            return;
            
        GameManager.instance.player.Trap();
    }
}
