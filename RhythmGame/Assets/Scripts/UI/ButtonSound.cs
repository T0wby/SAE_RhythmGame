using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _requestCollection.Add(EntityAudioRequest.Request(ESources.BUTTON, ESoundTypes.HOVER, Camera.main.transform));
    }
}
