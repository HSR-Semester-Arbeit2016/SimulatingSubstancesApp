using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {

	public Button buttonComponent;
	public Text nameLabel;


	private Item item;
	private ScrollList scrollList;

	// Use this for initialization
	void Start () 
	{
		buttonComponent.onClick.AddListener (OnButtonClick);
	}

	public void Setup(Item currentItem, ScrollList currentScrollList)
	{
		item = currentItem;
		nameLabel.text = item.configurationName;
		scrollList = currentScrollList;

	}

	public void OnButtonClick()
	{
		scrollList.TryTransferItemToOtherShop (item);
		#if DEBUG
		Debug.Log("Button: " + item.configurationName + "clicked!");
		#endif
	}
}