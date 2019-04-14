using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuCanvas : MonoBehaviour {

	public GameObject DeckBuilderMenu;
	public GameObject PlayMenu;
	public GameObject DeckBuilder;
	public GameObject DeckViewer;
	public GameObject MainMenuObjects;
	public GameObject DeckLayout;
	public GameObject PlayMenuDeckLayout;
	[SerializeField] GameObject DeckEditorButtonPrefab;
	List<GameObject> DeckEditorButtons;
    CardLoader cardLoader;
    DeckLoader deckLoader;
    DeckBuilder deckBuilder;

	string CurrentDeckToEdit;
	string CurrentDeckToPlay;

	// Use this for initialization
	void Start () {
		DeckEditorButtons = new List<GameObject>();
        cardLoader = GetComponent<CardLoader>();
        deckLoader = GetComponent<DeckLoader>();
        deckBuilder = GetComponent<DeckBuilder>();
		CurrentDeckToEdit = null;
		CurrentDeckToPlay = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OpenDeckBuilderMenu()
	{
		DeckBuilderMenu.SetActive(true);
		MainMenuObjects.SetActive(false);
		CurrentDeckToEdit = null;
        deckLoader.CreateDeckButtons();
	}

	public void CloseDeckBuilderMenu()
	{
		DeckBuilderMenu.SetActive(false);
		MainMenuObjects.SetActive(true);
	}

	public void OpenDeckBuilder()
	{
		DeckBuilder.SetActive(true);
		DeckBuilderMenu.SetActive(false);
        deckBuilder.ClearDeck();  
	}

    public void OpenDeckBuilderAsEditor()
    {
        DeckBuilder.SetActive(true);
        DeckBuilderMenu.SetActive(false);
    }

	public void OpenPlayMenu()
	{
		PlayMenu.SetActive(true);
		MainMenuObjects.SetActive(false);
		CurrentDeckToEdit = null;
        deckLoader.CreateDeckButtons();
    }

	public void ClosePlayMenu()
	{
		PlayMenu.SetActive(false);
		MainMenuObjects.SetActive(true);
	}

	public void BeginPlayGame()
	{

	}

	public void CloseDeckBuilder()
	{
		DeckBuilder.SetActive(false);
		DeckBuilderMenu.SetActive(true);
		//ClearDeckViewer();
		foreach(GameObject current in DeckEditorButtons)
		{
			Destroy(current);
		}
		DeckEditorButtons.Clear();
        deckLoader.CreateDeckButtons();
	}

	void ClearDeckViewer()
	{
		for(int a = 0; a < DeckViewer.transform.childCount; a++)
		{
			Destroy(DeckViewer.transform.GetChild(a).gameObject);
		}
	}

	public void SetSelectedDeck(string s)
	{
		CurrentDeckToEdit = s;
		Debug.Log("Selected Deck: " + s);
	}

	public void EditSelectedDeck()
	{
		if(CurrentDeckToEdit != null)
		{
			
			OpenDeckBuilder();
		}
	}

	public void EditNewDeck()
	{
		OpenDeckBuilder();
	}

    public void ClickEditDeck()
    {
        if (deckLoader.currentDeckSelected != null)
        {
            Debug.Log("Clicked Edit Deck and there was a deck to edit.");
            deckLoader.currentDeckSelected.PrintContents();
            deckBuilder.SetDeck();
            OpenDeckBuilderAsEditor();
        }
    }
}
