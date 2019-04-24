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
        public bool targeted;
        private int targetID;
        public int Target
        {
            get { return targetID; }
            set { targetID = value; }
        }
        public virtual void Trigger()
        {
        }
        public virtual void Trigger(PlayableCard target)
        {

        }
    }
    //Example of possible implementation of card effects
    [CreateAssetMenu(fileName = "New Card Effect", menuName = "Card Effect/Draw Cards")]
    [Serializable]
    public class DrawCardsEffect : CardEffect
    {
        //TODO: CARD EFFECTS NEED NETWORKING
        public int amount;
        public override void Trigger()
        {
            //Draw cards
            for(int a = 0; a < amount; a++)
            {
                GameManager.instance.playHandler.DrawCard();
            }
        }
        public override void Trigger(PlayableCard target)
        {
            base.Trigger(target);
        }
    }
}
