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
    public TextMeshProUGUI Name;
    public TextMeshProUGUI ClassCard;
    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI Defence;
    public void ShowCardInfo(Card card) 
    {
        SelfCard = card;
        Logo.sprite = card.Logo;
        Logo.preserveAspect = true;
        Name.text = card.Name;
        ClassCard.text = card.ClassCard;
        Cost.text = card.Cost.ToString();
        Attack.text = card.Attack.ToString();
        Armor.text = card.Armor.ToString();
        Defence.text = card.Defense.ToString();
    }
    private void Start()
    {
        ShowCardInfo(CardManager.AllCards[transform.GetSiblingIndex()]);
    }
}
