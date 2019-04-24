using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;
using TMPro;

public class DeckBuilderCard : MonoBehaviour {

	public TextMeshProUGUI AttackText, HealthText, CostText, NameText, ClassText, DescriptionText;
    public Image cardImage;
	public int AttackStat, HealthStat, CostStat;
	public string NameString, RarityString;
    public Card originalCardData;
    DeckBuilder deckBuilder;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BuildCard(Card c, DeckBuilder builder)
	{
        originalCardData = c;
        deckBuilder = builder;
		if(originalCardData != null)
        {
            CostStat = originalCardData.manaCost;
            NameString = originalCardData.name;
            CostText.text = CostStat.ToString();
            NameText.text = NameString;
            DescriptionText.text = originalCardData.description;
            if (originalCardData.cardType == CardInfo.CardType.Minion)
            {
                AttackStat = originalCardData.attack;
                HealthStat = originalCardData.health;
                //RARITY HERE
                AttackText.text = AttackStat.ToString();
                HealthText.text = HealthStat.ToString();
                ClassText.text = originalCardData.cardClass.ToString();
            }
            cardImage.sprite = originalCardData.artwork;
        }
	}

	public void AddSelfToDeck()
	{
        deckBuilder.TryAddCard(originalCardData);
	}
}
