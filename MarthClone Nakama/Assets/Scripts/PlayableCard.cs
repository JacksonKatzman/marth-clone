using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardInfo;

public class PlayableCard : MonoBehaviour
{
    public Card baseCard;
    public SpriteRenderer cardFrame;
    public TextMeshPro cardName;
    public SpriteRenderer cardArt;
    public TextMeshPro manaCostText, descriptionText;
    public SpriteRenderer raritySprite;
    public int networkID;
    public GameObject particles;
    public int manacost;
    public int attack, health;


    public virtual void SetToBaseCard()
    {

    }

    public virtual void OnCardPlayed()
    {
        foreach(CardEffect effect in baseCard.battlecries)
        {
            effect.Trigger(baseCard);
        }
    }

    public virtual void SetDrawLayer(int a)
    {

    }

    public virtual void RecieveAttack(PlayableCard other)
    {

    }

    public virtual void ChangeHealth(int amount)
    {

    }

    public string ConvertCardNameForDisplay()
    {
        if (baseCard.name.Length < 13)
        {
            int count = 13 - baseCard.name.Length;
            int front = count / 2;
            int back = front + count % 2;
            string temp = "";
            for (int a = 0; a < front; a++)
            {
                temp += "$";
            }
            temp += baseCard.name;
            for (int a = 0; a < back; a++)
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

    public Sprite GetRaritySprite()
    {
        Sprite toRet = null;
        if (baseCard.cardRarity == CardRarity.Common)
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

    public void SetParticlesAwake(bool b)
    {
        particles.SetActive(b);
    }
}
