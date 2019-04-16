using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOrganizer : MonoBehaviour
{
    public List<PlayableCard> cards;
    // Start is called before the first frame update
    void AWake()
    {
        cards = new List<PlayableCard>();
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
    }

    void OrganizeCards()
    {
        int numCards = cards.Count;
        Debug.Log("Organizing " + numCards + " cards.");
        if (numCards == 0)
            return;
        if (numCards == 1)
        {
            cards[0].transform.position = Vector3.zero;
            return;
        }
        float startPos = 0;
        if (numCards % 2 == 0)
        {
            startPos -= 1.5f;
        }
        startPos -= 3 * (((numCards + 1) / 2) - 1);
        for (int a = 0; a < numCards; a++)
        {
            Vector3 v = new Vector3(startPos, 0, 0);
            Debug.Log("Should be setting card " + a + " to: " + v.ToString());
            //cards[a].gameObject.transform.localPosition.Set(startPos, 0, 0);
            cards[a].transform.position = v;
            startPos += 3;
        }
    }
}
