using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CardInfo
{
    //[Serializable]
    //public enum StaticKeyWord {Taunt, Lifesteal, Charge, Rush, Divine_Shield, NUM_TYPES};
    [Serializable]
    public enum CardClassification {None, Beast, Mech, Dragon, All, NUM_TYPES};
    [Serializable]
    public enum CardRarity { Common, Rare, Epic, Legendary, None, NUM_TYPES };
    [Serializable]
    public enum CardType {Minion, Spell, NUM_TYPES};
    [Serializable]
    public class CardEffect : ScriptableObject
    {
        public virtual void Trigger()
        {
        }
    }
    //Example of possible implementation of card effects
    [CreateAssetMenu(fileName = "New Card Effect", menuName = "Card Effect/Draw Cards")]
    [Serializable]
    public class DrawCardsEffect : CardEffect
    {
        public int amount;
        public override void Trigger()
        {
            //Draw cards
            for(int a = 0; a < amount; a++)
            {
                GameManager.instance.playHandler.DrawCard();
            }
        }
    }
}
