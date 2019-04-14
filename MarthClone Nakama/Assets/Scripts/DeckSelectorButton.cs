using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectorButton : MonoBehaviour {

	public DeckLoader deckLoader;
	public string deckName;
    [SerializeField] Text myText;
	// Use this for initialization
	void Start () {

	}
    public void SetUp(string deckName, DeckLoader dl)
    {
        this.deckName = deckName;
        myText.text = deckName;
        this.deckLoader = dl;
    }

    public void OnClick()
    {
        deckLoader.ChangeCurrentSelectedDeck(deckName);
        Debug.Log("Deck Selector Button Clicked!");
    }

	

}
