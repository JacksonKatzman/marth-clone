using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckLoader : MonoBehaviour
{
    [SerializeField] GameObject builderDeckWindow;
    [SerializeField] GameObject playerDeckWindow;
    [SerializeField] GameObject DeckButtonPrefab;
    public Deck currentDeckSelected;
    List<GameObject> deckButtons;
    MainMenuCanvas mms;
    // Start is called before the first frame update
    void Awake()
    {
        mms = GetComponent<MainMenuCanvas>();
        deckButtons = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCurrentSelectedDeck(string deckName, bool forEditor)
    {
        Debug.Log("Trying to change currently selected deck!");
        if (GameManager.instance.localDecks.ContainsKey(deckName))
        {
            Debug.Log("Deck Name Found in localDecks!");
            //GameManager.instance.localDecks.TryGetValue(deckName, out currentDeckSelected);
            //currentDeckSelected = GameManager.instance.localDecks[deckName];
            currentDeckSelected = new Deck(deckName, GameManager.instance.localDecks[deckName].deckContents);
            if (forEditor)
                mms.ClickEditDeck();
            else
                HighlightSelected(deckName);
            //currentDeckSelected.PrintContents();
        }
    }

    public void CreateDeckButtons()
    {
        ClearDeckButtons();
        foreach(KeyValuePair<string, Deck> d in GameManager.instance.localDecks)
        {
            GameObject deckButtonObj = Instantiate(DeckButtonPrefab, builderDeckWindow.transform);
            DeckSelectorButton dsb = deckButtonObj.GetComponent<DeckSelectorButton>();
            dsb.SetUp(d.Key, this, true);
            deckButtons.Add(deckButtonObj);
        }
        foreach (KeyValuePair<string, Deck> d in GameManager.instance.localDecks)
        {
            GameObject deckButtonObj = Instantiate(DeckButtonPrefab, playerDeckWindow.transform);
            DeckSelectorButton dsb = deckButtonObj.GetComponent<DeckSelectorButton>();
            dsb.SetUp(d.Key, this, false);
            deckButtons.Add(deckButtonObj);
        }
    }

    void HighlightSelected(string deckName)
    {
        foreach(GameObject gObj in deckButtons)
        {
            DeckSelectorButton dsb = gObj.GetComponent<DeckSelectorButton>();
            if(dsb.deckName == deckName)
            {
                gObj.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                gObj.GetComponent<Image>().color = Color.white;
            }
        }
    }

    void ClearDeckButtons()
    {
        currentDeckSelected = null;
        foreach(GameObject g in deckButtons)
        {
            Destroy(g);
        }
        deckButtons.Clear();
    }
}
