using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro.Examples;

public class CardDragger : MonoBehaviour
{
    public bool inHand = true;
    private Vector3 mOffset;
    private float mZCoord;
    private Vector3 originalPos;
    public PlayHandler playHandler;
    Vector3 startPos;
    Vector3 targetPosition;
    float startTime;
    float journeyLength;
    bool moving = false;

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
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
    void OnMouseDrag()
    {
        if(inHand)
        {
            transform.position = GetMouseWorldPos() + mOffset;
            targetPosition = transform.position;
        }
    }

    void OnMouseUp()
    {
        if (!inHand)
        {
            transform.position = originalPos;
            targetPosition = transform.position;
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

    void PlayCardFromHand()
    {
        inHand = false;
        PlayableCard card = GetComponent<PlayableCard>();
        playHandler.myHandOrganizer.RemoveCard(card);
        transform.SetParent(playHandler.myPlayingFieldOrganizer.gameObject.transform);
        playHandler.myPlayingFieldOrganizer.AttemptToAddCard(card, transform.position);
        card.cardName.GetComponent<SkewTextExample>().RestartSkew();
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
}
