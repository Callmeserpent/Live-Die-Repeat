using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Fighter
{
   protected override void Death()
   {
        Destroy(gameObject);
   }
}
