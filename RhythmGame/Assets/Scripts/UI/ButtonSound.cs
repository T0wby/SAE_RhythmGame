using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;
    [SerializeField] private ESoundTypes _hoverType = ESoundTypes.HOVER;
    private GameObject _sfxManager;

    private void Awake()
    {
        _sfxManager = FindObjectOfType<SFXManager>().gameObject;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _requestCollection.Add(EntityAudioRequest.Request(ESources.BUTTON, _hoverType, _sfxManager.transform));
    }
}
