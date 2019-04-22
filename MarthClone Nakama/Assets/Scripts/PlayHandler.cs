using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardInfo;
using TMPro;
using DemoGame.Scripts.Session;

public class PlayHandler : MonoBehaviour
{
    public HandOrganizer myHandOrganizer, opponentHandOrganizer;
    public PlayingFieldOrganizer myPlayingFieldOrganizer, opponentFieldOrganizer;
    public HeroCard myHeroCard, enemyHeroCard;
    public List<PlayableCard> playableDeck;
    public DeckManager deckManager;
    public EndTurnButton endTurnButton;
    int maxMana = 0;
    int currentMana = 0;
    public TextMeshPro manaText;
    public List<PlayableCard> heroCards;

    [SerializeField] GameObject PlayableMinionPrefab;
    [SerializeField] GameObject PlayableSpellPrefab;

    void Awake()
    {
        deckManager = GetComponent<DeckManager>();
        if (GameManager.instance != null)
        {
            GameManager.instance.playHandler = this;
            deckManager.BuildDeckInitial();
            /*
            DrawCard();
            DrawCard();
            DrawCard();
            */
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
            //int id = (NakamaSessionManager.Instance.GetNextNetworkID()).Result;
            //Debug.Log("assigning card the id: " + id);
            //playableCard.networkID = id;
            playableCard.baseCard = cardTemplate;
            playableCard.SetToBaseCard();
            cardObj.GetComponent<CardDragger>().playHandler = this;
            cardObj.GetComponent<CardDragger>().owned = true;
            cardObj.GetComponent<CardDragger>().inHand = true;
            myHandOrganizer.AttemptToAddCard(playableCard);
            deckManager.playableDeck.RemoveAt(0);
        }
        return false;
    }


    public void StartTurn()
    {
        DrawCard();
        if(maxMana < 10)
            maxMana += 1;
        currentMana = maxMana;
        UpdateMana();
        myPlayingFieldOrganizer.MakeCardsPlayable(true);
        endTurnButton.ChangeSprite(true);
        //Toggle the end turn button back
    }

    public void EndTurn()
    {
        myHandOrganizer.MakeCardsPlayable(false);
        myPlayingFieldOrganizer.MakeCardsPlayable(false);
        endTurnButton.ChangeSprite(false);
    }

    public void OpponentPlayedCard(int cardID, int cardType, int absPos, int netID)
    {
        //for later, check for real cards
        if((CardInfo.CardType)cardType == CardInfo.CardType.Minion)
        {
            //PlayableMinion minion = new PlayableMinion(GameManager.instance.cardDatabase[cardID]);
            GameObject playableMinion = Instantiate(PlayableMinionPrefab);
            PlayableMinion minion = playableMinion.GetComponent<PlayableMinion>();
            minion.networkID = netID;
            minion.baseCard = GameManager.instance.cardDatabase[cardID];
            minion.SetToBaseCard();
            opponentFieldOrganizer.AddCard(minion, absPos);
        }
    }

    public void HandleIncomingCombat(int theirID, int myID)
    {
        PlayableCard myCard = null;
        PlayableMinion theirCard = null;
        foreach(PlayableCard c in myPlayingFieldOrganizer.cards)
        {
            if(c.networkID == myID)
            {
                myCard = (PlayableMinion)c;
                continue;
            }
        }
        foreach (PlayableCard c in opponentFieldOrganizer.cards)
        {
            if (c.networkID == theirID)
            {
                theirCard = (PlayableMinion)c;
                continue;
            }
        }
        foreach(PlayableCard c in heroCards)
        {
            if(c.networkID == myID)
            {
                myCard = c;
            }
        }
        if(myCard != null && theirCard != null)
        {
            myCard.RecieveAttack(theirCard);
            theirCard.RecieveAttack(myCard);
        }
    }

    public void SetupFirstTurn(bool goingFirst)
    {
        if(goingFirst)
        {
            myHeroCard.networkID = -1;
            enemyHeroCard.networkID = -2;
            DrawCard();
            DrawCard();
            DrawCard();
            //Handle Mulligans implemented later
            StartTurn();
        }
        else
        {
            myHeroCard.networkID = -2;
            enemyHeroCard.networkID = -1;
            DrawCard();
            DrawCard();
            DrawCard();
            DrawCard();
            EndTurn();
        }
    }

    public void SpendMana(int amount)
    {
        currentMana -= amount;
        UpdateMana();
    }
    public int GetCurrentMana()
    {
        return currentMana;
    }
    void UpdateMana()
    {
        manaText.text = "" + currentMana + "/" + maxMana;
        myHandOrganizer.MakeCardsPlayable(true);
    }
}
