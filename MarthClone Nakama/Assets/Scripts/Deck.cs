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

    public void PrintContents()
    {
        foreach(KeyValuePair<int, int> pair in deckContents)
        {
            Debug.Log("Card ID: " + pair.Key + " / Card Amount: " + pair.Value);
        }
    }

    // Start is called before the first frame update
    
}
