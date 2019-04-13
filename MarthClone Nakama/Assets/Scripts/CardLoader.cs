using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class CardLoader : MonoBehaviour
{
    MainMenuCanvas mmc;
    DeckBuilder deckBuilder;
    List<GameObject> cardViewers;
    //Card[] sortedCards;
    List<Card> sortedCards;

    [SerializeField] GameObject CardPrefab;
    [SerializeField] GameObject CardViewerPrefab;

    void Awake()
    {
        mmc = GetComponent<MainMenuCanvas>();
        deckBuilder = GetComponent<DeckBuilder>();
        sortedCards = new List<Card>();
        PopulateCardViewers();
    }

    void PopulateCardViewers()
    {
        sortedCards.Clear();
        for(int a = 0; a < GameManager.instance.cardDatabase.Length; a++)
        {
            sortedCards.Add(GameManager.instance.cardDatabase[a]);
        }
        //Sorts by Mana Cost then Name -- simple enough for now
        sortedCards = sortedCards.OrderBy(card => card.manaCost).ThenBy(card => card.name).ToList();

        Debug.Log("Sorted Cards Length: " + sortedCards.Count);
        cardViewers = new List<GameObject>();

        int numViewers = sortedCards.Count / 8;
        if (sortedCards.Count%8 != 0)
        {
            ++numViewers;
        }
        Debug.Log("Num Card Viewers: " + numViewers);
        for(int a = 0; a < numViewers; a++)
        {
            GameObject viewer = Instantiate(CardViewerPrefab, mmc.DeckBuilder.transform);
            for(int b = 0; b < 8; b++)
            {
                if ((a * 8) + b < sortedCards.Count)
                {
                    GameObject card = Instantiate(CardPrefab, viewer.transform);
                    DeckBuilderCard cardScript = card.GetComponent<DeckBuilderCard>();
                    //cardScript.originalCardData = sortedCards[(a * 8) + b];
                    cardScript.BuildCard(sortedCards[(a * 8) + b], deckBuilder);
                }
            }
            cardViewers.Add(viewer);
        }
    }
}