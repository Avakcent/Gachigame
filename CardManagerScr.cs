using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name, ClassCard;
    public Sprite Logo;
    public int Attack, Defense, Armor, Cost;
    public Card(string name, string classCard, string logoPath, int attack, int armor, int defense, int cost)
    {
        Name = name;
        ClassCard = classCard;
        Logo=Resources. Load<Sprite>(logoPath);
        Attack = attack; 
        Defense = defense; 
        Armor = armor; 
        Cost = cost;
    }
}
public static class CardManager
{
    public static List<Card> AllCards = new List<Card>();
}
public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        CardManager.AllCards.Add(new Card("BILLY", "M" , "Sprites/Cards/Billy", 5, 3, 6, 2));
        CardManager.AllCards.Add(new Card("VAN", "M", "Sprites/Cards/Van", 7, 2, 4, 2));
        CardManager.AllCards.Add(new Card("DANNY", "M", "Sprites/Cards/Danny", 3, 3, 8, 2));
        CardManager.AllCards.Add(new Card("STEVE", "M", "Sprites/Cards/Steve", 8, 2, 3, 2));
        CardManager.AllCards.Add(new Card("BRAD", "M", "Sprites/Cards/Brad", 4, 4, 5, 2));
    }
}