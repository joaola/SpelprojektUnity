using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemDatabase : MonoBehaviour {

	public List<Item> items = new List<Item> ();

	void Start(){
		items.Add (new Item ("Kebab",0,"A nice kebab",20,Item.ItemType.Consumable));
		items.Add (new Item ("RedBull",1,"A refreshing redbull",25,Item.ItemType.Consumable));
		items.Add (new Item ("HealthPot",2,"A potion of health",100,Item.ItemType.Consumable));
		items.Add (new Item ("PabstBlueRibbon",3,"A Pabst Blue Ribbon \n quality American beer",50,Item.ItemType.Consumable));
		items.Add (new Item ("Sword",4,"A sharp sword",0,Item.ItemType.Misc));
		items.Add (new Item ("Boot",5,"A leather boot",0,Item.ItemType.Misc));
		items.Add (new Item ("Skull",6,"A human skull",0,Item.ItemType.Misc));
		items.Add (new Item ("Bone",7,"A human bone",0,Item.ItemType.Misc));
		/*items.Add (new Item ("SaphireRing",7,"A gold ring with a saphire",0,Item.ItemType.Accessory));
		items.Add (new Item ("RubyRing",8,"A gold ring with a ruby",0,Item.ItemType.Accessory));
		items.Add (new Item ("EmeraldRing",9,"A gold ring with an emerald",0,Item.ItemType.Accessory));*/
		items.Add (new Item ("Rock",8,"A good old trusty rock",0,Item.ItemType.Misc));
		items.Add (new Item ("Key",9,"A key to a door somewhere",0,Item.ItemType.KeyItem));


	}
}
