using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : Collectable
{   
    public Sprite emptyChest;
    public int glimsAmount = 5;
    protected override void OnCollect()
    {   
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.glims += glimsAmount;
            GameManager.instance.ShowText("+" + glimsAmount + " Glims", 20, Color.yellow, transform.position, Vector3.up * 20, 0.7f);
            Debug.Log("Gain " + glimsAmount + " Glims!");
        }
    
    }
}
