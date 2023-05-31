using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Android;
using UnityEngine.UIElements;
using System.Linq;

public class Game
{
    public List<Card> EnemyDeck, PlayerDeck;
    public Game()
    {
        EnemyDeck = GiveDeckNATOCards();
        PlayerDeck = GiveDeckOWDCards();

    }
    List<Card> GiveDeckOWDCards()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(CardManager.OWDCards[Random.Range(0, CardManager.OWDCards.Count)]);
        }
        return list;
    }
    List<Card> GiveDeckNATOCards()
    {
        List<Card> list = new List<Card>();
        for (int i = 0; i < 10; i++)
        {
            list.Add(CardManager.NATOCards[Random.Range(0, CardManager.NATOCards.Count)]);
        }
        return list;
    }
}
public class GameManagerScr : MonoBehaviour
{
    public Game CurrentGame;
    public Transform Line1, Line2, Line3, Line4;
    public Transform EnemyHand, PlayerHand;
    public Transform CP00, CP01, CP02, CP03, CP04, CP10, CP11, CP12, CP13, CP14, CP20, CP21, CP22, CP23, CP24, CP30, CP31, CP32, CP33, CP34;
    public GameObject CardPref;
    int Turn, TurnTime = 30;
    public TextMeshProUGUI TurnTimeTxt;
    public UnityEngine.UI.Button EndTurnBtn;
    public List<CardInfoScr> PlayerHandCards = new List<CardInfoScr>(),
        PlayerFieldCards = new List<CardInfoScr>(),
        EnemyHandCards = new List<CardInfoScr>(),
        EnemyFieldCards = new List<CardInfoScr>();
    CardInfoScr Card_Info;
    public bool IsPlayerTurn { get => Turn%2==0; }

    void Start()
    {
        Turn = 0;
        CurrentGame = new Game();
        GiveHandCards(CurrentGame.EnemyDeck, EnemyHand);
        GiveHandCards(CurrentGame.PlayerDeck, PlayerHand);
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
            cardGO.GetComponent<CardInfoScr>().ShowCardInfo(card, true);
            PlayerHandCards.Add(cardGO.GetComponent<CardInfoScr>());
            cardGO.GetComponent<AttackedCardScr>().enabled = false;
        }
        deck.RemoveAt(0);
    }

    IEnumerator TurnFunc()
    {
        TurnTime = 30;
        TurnTimeTxt.text = TurnTime.ToString();
        foreach (var card in PlayerFieldCards)
        {
            if (card) card.DeHLCard();
        }
        if (IsPlayerTurn)
        {
            foreach (var card in PlayerFieldCards)
            { 
                card.SelfCard.AttackState(true);
                if (card) card.HLCard();
            }
            while (TurnTime-- > 0)
            {
                TurnTimeTxt.text = TurnTime.ToString();
                yield return new WaitForSeconds(1);
            }
        }
        else
        {
            foreach (var card in EnemyFieldCards)
            {
                card.SelfCard.AttackState(true);
            }
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
        int count = 3, place;
        bool f11=false;
        for (int i = 0; i <= count; i++)
            {
                place = Random.Range(1, 5);
                f11 = false;
                if (EnemyHandCards.Count == 0) break;
                if (place == 1 && CP00.transform.childCount == 0) { cards[0].transform.SetParent(CP00.transform); f11 = true; }
                else if (place == 2 && CP01.transform.childCount == 0) { cards[0].transform.SetParent(CP01.transform); f11 = true; }
                else if (place == 3 && CP02.transform.childCount == 0) { cards[0].transform.SetParent(CP02.transform); f11 = true; }
                else if (place == 4 && CP03.transform.childCount == 0) { cards[0].transform.SetParent(CP03.transform); f11 = true; }
                else if (place == 5 && CP04.transform.childCount == 0) { cards[0].transform.SetParent(CP04.transform); f11 = true; }
                if (f11 == true)
                {
                    cards[0].ShowCardInfo(cards[0].SelfCard, false);
                    EnemyFieldCards.Add(cards[0]);
                    EnemyHandCards.Remove(cards[0]);
                }
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
        if (PlayerHandCards.Count < 7) GiveCardToHand(CurrentGame.PlayerDeck, PlayerHand);
    }
    public void CardsFight(CardInfoScr playerCard, CardInfoScr enemyCard)
    {
        playerCard.SelfCard.GetDamage(enemyCard.SelfCard.Attack);
        enemyCard.SelfCard.GetDamage(playerCard.SelfCard.Attack);
        if (!enemyCard.SelfCard.IsAlive) DestroyCard(enemyCard);
        else enemyCard.RefreshData();
        if (!playerCard.SelfCard.IsAlive) DestroyCard(playerCard);
        else playerCard.RefreshData();
    }
    public void DestroyCard(CardInfoScr card)
    {
        card.GetComponent<CardMovementScr>().OnEndDrag(null);
        if (EnemyFieldCards.Exists(x=>x==card)) EnemyFieldCards.Remove(card);
        if (PlayerFieldCards.Exists(x=>x==card)) PlayerFieldCards.Remove(card);
        Destroy(card.gameObject);
    }
}
