using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CardNameplateScript : MonoBehaviour {

	[SerializeField] TextMeshProUGUI NameplateCost, NameplateName, NameplateNumber;
    //public DeckBuilderCard card;
    public Card card;
	public int amount;
    public DeckBuilder deckBuilder;

	public void SetCard(Card c)
	{
		card = c;
	}

	public void SetAmount(int a)
	{
		amount = a;
	}
	// Use this for initialization
	void Start () {
		
	}

	void OnDestroy()
	{
		//RemoveSelfFromDeck();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpdateNameplate()
    {
        NameplateCost.text = "" + card.manaCost;
        NameplateNumber.text = "" + amount;
        NameplateName.text = card.name;
    }

	public void RemoveCopy()
	{
        deckBuilder.TryRemoveCard(card);
	}
}
