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
    public class MatchMessageCardPlayed : MatchMessage<MatchMessageCardPlayed>
    {

        #region Fields

        public int cardID;
        public int cardType;
        public int absPos;
        public int netID;

        public MatchMessageCardPlayed(int cardID, int cardType, int absPos, int networkID)
        {
            this.cardID = cardID;
            this.cardType = cardType;
            this.absPos = absPos;
            this.netID = networkID;
        }

        #endregion


    }

}
