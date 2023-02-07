using AudioManaging;
using Scriptable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissLine : MonoBehaviour
{
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Integer _missedNodes;
    private GameObject _sfxManager;

    private void Awake()
    {
        _sfxManager = FindObjectOfType<SFXManager>().gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYMISS, _sfxManager.transform));

        ShortButton tmp = other.GetComponent<ShortButton>();
        if (tmp is not null)
        {
            _spawner.Pool.ReturnItem(tmp);
            _missedNodes++;
            PointManager.Instance.ResetComboCounter();
            PointManager.Instance.ReduceMomentum(0.15f);

            PointManager pointManager = PointManager.Instance;
            pointManager.ProgressCounter.Value = PointManager.Instance.CalcProgress();
        }
    }
}
