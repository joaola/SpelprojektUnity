using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{
	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public ItemType itemType;


	public enum ItemType{
		SkillItem,
		Consumable,
		Accessory,
		Misc,
		KeyItem
	}

	//power används inte
	public Item(string name,int ID,string desc,int power,ItemType type){
		itemName = name;
		itemID = ID;
		itemDesc = desc;
		itemIcon = Resources.Load <Texture2D>("Item Icons/" + name);
		itemPower = power;
		itemType = type;
	}

	public Item(){
	
	}

}
