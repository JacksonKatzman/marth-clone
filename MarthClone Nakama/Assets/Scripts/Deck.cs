using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Deck
{
    public string name;
    public Dictionary<int, int> deckContents;

    public Deck(string name, Dictionary<int, int> deckContents)
    {
        this.name = name;
        this.deckContents = deckContents;
    }

    public Deck(string deckName, string contents)
    {
        this.name = deckName;
        this.deckContents = new Dictionary<int, int>();
        Debug.Log("Begin Loading Deck with Contents: " + contents);
        string[] cardPairs = contents.Split(',');
        for(int a = 0; a < cardPairs.Length-1; a++)
        {
            Debug.Log("CARD PAIR: " + cardPairs[a]);
            string[] cardInfo = cardPairs[a].Split('/');
            int id = Int32.Parse(cardInfo[0]);
            Debug.Log("Card ID: " + id);
            int amount = Int32.Parse(cardInfo[1]);
            Debug.Log("Card Amount: " + amount);
            deckContents.Add(id, amount);
        }
    }

    public void PrintContents()
    {
        foreach(KeyValuePair<int, int> pair in deckContents)
        {
            Debug.Log("Card ID: " + pair.Key + " / Card Amount: " + pair.Value);
        }
    }

    
    
}
