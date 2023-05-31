using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AttackedCardScr : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        CardInfoScr card = eventData.pointerDrag.GetComponent<CardInfoScr>();
        Transform cardForCondition = eventData.pointerDrag.transform;
        string nameOfPlayerLine, nameOfEnemyLine;
        int numberOfLineForPlayer, numberOfLineForEnemy;
        if (card && card.SelfCard.CanAttack && transform.parent.GetComponent<DropPlaceScr>().Type == FieldType.ENEMY_FIELD) 
        {
            Debug.Log(cardForCondition.GetComponent<CardMovementScr>().DefaultParent.parent.name+" "+transform.parent.parent.name);
            nameOfPlayerLine = cardForCondition.GetComponent<CardMovementScr>().DefaultParent.parent.name;
            numberOfLineForPlayer = Convert.ToInt32(nameOfPlayerLine[nameOfPlayerLine.Length-1]);
            nameOfEnemyLine = transform.parent.parent.name;
            numberOfLineForEnemy = Convert.ToInt32(nameOfEnemyLine[nameOfEnemyLine.Length - 1]);
            if (Math.Abs(numberOfLineForPlayer - numberOfLineForEnemy) == 1 || (numberOfLineForPlayer - numberOfLineForEnemy) == 0)
            {
                card.SelfCard.AttackState(false);
                if (card.IsPlayer && card) card.DeHLCard();
                GetComponent<CardMovementScr>().GameManager.CardsFight(card, GetComponent<CardInfoScr>());
            }
        }
    }
}
