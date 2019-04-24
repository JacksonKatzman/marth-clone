using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardInfo;

namespace DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates
{
    /// <summary>
    /// Sent at the end of the match.
    /// Contains information about winner and loser of a current match.
    /// </summary>
    public class MatchMessageSpellCast : MatchMessage<MatchMessageSpellCast>
    {

        #region Fields

        public int cardID;
        public bool targeted;
        public int targetID;
        public int modifier;

        public MatchMessageSpellCast(int cardID, bool targeted, int targetID, int modifier)
        {
            this.cardID = cardID;
            this.targeted = targeted;
            this.targetID = targetID;
            this.modifier = modifier;
        }

        #endregion


    }

}