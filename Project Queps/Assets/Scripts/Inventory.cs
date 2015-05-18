using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> inventory = new List<Item>();
	public List<Item> slots = new List<Item> ();
	//private List<Item> equipped = new List<Item>();
	private bool showInventory;
	private ItemDatabase database;
	private bool showToolTip;
	private string tooltip;
	public GameObject player;


	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;

	private AnimatorLogic AnLog;

	// Use this for initialization
	void Start () {
		for(int i=0;i<(slotsX*slotsY);i++){
			slots.Add (new Item());
			inventory.Add (new Item());
		}

		database = GameObject.FindGameObjectWithTag ("Item Database").GetComponent<ItemDatabase> ();
		AnLog = player.GetComponent<AnimatorLogic> ();

		//testar att lägga till items i inventory
		AddItem (0);
		AddItem (3);
		AddItem (9);
	}

	void Update(){
		if (Input.GetButtonDown ("Inventory")) {

			Cursor.visible = true;
			showInventory = !showInventory;
		}

		if (AnLog.getLockOn () && showInventory == true) {
			showInventory = !showInventory;
		}

		if (showInventory == false) 
		{
			Cursor.visible = false;
			showToolTip = false;
		}


	}
	public bool Getshowinv(){
		return this.showInventory;
	}
	void OnGUI(){
		tooltip = "";

		GUI.skin = skin;

		if(showInventory){
			DrawInventory();
		}



		if(showToolTip){
			GUI.Box(new Rect(Event.current.mousePosition.x-200,Event.current.mousePosition.y,200,200),tooltip);
		}

		if(draggingItem){
			GUI.DrawTexture(new Rect(Event.current.mousePosition.x,Event.current.mousePosition.y,50,50),draggedItem.itemIcon);
		}

	}

	void DrawInventory(){
		Event e = Event.current;//event
		int i = 0;//index
		for(int y=0;y<slotsY;y++){
			for(int x=0;x<slotsX;x++){
				Rect slotRect = new Rect(((Screen.width-60)-(60*x)),y*60,50,50);
				GUI.Box(slotRect,"",skin.GetStyle("slot"));
				slots[i]=inventory[i];
				if(slots[i].itemName != null){
					GUI.DrawTexture(slotRect,slots[i].itemIcon);
					if(slotRect.Contains(Event.current.mousePosition)){
						tooltip = CreateToolTip(slots[i]);
						if(!draggingItem){
							showToolTip = true;
						}
						if(e.button == 0 && e.type == EventType.mouseDrag && !draggingItem){
							draggingItem = true;
							prevIndex = i;
							draggedItem = slots[i];
							inventory[i] = new Item();
						}


						if(e.type == EventType.MouseUp && draggingItem){
							inventory[prevIndex] = inventory[i];
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}

						//användning av consume items
						if(e.isMouse && e.type == EventType.mouseDown && e.button == 1){
							if(slots[i].itemType == Item.ItemType.Consumable || slots[i].itemType == Item.ItemType.Accessory || slots[i].itemType == Item.ItemType.Misc){
								UseItem(slots[i],i,true);
							}
						}
					}

					
				}
				else{
					if(slotRect.Contains(e.mousePosition)){
						if(e.type == EventType.MouseUp && draggingItem){
							inventory[i] = draggedItem;
							draggingItem = false;
							draggedItem = null;
						}
					}
				}
				if(tooltip==""){
					showToolTip = false;
				}

				i++;
			}
		}
	}





	string CreateToolTip(Item item){
		tooltip = item.itemName+ "\n\n" + item.itemDesc + "\n\n" + item.itemType;
		return tooltip;
	}

	void RemoveItem(int id){
		for(int i=0;i<inventory.Count;i++){
			if(inventory[i].itemID==id){
				inventory[i]= new Item();
				break;
			}
		}
	}







	public void AddItem(int id){
		for (int i=0; i<inventory.Count; i++) {
			if(inventory[i].itemName == null){
				for(int j=0;j<database.items.Count;j++){
					if(database.items[j].itemID==id){
						inventory[i] = database.items[j];
					}
				}
				break;
			}
		}
	}








	bool InventoryContains(int id)
	{
		for (int i = 0; i < inventory.Count; i++)
			if (inventory[i].itemID == id) return true;
		return false;
	}

	//consumables
	void UseItem(Item item, int slot, bool deleteItem){
		switch(item.itemID){

			case 0:{//kebab
				print ("used consumable:" + item.itemName);
				AnLog.DamagePlayer(-(item.itemPower));
				break;
			}
			case 1:{//redbull
				print ("used consumable:" + item.itemName);
				AnLog.DamagePlayer(-(item.itemPower));
				break;
			}
			case 2:{//health potion
				print ("used consumable:" + item.itemName);
				AnLog.health=AnLog.maxHealth;
				break;
			}
			case 3:{//pabst blue ribbon
				print ("used consumable:" + item.itemName);
				AnLog.DamagePlayer(item.itemPower);
				break;
			}
			case 4:{//sword
				print ("Discarded item:" + item.itemName);
				break;
			}
			case 5:{//boot
				print ("Discarded item:" + item.itemName);
				break;
			}
			case 6:{//skull
				print ("Discarded item:" + item.itemName);
				break;
			}
			case 7:{//bone
				print ("Discarded item:" + item.itemName);
				break;
			}
			/*case 7:{//saphire ring avaktiverade i draw funktionen
				print ("Equiped Item:" + item.itemName);
				break;
			}
			case 8:{//ruby ring
				print ("Equiped Item:" + item.itemName);
				break;
			}
			case 9:{//emerald ring
				print ("Equiped Item:" + item.itemName);
				break;
			}*/
			case 8:{//Rock
				print ("Discarded item:" + item.itemName);
				break;
			}
		}
		if(deleteItem){
			inventory[slot] = new Item();
		}
	}



}
