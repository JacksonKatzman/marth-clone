using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CardInfo;

[CreateAssetMenu(fileName = "New Card", menuName = "Card")]
public class Card : ScriptableObject
{
    [Header("Basic Info")]
    public new string name;
    public string description;
    public Sprite artwork;
    public int manaCost, attack, health;
    public int cardID;

    [Header("Card Attributes")]
    public CardClassification cardClass;
    public CardType cardType;
    //public bool[] staticKeyWords = new bool[(int)StaticKeyWord.NUM_TYPES];
    public bool Taunt, Charge, Lifesteal, Rush, Divine_Shield;
    
}
