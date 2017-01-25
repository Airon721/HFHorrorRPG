/**
 * @Author David Diienno
 * 1/23/17
 * Item class to be used in unison with the players inventory
*/

public class Item{
	public string itemName;
	private string itemID;
	public int numOfItem = 0;
	/**
	 *@params name - String to determine the items title
	 *@params id - Unique ID to the item, used for equals, and crafting later.
	 */
	public Item(string name, string id){
		itemName = name;
		itemID = id;	
	}
	// TODO - implement constructor to create an item based off the item ID (Data class using hashmap(itemName, ID))


	//Accessor & Mutator Methods
	public string getName(){
		return itemName;
	}
	public string getID(){
		return itemID;
	}
	public int getNumOf(){
		return numOfItem;
	}
	public void setName(string name){
		itemName = name;
	}
	public void setID(string id){
		itemID = id;
	}
	public void setNumOf(int num){
		numOfItem = num;
	}

}
