using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Fields

    [SerializeField] private HitLine _lineGoodOne;
    [SerializeField] private HitLine _lineGoodTwo;
    [SerializeField] private HitLine _linePerfect;
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
    
    public void OnHit(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_linePerfect.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYPERFECT, Camera.main.transform));
            }
            else if (_lineGoodOne.Buttons.Count > 0 || _lineGoodTwo.Buttons.Count > 0)
            {
                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
            }
        }
    }
    
    //public void OnGoodHit(InputAction.CallbackContext context)
    //{
    //    if (context.started && _lineThree.Buttons.Count > 0)
    //    {
    //        _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.LINETHREE, Camera.main.transform));
    //    }
    //}

    #endregion

    #endregion
}
