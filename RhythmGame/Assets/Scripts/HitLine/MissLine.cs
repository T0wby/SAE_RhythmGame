using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissLine : MonoBehaviour
{
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;
    [SerializeField] private Spawner _spawner;


    private void OnTriggerEnter(Collider other)
    {
        _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.KEYMISS, Camera.main.transform));
        //other.gameObject.SetActive(false);

        ShortButton tmp = other.GetComponent<ShortButton>();
        //Might need to seperate into each line
        if (tmp is not null)
        {
            _spawner.Pool.ReturnItem(tmp);
        }
    }
}
