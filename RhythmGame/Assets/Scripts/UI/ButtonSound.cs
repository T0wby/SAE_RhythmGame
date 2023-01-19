using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;
    [SerializeField] private ESoundTypes _hoverType = ESoundTypes.HOVER;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _requestCollection.Add(EntityAudioRequest.Request(ESources.BUTTON, _hoverType, Camera.main.transform));
    }
}
