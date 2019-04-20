using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoGame.Scripts.Gameplay.NetworkCommunication;
using DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates;

public class EndTurnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        if(MatchCommunicationManager.Instance != null)
        {
            MatchMessageEndTurn endturn = new MatchMessageEndTurn("", 0.0f);
            MatchCommunicationManager.Instance.SendMatchStateMessage(DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates.MatchMessageType.TurnEnded, endturn);
            //Locally end turn.
            GameManager.instance.EndTurn();
        }
        else
        {
            Debug.Log("No Match Com Manager Present.");
        }
    }
}
