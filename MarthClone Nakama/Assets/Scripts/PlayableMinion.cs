using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardInfo;

public class PlayableMinion : PlayableCard
{
    
    [SerializeField] TextMeshPro attackText, healthText, classText;

    int attack, health;

    void Start()
    {
        SetToBaseCard();
    }
    public PlayableMinion (Card c)
    {
        baseCard = c;
        SetToBaseCard();
    }

    public override void SetToBaseCard()
    {
        cardArt.sprite = baseCard.artwork;
        manacost = baseCard.manaCost;
        manaCostText.text = manacost.ToString();
        attack = baseCard.attack;
        health = baseCard.health;
        attackText.text = baseCard.attack.ToString();
        healthText.text = baseCard.health.ToString();
        classText.text = baseCard.cardClass.ToString();
        descriptionText.text = baseCard.description;
        cardName.text = ConvertCardNameForDisplay();
        raritySprite.sprite = GetRaritySprite();
    }

    
}
