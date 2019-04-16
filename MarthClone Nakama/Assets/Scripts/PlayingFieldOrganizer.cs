﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingFieldOrganizer : MonoBehaviour
{
    // Start is called before the first frame update
    public List<PlayableCard> cards;
    void Awake()
    {
        cards = new List<PlayableCard>();
        tempGetChildren();
        OrganizeCards();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OrganizeCards()
    {
        int numCards = cards.Count;
        Debug.Log("Organizing " + numCards + " cards.");
        if (numCards == 0)
            return;
        if(numCards == 1)
        {
            cards[0].transform.position = Vector3.zero;
            return;
        }
        float startPos = 0;
        if(numCards%2 == 0)
        {
            startPos -= 2.0f;
        }
        startPos -= 4 * (((numCards+1) / 2)-1);
        for(int a = 0; a < numCards; a++)
        {
            Vector3 v = new Vector3(startPos, 0, 0);
            Debug.Log("Should be setting card " + a + " to: " + v.ToString());
            //cards[a].gameObject.transform.localPosition.Set(startPos, 0, 0);
            cards[a].transform.position = v;
            startPos += 4;
        }
    }
    public bool AttemptToAddCard(PlayableCard card, Vector3 pos)
    {
        if(cards.Count >= 7)
        {
            return false;
        }
        else
        {
            AddCard(card, pos);
            return true;
        }
    }

    void AddCard(PlayableCard card, Vector3 pos)
    {
        if(cards.Count == 0)
        {
            cards.Add(card);
            return;
        }
        if(pos.x < cards[0].transform.position.x)
        {
            cards.Insert(0, card);
            return;
        }
        for(int a = 0; a < cards.Count - 1; a++)
        {
            if(pos.x > cards[a].transform.position.x && pos.x < cards[a+1].transform.position.x)
            {
                cards.Insert(a + 1, card);
                return;
            }
        }
        cards.Add(card);
        OrganizeCards();
    }

    void tempGetChildren()
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            cards.Add(transform.GetChild(a).GetComponent<PlayableCard>());
        }
    }
}
