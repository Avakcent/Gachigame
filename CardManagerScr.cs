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
    public static List<Card> MasterCards = new List<Card>();
}
public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        CardManager.MasterCards.Add(new Card("BILLY", "M" , "Sprites/Cards/Billy", 5, 3, 6, 2));
        CardManager.MasterCards.Add(new Card("VAN", "M", "Sprites/Cards/Van", 7, 2, 4, 2));
        CardManager.MasterCards.Add(new Card("DANNY", "M", "Sprites/Cards/Danny", 3, 4, 9, 3));
        CardManager.MasterCards.Add(new Card("STEVE", "M", "Sprites/Cards/Steve", 9, 3, 3, 3));
        CardManager.MasterCards.Add(new Card("BRAD", "M", "Sprites/Cards/Brad", 4, 4, 5, 2));
        CardManager.AllCards.Add(new Card("MARK", "S", "Sprites/Cards/Mark", 1, 1, 3, 1));
        CardManager.AllCards.Add(new Card("CAMERON", "S", "Sprites/Cards/Cameron", 3, 0, 2, 1));
        CardManager.AllCards.Add(new Card("STEVIE", "S", "Sprites/Cards/Stevie", 2, 0, 2, 0));
        CardManager.AllCards.Add(new Card("COLTON", "S", "Sprites/Cards/Colton", 2, 1, 1, 1));
        CardManager.AllCards.Add(new Card("ANTHONY", "S", "Sprites/Cards/Anthony", 1, 0, 3, 0));
        CardManager.AllCards.Add(new Card("BO", "S", "Sprites/Cards/Bo", 3, 1, 1, 1));
        CardManager.AllCards.Add(new Card("NICK", "S", "Sprites/Cards/Nick", 0, 2, 3, 1));
        CardManager.AllCards.Add(new Card("THUNDER", "S", "Sprites/Cards/Thunder", 1, 0, 5, 1));
        CardManager.AllCards.Add(new Card("DANIEL", "S", "Sprites/Cards/Daniel", 2, 1, 1, 0));
        CardManager.AllCards.Add(new Card("LARRY", "S", "Sprites/Cards/Larry", 3, 0, 1, 0));
    }
}