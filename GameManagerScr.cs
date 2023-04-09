using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Android;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeckSlaves, PlayerDeckMasters;
    public Game()
    {
        EnemyDeck = GiveDeckCard();
        PlayerDeckSlaves = GiveDeckCard();
        PlayerDeckMasters = GiveDeckMasterCard();

    }
    List<Card> GiveDeckCard()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(CardManager.AllCards[Random.Range(0, CardManager.AllCards.Count)]);
        }
        return list;
    }
    List<Card> GiveDeckMasterCard()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(CardManager.MasterCards[Random.Range(0, CardManager.MasterCards.Count)]);
        }
        return list;
    }
}
public class GameManagerScr : MonoBehaviour
{
    public Game CurrentGame;
    public Transform EnemyHand, PlayerHand, EnemyField1, EnemyField2, EnemyField3, EnemyField4, EnemyField5, PlayerField1, PlayerField2, PlayerField3, PlayerField4, PlayerField5;
    public GameObject CardPref;
    int Turn, TurnTime = 60;
    public TextMeshProUGUI TurnTimeTxt;
    public Button EndTurnBtn;
    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
        PlayerFieldCards = new List<CardInfoScr>(),
        EnemyHandCards = new List<CardInfoScr>(),
        EnemyFieldCards = new List<CardInfoScr>();
    public bool IsPlayerTurn { get => Turn%2==0; }

    void Start()
    {
        Turn = 0;
        CurrentGame = new Game();
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeckSlaves, PlayerHand);
        StartCoroutine(TurnFunc());
    }
    void GiveHandCards(List<Card> deck, Transform hand)
    {
        for (int i = 0;i < 4;i++) 
        {
            GiveCardToHand(deck, hand);
        }
    }
    void GiveCardToHand(List<Card> deck, Transform hand)
    {
        if(deck.Count == 0) { return; }
        Card card = deck[0];
        GameObject cardGO = Instantiate(CardPref, hand, false);
        if (hand == EnemyHand)
        {
            cardGO.GetComponent<CardInfoScr>().HideCardInfo(card);
            EnemyHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }

        else
        {
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScr>());
        }
        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();
        if (IsPlayerTurn)
        {
            while (TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            while (TurnTime-- > 27)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
            if (EnemyHandCards.Count > 0)
                EnemyTurn(EnemyHandCards);
        }
        ChangeTurn();
    }
    void EnemyTurn(List<CardInfoScr> cards)
    {
        int count = Random.Range(0, cards.Count), place=Random.Range(1, 5);
        for (int i = 0; i < 1; i++) 
        {
            cards[0].ShowCardInfo(cards[0].SelfCard);
            
            if (place == 1) cards[0].transform.SetParent(EnemyField1);
            if (place == 2) cards[0].transform.SetParent(EnemyField2);
            if (place == 3) cards[0].transform.SetParent(EnemyField3);
            if (place == 4) cards[0].transform.SetParent(EnemyField4);
            if (place == 5) cards[0].transform.SetParent(EnemyField5);
            EnemyFieldCards.Add(cards[0]);
            EnemyHandCards.Remove(cards[0]);
        }
    }
    public void ChangeTurn() 
    {
        StopAllCoroutines();
        Turn++;
        EndTurnBtn.interactable = IsPlayerTurn;
        if (IsPlayerTurn)
            GiveNewCards();
        StartCoroutine(TurnFunc());
    }
    void GiveNewCards() 
    {
        if (EnemyHandCards.Count<7) GiveCardToHand(CurrentGame.EnemyDeck, EnemyHand);
        if (PlayerHandCards.Count < 7) GiveCardToHand(CurrentGame.PlayerDeckSlaves, PlayerHand);
    }
}
