using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using DemoGame.Scripts.Session;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Card[] cardDatabase;
    public Dictionary<string, Deck> localDecks;
    public Deck currentDeck;
    public PlayHandler playHandler;
    public MatchUI matchUI;

    bool myTurn = true;
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            LoadCards();
            //AttemptConnect();
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
        SaveDecks();
    }

    void SaveDecks()
    {
        string allDeckNames = "";
        foreach(KeyValuePair<string, Deck> d in localDecks)
        {
            allDeckNames += d.Key;
            allDeckNames += ",";
            string deckContentsString = "";
            foreach(KeyValuePair<int,int> cardPair in d.Value.deckContents)
            {
                deckContentsString += (cardPair.Key + "/" + cardPair.Value);
                deckContentsString += ",";
            }
            Debug.Log("Saving Deck: " + d.Key + " with contents: " + deckContentsString);
            PlayerPrefs.SetString(d.Key, deckContentsString);
        }
        //string decksWithID = "Decks:";
        //decksWithID += NakamaSessionManager.Instance.Account.Devices.
        Debug.Log("Saving to $$Decks: " + allDeckNames);
        PlayerPrefs.SetString("$$Decks", allDeckNames);
    }
    void LoadDecks()
    {
        string allDeckNames = PlayerPrefs.GetString("$$Decks");
        if(allDeckNames.Length > 0)
        {
            string[] deckNames = allDeckNames.Split(',');
            for(int a = 0; a < deckNames.Length-1; a++)
            {
                Debug.Log("Begin loading Deck: " + deckNames[a]);
                string deckContents = PlayerPrefs.GetString(deckNames[a]);
                Deck deck = new Deck(deckNames[a], deckContents);
                localDecks.Add(deck.name, deck);
            }
        }
    }

    public void PlayButtonPressed(Deck deck)
    {
        //Will do matchmaking here eventually
        currentDeck = deck;
        StartMatchMaking();
        //SceneManager.LoadScene("PlayingField");
    }

    void StartMatchMaking()
    {
        NakamaSessionManager.Instance.StartMachmaker();
    }

    public void CancelMatchmaking()
    {
        NakamaSessionManager.Instance.StopMatchmaker();
    }

    public async void AttemptConnect()
    {
        AuthenticationResponse response = await NakamaSessionManager.Instance.ConnectAsync();
        if(response != AuthenticationResponse.Error)
        {
            Debug.Log("Successful log in.");
            LoadDecks();
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("Error in Log in: " + response);
        }
    }

    public void StartTurn()
    {
        if (!myTurn)
        {
            myTurn = true;
            playHandler.StartTurn();
            Debug.Log("Your turn has started!");
        }
    }

    public void EndTurn()
    {
        if (myTurn)
        {
            myTurn = false;
            playHandler.EndTurn();
            Debug.Log("Your turn has ended.");
        }
    }
}
