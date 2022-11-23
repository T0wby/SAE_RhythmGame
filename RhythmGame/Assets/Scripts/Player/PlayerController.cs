using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields

    [Header("PerfectHitLine")]
    [SerializeField] private HitArea _perfectLineOne;
    [SerializeField] private HitArea _perfectLineTwo;
    [SerializeField] private HitArea _perfectLineThree;
    [SerializeField] private HitArea _perfectLineFour;

    [Header("GoodBeforeHitLine")]
    [SerializeField] private HitArea _goodBeforeLineOne;
    [SerializeField] private HitArea _goodBeforeLineTwo;
    [SerializeField] private HitArea _goodBeforeLineThree;
    [SerializeField] private HitArea _goodBeforeLineFour;
    
    [Header("GoodAfterHitLine")]
    [SerializeField] private HitArea _goodAfterLineOne;
    [SerializeField] private HitArea _goodAfterLineTwo;
    [SerializeField] private HitArea _goodAfterLineThree;
    [SerializeField] private HitArea _goodAfterLineFour;


    [SerializeField] private NotifyEntityRequestCollection _requestCollection;

    #endregion

    #region Properties
    public Controller Inputs { get; set; } = null;
    #endregion

    #region Methods

    #region Unity

    private void Awake()
    {
        Inputs ??= new Controller();
    }

    private void OnEnable()
    {
        Inputs?.Enable();
    }

    private void OnDisable()
    {
        Inputs?.Disable();
    }

    #endregion

    #region Callbacks
    
    public void OnHitOne(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_perfectLineOne.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYPERFECT, Camera.main.transform));
                _perfectLineOne.ResetArea();
            }
            else if (_goodBeforeLineOne.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodBeforeLineOne.ResetArea();
            }
            else if (_goodAfterLineOne.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodAfterLineOne.ResetArea();
            }
        }
    }

    public void OnHitTwo(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_perfectLineTwo.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYPERFECT, Camera.main.transform));
                _perfectLineTwo.ResetArea();
            }
            else if (_goodBeforeLineTwo.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodBeforeLineTwo.ResetArea();
            }
            else if (_goodAfterLineTwo.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodAfterLineTwo.ResetArea();
            }
        }
    }

    public void OnHitThree(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_perfectLineThree.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYPERFECT, Camera.main.transform));
                _perfectLineThree.ResetArea();
            }
            else if (_goodBeforeLineThree.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodBeforeLineThree.ResetArea();
            }
            else if (_goodAfterLineThree.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodAfterLineThree.ResetArea();
            }
        }
    }

    public void OnHitFour(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_perfectLineFour.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYPERFECT, Camera.main.transform));
                _perfectLineFour.ResetArea();
            }
            else if (_goodBeforeLineFour.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodBeforeLineFour.ResetArea();
            }
            else if (_goodAfterLineFour.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
                _goodAfterLineFour.ResetArea();
            }
        }
    }

    #endregion

    #endregion
}
