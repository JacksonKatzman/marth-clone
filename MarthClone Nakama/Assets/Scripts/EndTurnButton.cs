using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DemoGame.Scripts.Gameplay.NetworkCommunication;

public class EndTurnButton : MonoBehaviour
{
    // Start is called before the first frame update
    void OnMouseDown()
    {
        if(MatchCommunicationManager.Instance != null)
        {
            //MatchCommunicationManager.Instance.SendMatchStateMessage(DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates.MatchMessageType.TurnEnded)
        }
        else
        {
            Debug.Log("No Match Com Manager Present.");
        }
    }
}
