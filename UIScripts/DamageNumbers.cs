﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageNumbers : MonoBehaviour{
    
    public static DamageNumbers instance;

    public Text damageText;

    public float lifeTime = 1f;
    public float movespeed = 1f;

    public float placementJitter = .5f;

    // Start is called before the first frame update
    void Start(){
        instance = this;
    }

    // Update is called once per frame
    void Update(){
        Destroy(gameObject, lifeTime);
        transform.position += new Vector3(0f, movespeed * Time.deltaTime, 0f);
    }

    public void SetDamage(int damageAmount)
    {
        damageText.text = damageAmount.ToString();
        transform.position += new Vector3(Random.Range(-placementJitter, placementJitter), Random.Range(-placementJitter, placementJitter), 0f);
    }
}
