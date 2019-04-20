using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CardInfo;

public class PlayableMinion : PlayableCard
{
    
    [SerializeField] TextMeshPro attackText, healthText, classText;

    public int attack, health;

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
        if(classText.text == "None")
        {
            classText.text = "";
        }
    }

    public override void OnCardPlayed()
    {
        base.OnCardPlayed();
    }

    public override void SetDrawLayer(int a)
    {
        a *= 2;
        cardArt.sortingOrder = a;
        cardFrame.sortingOrder = a+1;
        cardName.sortingOrder = a + 1;
        attackText.sortingOrder = a + 1;
        healthText.sortingOrder = a + 1;
        manaCostText.sortingOrder = a + 1;
        classText.sortingOrder = a + 1;
        descriptionText.sortingOrder = a + 1;
        raritySprite.sortingOrder = a + 1;
    }

    public override void RecieveAttack(PlayableCard other)
    {
        if(health > 0)
        {
            health -= ((PlayableMinion)other).attack;
            UpdateVisibleCard();
            if(health <= 0)
            {
                HandleDying();
            }
        }
    }

    public void UpdateVisibleCard()
    {
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
    }

    public void HandleDying()
    {
        Destroy(this.gameObject);
    }

}
