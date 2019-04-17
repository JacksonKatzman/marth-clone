using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{
    public HandOrganizer myHandOrganizer;
    public PlayingFieldOrganizer myPlayingFieldOrganizer;
    public List<PlayableCard> playableDeck;
    public DeckManager deckManager;

    [SerializeField] GameObject PlayableMinionPrefab;
    [SerializeField] GameObject PlayableSpellPrefab;

    void Awake()
    {
        deckManager = GetComponent<DeckManager>();
        if (GameManager.instance != null)
        {
            GameManager.instance.playHandler = this;
            deckManager.BuildDeckInitial();
            DrawCard();
            DrawCard();
            DrawCard();
        }
    }

    public bool DrawCard()
    {
        if(deckManager.playableDeck.Count > 0)
        {
            Card cardTemplate = deckManager.playableDeck[0];
            GameObject cardObj;
            if(cardTemplate.cardType == CardInfo.CardType.Minion)
            {
                cardObj = Instantiate(PlayableMinionPrefab);
            }
            else
            {
                cardObj = Instantiate(PlayableSpellPrefab);
            }
            PlayableCard playableCard = cardObj.GetComponent<PlayableCard>();
            playableCard.baseCard = cardTemplate;
            playableCard.SetToBaseCard();
            cardObj.GetComponent<CardDragger>().playHandler = this;
            myHandOrganizer.AttemptToAddCard(playableCard);
            deckManager.playableDeck.RemoveAt(0);
        }
        return false;
    }

}
