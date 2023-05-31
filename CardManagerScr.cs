using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string Name;
    public Sprite Logo, Nation;
    public int Attack, Defense, Armor, Cost;
    public bool CanAttack;
    public Card(string name, string nationPath, string logoPath, int attack, int armor, int defense, int cost)
    {
        Name = name;
        Nation = Resources.Load<Sprite>(nationPath);
        Logo = Resources. Load<Sprite>(logoPath);
        Attack = attack; 
        Defense = defense; 
        Armor = armor; 
        Cost = cost;
        CanAttack = false;
    }
    public bool IsAlive
    {
        get
        {
            return Defense > 0;
        }
    }
    public void AttackState(bool can_attack)
    { 
        CanAttack = can_attack; 
    }
    public void GetDamage(int damage)
    {
        if (damage - Armor > 0) 
        {
            Defense -= (damage - Armor);
        }
    }
}
public static class CardManager
{
    public static List<Card> OWDCards = new List<Card>();
    public static List<Card> NATOCards = new List<Card>();
}
public class CardManagerScr : MonoBehaviour
{
    public void Awake()
    {
        CardManager.OWDCards.Add(new Card("Стрелк. пехота", "Sprites/Nations/USSR", "Sprites/Cards/SovietStrelki", 2, 0, 3, 0));
        CardManager.NATOCards.Add(new Card("Стрелк. пехота", "Sprites/Nations/US", "Sprites/Cards/US_Strelki", 2, 0, 3, 0));
    }
}