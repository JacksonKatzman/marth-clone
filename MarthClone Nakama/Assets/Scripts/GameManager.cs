using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Card[] cardDatabase;
    public Dictionary<string, Deck> localDecks;
    public Deck currentDeck;
    public PlayHandler playHandler;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            LoadCards();
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //LoadCards loads the entire card database into the manager and sorts it by ID.
    //Will need a separate list later that includes only the cards currently in rotation
    //and a separate loader for it that happens when we auth with server.
    public void LoadCards()
    {
        cardDatabase = Resources.LoadAll<Card>("Cards");
        Array.Sort(cardDatabase, delegate (Card c1, Card c2)
        {
            return c1.cardID.CompareTo(c2.cardID);
        });
        localDecks = new Dictionary<string, Deck>();
    }

    public void AddLocalDeck(Deck d)
    {
        //For now just overwrite if we have the same name
        if(localDecks.ContainsKey(d.name))
        {
            localDecks.Remove(d.name);
        }
        localDecks.Add(d.name, d);
    }

    void LoadDecks()
    {

    }

    public void PlayButtonPressed(Deck deck)
    {
        //Will do matchmaking here eventually
        currentDeck = deck;
        SceneManager.LoadScene("PlayingField");
    }
}
