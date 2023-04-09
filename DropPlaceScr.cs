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
        if (Type != FieldType.SELF_FIELD) return;
        CardMovementScr card = eventData.pointerDrag.GetComponent<CardMovementScr>();
        if (card && transform.childCount == 0)
        {
            card.GameManager.PlayerHandCards.Remove(card.GetComponent<CardInfoScr>());
            card.GameManager.PlayerFieldCards.Add(card.GetComponent<CardInfoScr>());
            card.DefaultParent = transform;
        }
    }
}
