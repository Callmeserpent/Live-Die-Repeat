using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float WindballSpeed = 2.5f;
    public float FireballSpeed = 2f;
    public float WaterballSpeed = 1.5f;
    public float EarthballSpeed = 1f;
    
    public float WindballDistance = 0.75f;
    public float FireballDistance = 0.5f;
    public float WaterballDistance = 0.4f;
    public float EarthballDistance = 0.3f;

    public Transform Windball;
    public Transform Fireball;
    public Transform Waterball;
    public Transform Earthball;

    private void Update() {
        Windball.position = transform.position + new Vector3(-Mathf.Cos(Time.time * WindballSpeed) * WindballDistance, Mathf.Sin(Time.time * WindballSpeed) * WindballDistance, 0);
        Fireball.position = transform.position + new Vector3(-Mathf.Cos(Time.time * FireballSpeed) * FireballDistance, Mathf.Sin(Time.time * FireballSpeed) * FireballDistance, 0);
        Waterball.position = transform.position + new Vector3(-Mathf.Cos(Time.time * WaterballSpeed) * WaterballDistance, Mathf.Sin(Time.time * WaterballSpeed) * WaterballDistance, 0);
        Earthball.position = transform.position + new Vector3(-Mathf.Cos(Time.time * EarthballSpeed) * EarthballDistance, Mathf.Sin(Time.time * EarthballSpeed) * EarthballDistance, 0);
   
    }
}