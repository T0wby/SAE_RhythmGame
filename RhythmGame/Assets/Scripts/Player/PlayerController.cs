using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields

    [SerializeField] private HitArea _lineOne;
    [SerializeField] private HitArea _lineTwo;
    [SerializeField] private HitArea _lineThree;
    [SerializeField] private HitArea _lineFour;
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

    public void OnLineOne(InputAction.CallbackContext context)
    {
        if (context.started && _lineOne.Buttons.Count > 0)
        {
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.LINEONE, Camera.main.transform));
        }
    }
    
    public void OnLineTwo(InputAction.CallbackContext context)
    {
        if (context.started && _lineTwo.Buttons.Count > 0)
        {
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.LINETWO, Camera.main.transform));
        }
    }
    
    public void OnLineThree(InputAction.CallbackContext context)
    {
        if (context.started && _lineThree.Buttons.Count > 0)
        {
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.LINETHREE, Camera.main.transform));
        }
    }
    
    public void OnLineFour(InputAction.CallbackContext context)
    {
        if (context.started && _lineFour.Buttons.Count > 0)
        {
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.LINEFOUR, Camera.main.transform));
        }
    }

    #endregion

    #endregion
}
