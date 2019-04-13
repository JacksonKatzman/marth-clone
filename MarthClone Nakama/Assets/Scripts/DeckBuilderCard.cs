using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Core;

public class DeckBuilderCard : MonoBehaviour {

	public Text AttackText, HealthText, CostText, NameText;
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
            AttackStat = originalCardData.attack;
            HealthStat = originalCardData.health;
            CostStat = originalCardData.manaCost;
            NameString = originalCardData.name;
            //RARITY HERE
            AttackText.text = AttackStat.ToString();
            HealthText.text = HealthStat.ToString();
            CostText.text = CostStat.ToString();
            NameText.text = NameString;
        }
	}

	public void AddSelfToDeck()
	{
        deckBuilder.TryAddCard(originalCardData);
	}
}
