using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectorButton : MonoBehaviour {

	public MainMenuCanvas mms;
	public string DeckName;
	// Use this for initialization
	void Start () {
//		DeckName = GetComponent<Text>().text;
	}

	public void SetCurrentDeck()
	{
		mms.SetSelectedDeck(DeckName);
	}

}
