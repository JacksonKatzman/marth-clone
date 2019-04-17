using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class DeckBuilder : MonoBehaviour
{
    [SerializeField] GameObject deckWindow;
    [SerializeField] InputField deckNameText;
    [SerializeField] Text cardCount;
    [SerializeField] GameObject cardPlatePrefab;

    Dictionary<int, int> currentDeck;
    List<GameObject> deckDisplayPlates;
    DeckLoader deckLoader;
    MainMenuCanvas mms;

    int currentCardsInDeck = 0;
    static int MAX_NUM_ALLOWED = 2;
    static int MAX_CARDS_DECK = 30;

    void Awake()
    {
        currentDeck = new Dictionary<int, int>();
        deckDisplayPlates = new List<GameObject>();
        deckLoader = GetComponent<DeckLoader>();
        mms = GetComponent<MainMenuCanvas>();
    }

    public void ClearDeck()
    {
        currentDeck.Clear();
        deckNameText.text = "Default Deck Name";
        ClearDeckDisplay();
    }
    
    void ClearDeckDisplay()
    {
        foreach(GameObject g in deckDisplayPlates)
        {
            Destroy(g);
        }
        deckDisplayPlates.Clear();
        currentCardsInDeck = 0;
    }

    public void SetDeck()
    {
        ClearDeckDisplay();
        currentDeck = new Dictionary<int,int>(deckLoader.currentDeckSelected.deckContents);
        deckNameText.text = deckLoader.currentDeckSelected.name;
        Debug.Log("Set Deck For Editing. Name: " + deckLoader.currentDeckSelected.name);
        Debug.Log("Contents:");
        PrintContents();
        RebuildVisibleDeckList();
    }

    

    public bool TryAddCard(Card c)
    {
        if(currentCardsInDeck >= MAX_CARDS_DECK)
        {
            return false;
        }
        if(currentDeck.ContainsKey(c.cardID))
        {
            if(currentDeck[c.cardID] < MAX_NUM_ALLOWED)
            {
                currentDeck[c.cardID] += 1;
                RebuildVisibleDeckList();
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            currentDeck.Add(c.cardID, 1);
            RebuildVisibleDeckList();
            return true;
        }
    }

    public bool TryRemoveCard(Card c)
    {
        if (currentDeck.ContainsKey(c.cardID))
        {
            currentDeck[c.cardID] -= 1;
            if (currentDeck[c.cardID] <= 0)
            {
                currentDeck.Remove(c.cardID);
            }
            RebuildVisibleDeckList();
            return true;
        }
        else
        {
            return false;
        }
    }

    void RebuildVisibleDeckList()
    {
        ClearDeckDisplay();
        List<CardNameplateScript> nameplateScripts = new List<CardNameplateScript>();
        foreach(KeyValuePair<int, int> entry in currentDeck)
        {
            CardNameplateScript cns = new CardNameplateScript();
            cns.SetCard(GameManager.instance.cardDatabase[entry.Key]);
            cns.SetAmount(entry.Value);
            nameplateScripts.Add(cns);
        }
        nameplateScripts = nameplateScripts.OrderBy(cns => cns.card.manaCost).ThenBy(cns => cns.card.name).ToList();
        foreach(CardNameplateScript cns in nameplateScripts)
        {
            GameObject cardPlate = Instantiate(cardPlatePrefab, deckWindow.transform);
            CardNameplateScript cardNameplateScript = cardPlate.GetComponent<CardNameplateScript>();
            cardNameplateScript.card = cns.card;
            cardNameplateScript.amount = cns.amount;
            cardNameplateScript.deckBuilder = this;
            cardNameplateScript.UpdateNameplate();
            deckDisplayPlates.Add(cardPlate);
            currentCardsInDeck += cns.amount;
        }
        //Update visible card counter;
        cardCount.text = "" + currentCardsInDeck + "/" + MAX_CARDS_DECK;
    }

    public void SaveDeck()
    {
        //TODO: Gotta write some code to save the currentDeck dictionary of card IDs and amounts to a database or however you want to do it.
        //You can use the name as the key or just save the name along with the ID just make sure it can pull the name as well.
        GameManager.instance.AddLocalDeck(new Deck(deckNameText.text, currentDeck));
        mms.CloseDeckBuilder();
    }

    void PrintContents()
    {
        foreach (KeyValuePair<int, int> pair in currentDeck)
        {
            Debug.Log("Card ID: " + pair.Key + " / Card Amount: " + pair.Value);
        }
    }
}
