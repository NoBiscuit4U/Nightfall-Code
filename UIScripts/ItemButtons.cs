using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButtons : MonoBehaviour{
    
    public Image buttonImage;
    public Image rarityImage;
    public Image commonImage, uncommonImage, rareImage, epicImage, legendaryImage;
    public Text amountText;
    public int buttonValue;

    public bool Common, Uncommon, Rare, Epic, Legendary;

    public void Press()
    {
        if(GameMenu.instance.theMenu.activeInHierarchy || BattleManager.instance.battleScene)
        {
         if(GameManager.instance.itemsHeld[buttonValue] != "")
         {
            GameMenu.instance.SelectItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
         }
        }
    
    if(Shop.instance.shopMenu.activeInHierarchy)
    {
        if(Shop.instance.buyMenu.activeInHierarchy)
        {
            Shop.instance.SelectBuyItem(GameManager.instance.GetItemDetails(Shop.instance.itemsForSale[buttonValue]));
        }
   
        if(Shop.instance.sellMenu.activeInHierarchy)
        {
            Shop.instance.SelectSellItem(GameManager.instance.GetItemDetails(GameManager.instance.itemsHeld[buttonValue]));
        }
    }

    
    }

  public void Rarity()
  {
    Common = Item.instance.isCommon;
    Uncommon = Item.instance.isUncommon;
    Rare = Item.instance.isRare;
    Epic = Item.instance.isEpic;
    Legendary = Item.instance.isLegendary;

  }


}
  
