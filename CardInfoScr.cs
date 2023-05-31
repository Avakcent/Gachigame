using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class CardInfoScr : MonoBehaviour
{
    public Card SelfCard;
    public Image Logo;
    public Image Nation;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Defence;
    public GameObject HL;
    public bool IsPlayer;
    public void HideCardInfo(Card card)
    {
        SelfCard = card;
        Logo.sprite=null;
        Nation.sprite=null;
        Name.text = "";
        Cost.text = "";
        Attack.text = "";
        Armor.text = "";
        Defence.text = "";
        IsPlayer = false;
    }
    public void ShowCardInfo(Card card, bool isPlayer) 
    {
        IsPlayer = isPlayer;
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Nation.sprite = card.Nation;
        Nation.preserveAspect = true;
        Name.text = card.Name;
        Cost.text = card.Cost.ToString();
        Attack.text = card.Attack.ToString();
        Armor.text = card.Armor.ToString();
        Defence.text = card.Defense.ToString();
        RefreshData();

    }
    public void RefreshData()
    {
        Attack.text = SelfCard.Attack.ToString();
        Armor.text = SelfCard.Armor.ToString();
        Defence.text = SelfCard.Defense.ToString();
    }
    public void HLCard()
    {
        HL.SetActive(true);
    }
    public void DeHLCard()
    {
        HL.SetActive(false);
    }

}
