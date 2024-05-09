using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{
    public bool hasBeenTriggered = false;

    public static ProjectileTrap instance;

    [Header("ProjectileStats")]
    public GameObject projectile;
    public Transform target;
    public int damageToDeal;
    public float moveSpeed;

    void Start()
    {
        instance = this;
        target = FindObjectOfType<PlayerController>().transform;
    }
    
    void Update() 
    {
        if(hasBeenTriggered)
        {
            Instantiate(projectile);
            hasBeenTriggered = false;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            hasBeenTriggered = true;
        }
    }
}
