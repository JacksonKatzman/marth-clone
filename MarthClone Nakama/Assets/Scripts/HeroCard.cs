using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HeroCard : PlayableCard
{
    [SerializeField] TextMeshPro healthText;
    // Start is called before the first frame update
    public override void SetToBaseCard()
    {

    }

    public override void OnCardPlayed()
    {

    }

    public override void SetDrawLayer(int a)
    {

    }

    public override void RecieveAttack(PlayableCard other)
    {
        if (health > 0)
        {
            health -= ((PlayableMinion)other).attack;
            healthText.text = health.ToString();
            if (health <= 0)
            {
                //HandleDying();
            }
        }
    }
}
