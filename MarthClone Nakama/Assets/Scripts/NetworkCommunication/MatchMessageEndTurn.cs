using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates
{
    /// <summary>
    /// Sent at the end of the match.
    /// Contains information about winner and loser of a current match.
    /// </summary>
    public class MatchMessageEndTurn : MatchMessage<MatchMessageEndTurn>
    {

        #region Fields


        /// <summary>
        /// Id of the match.
        /// </summary>
        public string matchId;

        /// <summary>
        /// Duration of the match.
        /// </summary>
        public float time;

        #endregion

        public MatchMessageEndTurn(string matchID, float time)
        {
            this.matchId = matchID;
            this.time = time;
        }
    }

}
