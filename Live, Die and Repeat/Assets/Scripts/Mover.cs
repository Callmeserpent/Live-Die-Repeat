using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.325f;
    public float xSpeed = 0.5f;

    protected virtual void Start(){
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {   
        //Reset MoveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //Swap sprite direction, whether you're going right or left
        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        //Add push vector, if any
        moveDelta += pushDirection;
        //Reduce push force every frame, based off recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed); 

        //Move in direction by casting box, if box null, free to move
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), 
        Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        
        if (hit.collider == null){
            //Moving
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), 
        Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        
        if (hit.collider == null){
            //Moving
            transform.Translate(moveDelta.x* Time.deltaTime, 0, 0);
        }
    }
}

