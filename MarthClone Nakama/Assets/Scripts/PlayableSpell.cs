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
}
