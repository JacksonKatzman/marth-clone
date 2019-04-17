using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System;

public class DeckManager : MonoBehaviour
{
    public List<Card> playableDeck;
    public void BuildDeckInitial()
    {
        Deck deck = GameManager.instance.currentDeck;
        playableDeck = new List<Card>();
        foreach (KeyValuePair<int, int> c in deck.deckContents)
        {
            AddCardToPlayableDeck(c.Key, c.Value);
        }
        Shuffle();
    }

    public void AddCardToPlayableDeck(int cardID, int amount)
    {
        Card cardTemplate = GameManager.instance.cardDatabase[cardID];
        for (int a = 0; a < amount; a++)
        {
            playableDeck.Add(cardTemplate);
        }
    }

    public void Shuffle()
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = playableDeck.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            Card value = playableDeck[k];
            playableDeck[k] = playableDeck[n];
            playableDeck[n] = value;
        }
    }
}
