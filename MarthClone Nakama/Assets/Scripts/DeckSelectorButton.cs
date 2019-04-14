using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSelectorButton : MonoBehaviour {

	public DeckLoader deckLoader;
	public string deckName;
    public bool forEditor;
    [SerializeField] Text myText;
	// Use this for initialization
	void Start () {

	}
    public void SetUp(string deckName, DeckLoader dl, bool edit)
    {
        this.deckName = deckName;
        myText.text = deckName;
        this.deckLoader = dl;
        this.forEditor = edit;
    }

    public void OnClick()
    {
        deckLoader.ChangeCurrentSelectedDeck(deckName, forEditor);
        Debug.Log("Deck Selector Button Clicked!");
    }

	

}
