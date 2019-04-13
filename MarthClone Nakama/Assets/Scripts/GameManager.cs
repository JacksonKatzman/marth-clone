using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public Card[] cardDatabase;
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
    }
}
