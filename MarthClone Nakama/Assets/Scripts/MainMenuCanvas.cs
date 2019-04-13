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

	string CurrentDeckToEdit;
	string CurrentDeckToPlay;

	// Use this for initialization
	void Start () {
		DeckEditorButtons = new List<GameObject>();
		CurrentDeckToEdit = null;
		CurrentDeckToPlay = null;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateDeckEditorButton(string deckName)
	{
		GameObject DEB = Instantiate(DeckEditorButtonPrefab);
		DEB.transform.SetParent(DeckLayout.transform);
		DEB.transform.GetComponentInChildren<Text>().text = deckName;
		DEB.GetComponent<DeckSelectorButton>().mms = this;
		DEB.GetComponent<DeckSelectorButton>().DeckName = deckName;
		DeckEditorButtons.Add(DEB);
		GameObject DEB2 = Instantiate(DeckEditorButtonPrefab);
		DEB2.transform.SetParent(PlayMenuDeckLayout.transform);
		DEB2.transform.GetComponentInChildren<Text>().text = deckName;
		DEB2.GetComponent<DeckSelectorButton>().mms = this;
		DEB2.GetComponent<DeckSelectorButton>().DeckName = deckName;
		DeckEditorButtons.Add(DEB2);
	}

	public void OpenDeckBuilderMenu()
	{
		DeckBuilderMenu.SetActive(true);
		MainMenuObjects.SetActive(false);
		CurrentDeckToEdit = null;
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
	}

	public void OpenPlayMenu()
	{
		PlayMenu.SetActive(true);
		MainMenuObjects.SetActive(false);
		CurrentDeckToEdit = null;
	}

	public void ClosePlayMenu()
	{
		PlayMenu.SetActive(false);
		MainMenuObjects.SetActive(true);
	}

	public void BeginPlayGame()
	{
		if(CurrentDeckToEdit != null)
		{
			
		}
	}

	public void CloseDeckBuilder()
	{
		DeckBuilder.SetActive(false);
		DeckBuilderMenu.SetActive(true);
		ClearDeckViewer();
		foreach(GameObject current in DeckEditorButtons)
		{
			Destroy(current);
		}
		DeckEditorButtons.Clear();

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
}
