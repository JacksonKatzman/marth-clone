using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOrganizer : MonoBehaviour
{
    public List<PlayableCard> cards;
    // Start is called before the first frame update
    void Awake()
    {
        cards = new List<PlayableCard>();
        tempGetChildren();
        OrganizeCards();
    }

    public bool AttemptToAddCard(PlayableCard card)
    {
        if (cards.Count >= 10)
        {
            return false;
        }
        else
        {
            AddCard(card);
            return true;
        }
    }

    void AddCard(PlayableCard card)
    {
        cards.Add(card);
        card.transform.parent = this.transform;
        card.gameObject.GetComponent<CardDragger>().inHand = true;
        OrganizeCards();
    }
    public void RemoveCard(PlayableCard card)
    {
        cards.Remove(card);
        OrganizeCards();
    }

    void OrganizeCards()
    {
        int numCards = cards.Count;
        float moveAmount = 8.0f/numCards;
        Debug.Log("Organizing " + numCards + " cards.");
        if (numCards == 0)
            return;
        if (numCards == 1)
        {
            cards[0].transform.localPosition = Vector3.zero;
            return;
        }
        float startPos = 0;
        if (numCards % 2 == 0)
        {
            startPos -= moveAmount/2;
        }
        startPos -= moveAmount * (((numCards + 1) / 2) - 1);
        for (int a = 0; a < numCards; a++)
        {
            Vector3 v = new Vector3(startPos, 0.01f*a, 0);
            //Vector3 v = new Vector3(startPos, 0.01f * a, -1 * Mathf.Abs(startPos / numCards));
            Debug.Log("Should be setting card " + a + " to: " + v.ToString());
            //cards[a].gameObject.transform.localPosition.Set(startPos, 0, 0);
            cards[a].transform.localPosition = v;
            cards[a].SetDrawLayer(a);
            //cards[a].transform.RotateAroundLocal(transform.up, 5 * startPos);
            //cards[a].transform.Rotate(new Vector3(0, 5 * startPos, 0));
            startPos += moveAmount;
        }
    }

    void tempGetChildren()
    {
        for (int a = 0; a < this.transform.childCount; a++)
        {
            cards.Add(transform.GetChild(a).GetComponent<PlayableCard>());
        }
    }
}
