using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoGame.Scripts.Gameplay.NetworkCommunication;
using DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates;

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
       // Debug.Log("Organizing " + numCards + " cards.");
        if (numCards == 0)
            return;
        if(numCards == 1)
        {
            cards[0].transform.localPosition = Vector3.zero;
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
            Vector3 v = new Vector3(startPos, 0, transform.position.z);
           // Debug.Log("Should be setting card " + a + " to: " + v.ToString());
            //cards[a].gameObject.transform.localPosition.Set(startPos, 0, 0);
            //cards[a].transform.localPosition = v;
            cards[a].GetComponent<CardDragger>().BeginLerp(cards[a].transform.position, v);
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

    public void RemoveCard(PlayableCard card)
    {
        OrganizeCards();
        cards.Remove(card);    
    }

    public void AddCard(PlayableCard card, int absPos)
    {
        Debug.Log("Attempting to add card to opponents board.");
        GameManager.instance.currentNextID++;
        card.gameObject.transform.parent = transform;
        cards.Insert(absPos, card);
        OrganizeCards();
    }

    void AddCard(PlayableCard card, Vector3 pos)
    {
        int cardNetID = GameManager.instance.currentNextID;
        card.networkID = cardNetID;
        GameManager.instance.currentNextID++;
        int absPos = 0;
        MatchMessageCardPlayed cardPlayed;
        if (cards.Count == 0)
        {
            cards.Add(card);
            OrganizeCards();
            cardPlayed = new MatchMessageCardPlayed(card.baseCard.cardID, (int)card.baseCard.cardType, absPos, cardNetID);
            MatchCommunicationManager.Instance.SendMatchStateMessage(MatchMessageType.CardPlayed, cardPlayed);
            return;
        }
        if(pos.x < cards[0].transform.position.x)
        {
            cards.Insert(0, card);
            OrganizeCards();
            cardPlayed = new MatchMessageCardPlayed(card.baseCard.cardID, (int)card.baseCard.cardType, absPos, cardNetID);
            MatchCommunicationManager.Instance.SendMatchStateMessage(MatchMessageType.CardPlayed, cardPlayed);
            return;
        }
        for(int a = 0; a < cards.Count - 1; a++)
        {
            if(pos.x > cards[a].transform.position.x && pos.x < cards[a+1].transform.position.x)
            {
                cards.Insert(a + 1, card);
                OrganizeCards();
                absPos = a + 1;
                cardPlayed = new MatchMessageCardPlayed(card.baseCard.cardID, (int)card.baseCard.cardType, absPos, cardNetID);
                MatchCommunicationManager.Instance.SendMatchStateMessage(MatchMessageType.CardPlayed, cardPlayed);
                return;
            }
        }
        cards.Add(card);
        absPos = cards.Count-1;
        cardPlayed = new MatchMessageCardPlayed(card.baseCard.cardID, (int)card.baseCard.cardType, absPos, cardNetID);
        MatchCommunicationManager.Instance.SendMatchStateMessage(MatchMessageType.CardPlayed, cardPlayed);
        OrganizeCards();
    }

    void tempGetChildren()
    {
        for(int a = 0; a < this.transform.childCount; a++)
        {
            cards.Add(transform.GetChild(a).GetComponent<PlayableCard>());
        }
    }

    public void MakeCardsPlayable(bool playable)
    {
        foreach (PlayableCard card in cards)
        {
            CardDragger dragger = card.GetComponent<CardDragger>();
            dragger.dragable = playable;
            card.particles.SetActive(playable);
        }
    }
}
