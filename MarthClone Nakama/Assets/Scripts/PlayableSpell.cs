using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableSpell : PlayableCard
{
    // Start is called before the first frame update
    void Start()
    {
        SetToBaseCard();
    }
    public override void SetToBaseCard()
    {
        cardArt.sprite = baseCard.artwork;
        manacost = baseCard.manaCost;
        manaCostText.text = manacost.ToString();

        descriptionText.text = baseCard.description;
        cardName.text = ConvertCardNameForDisplay();
        raritySprite.sprite = GetRaritySprite();
    }

    public override void OnCardPlayed()
    {
        base.OnCardPlayed();
    }
    public override void SetDrawLayer(int a)
    {
        a *= 2;
        cardArt.sortingOrder = a;
        cardFrame.sortingOrder = a + 1;
        cardName.sortingOrder = a+1;
        manaCostText.sortingOrder = a + 1;
        descriptionText.sortingOrder = a + 1;
        raritySprite.sortingOrder = a + 1;
    }

    public override void RecieveAttack(PlayableCard other)
    {

    }
}
