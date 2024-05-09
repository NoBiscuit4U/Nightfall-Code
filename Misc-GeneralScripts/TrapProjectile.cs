using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrapProjectile : MonoBehaviour
{
    public static TrapProjectile instance;
    
    public Transform startTransform;
    public Transform startSelf;
    public Transform self;
    public Transform targetP;
    
    public GameObject proj;

    public int damageToDealP;
    public float moveSpeedP;

    void Start()
    {
        instance = this;
        startTransform = ProjectileTrap.instance.transform;
        startSelf.position = startTransform.position;
        damageToDealP = ProjectileTrap.instance.damageToDeal;
        moveSpeedP = ProjectileTrap.instance.moveSpeed;
    }

    void Update()
    {
        float speed = moveSpeedP * Time.deltaTime;
        targetP = ProjectileTrap.instance.target;
        transform.position =Vector3.MoveTowards(self.position,targetP.position,speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager.instance.gotHitByProj = true;
            Destroy(proj);
        }

        Destroy(proj);
    }
}
