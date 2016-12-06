
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


[System.Serializable]
public class Item
{
	public string configurationName;
}

public class ScrollList : MonoBehaviour
{

	public List<Item> itemList;
	public Transform contentPanel;
	public SimpleObjectPool buttonObjectPool;
	private int count = 0;

	void Start ()
	{
		RefreshDisplay ();
	}

	void RefreshDisplay ()
	{

		RemoveButtons ();
		AddButtons ();
	}

	private void RemoveButtons ()
	{
		while (contentPanel.childCount > 0) {
			GameObject toRemove = transform.GetChild (0).gameObject;
			buttonObjectPool.ReturnObject (toRemove);
		}
	}

	private void AddButtons ()
	{
		for (int i = 0; i < itemList.Count; i++) {
			Item item = itemList [i];
			GameObject newButton = buttonObjectPool.GetObject ();
			newButton.transform.SetParent (contentPanel);

			SampleButton sampleButton = newButton.GetComponent<SampleButton> ();
			sampleButton.Setup (item, this);
		}
	}

	public void TryTransferItemToOtherShop (Item item)
	{ //TODO use this to navigate to scene or to load configuration
		
		//	RemoveItem (item, this);
		RefreshDisplay ();
		Debug.Log ("TryTransferItemToOtherShop called with item: " + item.configurationName + " count: " + count.ToString ());
		count++;
	}

	private void RemoveItem (Item itemToRemove, ScrollList shopList)
	{
		for (int i = shopList.itemList.Count - 1; i >= 0; i--) {
			if (shopList.itemList [i] == itemToRemove) {
				shopList.itemList.RemoveAt (i);
			}
		}
	}
}