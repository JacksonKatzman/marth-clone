using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayableCard : MonoBehaviour
{
    public Card baseCard;
    public TextMeshPro cardName;
    public TextMeshPro manaCostText;
    public SpriteRenderer raritySprite;
    protected int manacost;
}
