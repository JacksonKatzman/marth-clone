using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckLoader : MonoBehaviour
{
    [SerializeField] GameObject deckWindow;
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

    public void ChangeCurrentSelectedDeck(string deckName)
    {
        Debug.Log("Trying to change currently selected deck!");
        if (GameManager.instance.localDecks.ContainsKey(deckName))
        {
            Debug.Log("Deck Name Found in localDecks!");
            //GameManager.instance.localDecks.TryGetValue(deckName, out currentDeckSelected);
            //currentDeckSelected = GameManager.instance.localDecks[deckName];
            currentDeckSelected = new Deck(deckName, GameManager.instance.localDecks[deckName].deckContents);
            mms.ClickEditDeck();
            //currentDeckSelected.PrintContents();
        }
    }

    public void CreateDeckButtons()
    {
        ClearDeckButtons();
        foreach(KeyValuePair<string, Deck> d in GameManager.instance.localDecks)
        {
            GameObject deckButtonObj = Instantiate(DeckButtonPrefab, deckWindow.transform);
            DeckSelectorButton dsb = deckButtonObj.GetComponent<DeckSelectorButton>();
            dsb.SetUp(d.Key, this);
            deckButtons.Add(deckButtonObj);
        }
    }

   

    void ClearDeckButtons()
    {
        foreach(GameObject g in deckButtons)
        {
            Destroy(g);
        }
        deckButtons.Clear();
    }
}
