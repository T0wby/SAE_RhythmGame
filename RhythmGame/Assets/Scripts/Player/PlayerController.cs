using AudioManaging;
using Scriptable;
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

    [Header("Counter")]
    [SerializeField] private Integer _goodHitCounter;
    [SerializeField] private Integer _perfectHitCounter;

    [Header("Music")]
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
            DoActionOnLine(_perfectLineOne, _goodBeforeLineOne, _goodAfterLineOne);
        }
    }

    public void OnHitTwo(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DoActionOnLine(_perfectLineTwo, _goodBeforeLineTwo, _goodAfterLineTwo);
        }
    }

    public void OnHitThree(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DoActionOnLine(_perfectLineThree, _goodBeforeLineThree, _goodAfterLineThree);
        }
    }

    public void OnHitFour(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DoActionOnLine(_perfectLineFour, _goodBeforeLineFour, _goodAfterLineFour);
        }
    }



    #endregion

    #region Function

    private void DoActionOnLine(HitArea perfect, HitArea goodOne, HitArea goodTwo)
    {
        if (perfect.Buttons.Count > 0)
        {
            PointManager pointManager = PointManager.Instance;
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYPERFECT, Camera.main.transform));
            perfect.ResetArea();
            goodOne.ResetArea();
            goodTwo.ResetArea();
            _perfectHitCounter++;
            pointManager.ComboCounter++;
            pointManager.MomentumCounter.Value += 0.1f;
            pointManager.CalculateScore(pointManager.PerfectNodePoints);
        }
        else if (goodOne.Buttons.Count > 0)
        {
            PointManager pointManager = PointManager.Instance;
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
            goodOne.ResetArea();
            goodTwo.ResetArea();
            _goodHitCounter++;
            pointManager.ComboCounter++;
            pointManager.MomentumCounter.Value += 0.05f;
            pointManager.CalculateScore(pointManager.GoodNodePoints);
        }
        else if (goodTwo.Buttons.Count > 0)
        {
            PointManager pointManager = PointManager.Instance;
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYGOOD, Camera.main.transform));
            goodTwo.ResetArea();
            _goodHitCounter++;
            pointManager.ComboCounter++;
            pointManager.MomentumCounter.Value += 0.05f;
            pointManager.CalculateScore(pointManager.GoodNodePoints);
        }
    }

    #endregion

    #endregion
}
