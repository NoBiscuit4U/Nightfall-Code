﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour{
    
    private bool canOpen;

    public string[] ItemsForSale = new string[55];
    
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(canOpen && Input.GetButtonDown("Fire1") && PlayerController.instance.canMove)
        {
            Shop.instance.itemsForSale = ItemsForSale;
            
            Shop.instance.OpenShop();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canOpen = true;
            DialogManager.instance.LeftClickNotifier();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            canOpen = false;
            DialogManager.instance.LeftClickNotifierBlank();
        }
    }
}
