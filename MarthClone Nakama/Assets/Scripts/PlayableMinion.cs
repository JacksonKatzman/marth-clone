using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardInfo;

public class PlayableMinion : PlayableCard
{
    [SerializeField] SpriteRenderer cardArt;
    [SerializeField] TextMeshPro attackText, healthText, classText, descriptionText;

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

    public void SetToBaseCard()
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

    string ConvertCardNameForDisplay()
    {
        if(baseCard.name.Length < 12)
        {
            int count = 12 - baseCard.name.Length;
            int front = count / 2;
            int back = front + count % 2;
            string temp = "";
            for(int a = 0; a < front; a++)
            {
                temp += "$";
            }
            temp += baseCard.name;
            for(int a = 0; a < back; a++)
            {
                temp += "$";
            }
            return temp;
        }
        else
        {
            return baseCard.name;
        }
    }

    Sprite GetRaritySprite()
    {
        Sprite toRet = null;
        if(baseCard.cardRarity == CardRarity.Common)
        {
            toRet = (Sprite)Resources.Load<Sprite>("Rarity_Common");
        }
        if (baseCard.cardRarity == CardRarity.Rare)
        {
            toRet = (Sprite)Resources.Load<Sprite>("Rarity_Rare");
        }
        if (baseCard.cardRarity == CardRarity.Epic)
        {
            toRet = (Sprite)Resources.Load<Sprite>("Rarity_Epic");
        }
        if (baseCard.cardRarity == CardRarity.Legendary)
        {
            toRet = (Sprite)Resources.Load<Sprite>("Rarity_Legendary");
        }
        return toRet;
    }
}
