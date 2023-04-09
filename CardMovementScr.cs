using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovementScr : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Camera MainCamera;
    Vector3 offset;
    public Transform DefaultParent, DefaultTempCardParent;
    GameObject TempCardGO;
    public bool IsDraggable;
    public Vector3 newPos;
    public GameManagerScr GameManager;
    void Awake()
    {
        MainCamera = Camera.allCameras[0];
        TempCardGO = GameObject.Find("TempCardGO");
        GameManager = FindAnyObjectByType<GameManagerScr>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - MainCamera.ScreenToWorldPoint(eventData.position);
        DefaultParent = transform.parent;
        IsDraggable = (DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_HAND || DefaultParent.GetComponent<DropPlaceScr>().Type == FieldType.SELF_FIELD) && GameManager.IsPlayerTurn;
        if (!IsDraggable) return;
        transform.SetParent(DefaultParent.parent);
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!IsDraggable) return;
        newPos = MainCamera.ScreenToWorldPoint(eventData.position);
        transform.position = newPos + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!IsDraggable) return;
        transform.SetParent(DefaultParent);
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
