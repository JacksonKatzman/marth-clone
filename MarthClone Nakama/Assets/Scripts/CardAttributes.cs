using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DemoGame.Scripts.Utils;
using DemoGame.Scripts.Session;
using DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates;
using DemoGame.Scripts.Gameplay.NetworkCommunication;

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
    public enum TargetAllowance { Friendly, Enemy, Both, Any, NUM_TYPES };
    [Serializable]
    public class CardEffect : ScriptableObject
    {
        public bool targeted;
        //public int modifier;
        private int targetID;
        public int Target
        {
            get { return targetID; }
            set { targetID = value; }
        }
        public virtual void Trigger(Card caster, bool broadcast = true, int modifier = 0)
        {
        }
        public virtual void Trigger(Card caster, PlayableCard target, bool broadcast = true, int modifier = 0)
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
        public override void Trigger(Card caster, bool broadcast = true, int modifier = 0)
        {
            //Draw cards
            for(int a = 0; a < amount; a++)
            {
                GameManager.instance.playHandler.DrawCard();
            }
        }
        public override void Trigger(Card caster, PlayableCard target, bool broadcast = true, int modifier = 0)
        {
            
        }
    }

    [CreateAssetMenu(fileName = "New Card Effect", menuName = "Card Effect/Change Health")]
    [Serializable]
    public class ChangeHealthEffect : CardEffect
    {
        //TODO: CARD EFFECTS NEED NETWORKING
        public int amount;
        public override void Trigger(Card caster, bool broadcast = true, int modifier = 0)
        {
            //Untargeted might change health of all units
        }
        public override void Trigger(Card caster, PlayableCard target, bool broadcast = true, int modifier = 0)
        {
            target.ChangeHealth(amount + modifier);
            if (broadcast)
            {
                MatchMessageSpellCast cast = new MatchMessageSpellCast(caster.cardID, true, target.networkID, modifier);
                MatchCommunicationManager.Instance.SendMatchStateMessage(MatchMessageType.SpellActivated, cast);
            }
        }
    }
}
