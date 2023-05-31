using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public enum FieldType
{
    SELF_HAND,
    SELF_FIELD,
    ENEMY_HAND,
    ENEMY_FIELD
}

public class DropPlaceScr : MonoBehaviour, IDropHandler
{
    public FieldType Type;


    public void OnDrop(PointerEventData eventData)
    {
        Transform[,] CardPlaceMap = { {GameObject.Find("CP00").transform, GameObject.Find("CP01").transform, GameObject.Find("CP02").transform, GameObject.Find("CP03").transform, GameObject.Find("CP04").transform },
        {GameObject.Find("CP10").transform, GameObject.Find("CP11").transform, GameObject.Find("CP12").transform, GameObject.Find("CP13").transform, GameObject.Find("CP14").transform },
        {GameObject.Find("CP20").transform, GameObject.Find("CP21").transform, GameObject.Find("CP22").transform, GameObject.Find("CP23").transform, GameObject.Find("CP24").transform },
        {GameObject.Find("CP30").transform, GameObject.Find("CP31").transform, GameObject.Find("CP32").transform, GameObject.Find("CP33").transform, GameObject.Find("CP34").transform }};
        if (Type != FieldType.SELF_FIELD) return;
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        bool flagIntoRowsCicle = false;
        if (card && transform.childCount == 0 && card.DefaultParent == GameObject.Find("PlayerHand").transform)
        {
            for (int j = 0; j < 5; j++)
            {
                if (transform == CardPlaceMap[3, j])
                {
                    card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
                    card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScr>());
                    card.DefaultParent = transform;
                    break;
                }
            }
        }
        else if (card && transform.childCount == 0)
        {
            for (int i = 1; i < 4; i++)
            {
                if (card.DefaultParent == CardPlaceMap[i, 0] &&
                    ((transform == CardPlaceMap[i - 1, 0] || transform == CardPlaceMap[i - 1, 1]) ||
                    (i < 3 && (transform == CardPlaceMap[i + 1, 0] || transform == CardPlaceMap[i + 1, 1])) ||
                    transform == CardPlaceMap[i, 1]))
                {
                    card.DefaultParent = transform;
                    break;
                }
                else if (card.DefaultParent == CardPlaceMap[i, 4] &&
                    ((transform == CardPlaceMap[i - 1, 4] || transform == CardPlaceMap[i - 1, 3]) ||
                    (i < 3 && (transform == CardPlaceMap[i + 1, 4] || transform == CardPlaceMap[i + 1, 3])) ||
                    transform == CardPlaceMap[i, 3]))
                {
                    card.DefaultParent = transform;
                    break;
                }
                    for (int j = 1; j < 4; j++)
                    {
                        if (card.DefaultParent == CardPlaceMap[i, j] &&
                            (i < 3 && (transform == CardPlaceMap[i + 1, j - 1] || transform == CardPlaceMap[i + 1, j] || transform == CardPlaceMap[i + 1, j + 1])) ||
                            (transform == CardPlaceMap[i, j + 1] || transform == CardPlaceMap[i, j - 1]))
                        {
                            card.DefaultParent = transform;
                            flagIntoRowsCicle = true;
                            break;
                        }
                    }
                    if (flagIntoRowsCicle) break;
                
            }
        }
    }
}