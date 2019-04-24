using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.Examples;
using DemoGame.Scripts.Gameplay.NetworkCommunication;
using DemoGame.Scripts.Gameplay.NetworkCommunication.MatchStates;
using CardInfo;

public class CardDragger : MonoBehaviour
{
    public bool owned = true;
    public bool inHand = true;
    public bool dragable = true;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 originalPos;
    public PlayHandler playHandler;
    Vector3 startPos;
    Vector3 targetPosition;
    float startTime;
    float journeyLength;
    bool moving = false;
    CardType cardType;
    CardEffect effect;

    void Update()
    {
        if (moving)
        {
            if (targetPosition != null && Vector3.Distance(transform.localPosition, targetPosition) > 0.02)
            {
                float distCovered = (Time.time - startTime) * 10.0f;
                float fracJourney = distCovered / journeyLength;
                transform.position = Vector3.Lerp(startPos, targetPosition, fracJourney);
            }
            else if (Vector3.Distance(transform.localPosition, targetPosition) <= 0.02)
            {
                transform.localPosition = targetPosition;
                moving = false;
            }
        }
    }

    void OnMouseDown()
    {
        originalPos = gameObject.transform.position;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        cardType = GetComponent<PlayableCard>().baseCard.cardType;
        if (cardType == CardType.Minion)
        {
            
        }
        else if (cardType == CardType.Spell && inHand)
        {
            effect = GetComponent<PlayableCard>().baseCard.battlecries[0];
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        if (dragable && owned)
        {
            if (cardType == CardType.Minion)
            {
                HandleMinionDrag();
            }
            else if(cardType == CardType.Spell && inHand)
            {
                HandleSpellDrag();
            }
        }
    }

    void HandleMinionDrag()
    {
        if (!inHand)
        {
            //Debug.Log("DRAGGING ON BOARD!");
            GameManager.instance.matchUI.SetRedArrow(originalPos, Input.mousePosition);
        }
        else
        {
            transform.position = GetMouseWorldPos() + mOffset;
            targetPosition = transform.position;
        }
    }

    void HandleSpellDrag()
    {
        if (effect.targeted)
        {
            if (transform.localPosition.z <= 5.0f)
            {
                transform.position = GetMouseWorldPos() + mOffset;
                targetPosition = transform.position;
            }
            else
            {
                //GameManager.instance.matchUI.SetRedTarget(Input.mousePosition);
                GameObject.Find("MatchUI").GetComponent<MatchUI>().SetRedTarget(Input.mousePosition);
            }
        }
        else
        {
            transform.position = GetMouseWorldPos() + mOffset;
            targetPosition = transform.position;
        }
    }

    void OnMouseUp()
    {
        GameObject.Find("MatchUI").GetComponent<MatchUI>().HideRedTarget();
        if (cardType == CardType.Minion)
        {
            HandleMinionMouseUp();
        }
        else if (cardType == CardType.Spell && inHand)
        {
            HandleSpellMouseUp();
        }
    }

    void HandleMinionMouseUp()
    {
        if (!inHand)
        {
            transform.position = originalPos;
            targetPosition = transform.position;
            GameManager.instance.matchUI.HideRedArrow();
            CheckValidTarget();
        }
        else
        {
            if (transform.localPosition.z < 6.0f)
            {
                transform.position = originalPos;
                targetPosition = transform.position;
            }
            else
            {
                //Play the card.
                PlayCardFromHand();
            }
        }
    }

    void HandleSpellMouseUp()
    {
        if (effect.targeted)
        {
            PlayableCard card = CheckValidSpellTarget();
            if(card != null)
            {
                //Handle Spell casting on that target
                Debug.Log("CASTING TARGETED SPELL");
                CastSpellFromHand(card);
            }
            else
            {
                transform.position = originalPos;
                targetPosition = transform.position;
            }
        }
        else
        {
            if (transform.localPosition.z < 5.0f)
            {
                transform.position = originalPos;
                targetPosition = transform.position;
            }
            else
            {
                //Handle spell casting no target
                Debug.Log("CASTING UNTARGETED SPELL");
                CastSpellFromHand(null);
            }
        }
    }

    void CastSpellFromHand(PlayableCard target)
    {
        if (target != null)
            effect.Trigger(target);
        else
            effect.Trigger();
        PlayableCard card = GetComponent<PlayableCard>();
        playHandler.myHandOrganizer.RemoveCard(card);
        Destroy(this.gameObject);
    }

    void PlayCardFromHand()
    {
        inHand = false;
        PlayableCard card = GetComponent<PlayableCard>();
        playHandler.myHandOrganizer.RemoveCard(card);
        transform.SetParent(playHandler.myPlayingFieldOrganizer.gameObject.transform);
        playHandler.myPlayingFieldOrganizer.AttemptToAddCard(card, transform.position);
        card.cardName.GetComponent<SkewTextExample>().RestartSkew();
        GameManager.instance.playHandler.SpendMana(card.manacost);
        SetToReady(false);
        card.OnCardPlayed();
    }

    public void BeginLerp(Vector3 startPos, Vector3 endPos)
    {
        targetPosition = endPos;
        this.startPos = startPos;
        journeyLength = Vector3.Distance(startPos, endPos);
        startTime = Time.time;
        moving = true;
    }

    void CheckValidTarget()
    {
        if(!inHand && dragable && owned)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                PlayableCard card = hit.transform.gameObject.GetComponent<PlayableCard>();
                if(card != null)
                {
                    BeginAttack(card);
                }
            }
        }
    }

    PlayableCard CheckValidSpellTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            PlayableCard card = hit.transform.gameObject.GetComponent<PlayableCard>();
            if (card != null)
            {
                return card;
            }
        }
        return null;
    }

    void BeginAttack(PlayableCard card)
    {
        //LOTS OF PLACEHOLDER CODE HERE
        SetToReady(false);
        PlayableMinion me = GetComponent<PlayableMinion>();
        card.RecieveAttack(me);
        me.RecieveAttack(card);
        MatchMessageHandleCombat combat = new MatchMessageHandleCombat(me.networkID, card.networkID);
        MatchCommunicationManager.Instance.SendMatchStateMessage(MatchMessageType.UnitAttacked, combat);
    }

    void SetToReady(bool b)
    {
        dragable = b;
        GetComponent<PlayableCard>().particles.SetActive(b);
    }
}
