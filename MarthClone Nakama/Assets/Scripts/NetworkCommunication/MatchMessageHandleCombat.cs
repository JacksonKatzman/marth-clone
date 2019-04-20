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
    public class MatchMessageHandleCombat : MatchMessage<MatchMessageHandleCombat>
    {

        #region Fields

        public int myNetID;
        public int enemyNetID;

        public MatchMessageHandleCombat(int myNetID, int enemyNetID)
        {
            this.myNetID = myNetID;
            this.enemyNetID = enemyNetID;
        }




        #endregion


    }

}