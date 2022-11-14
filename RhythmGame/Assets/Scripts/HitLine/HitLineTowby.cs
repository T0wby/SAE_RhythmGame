using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HitLineTowby : MonoBehaviour
{
    private List<AButton> buttons;
    private AButton tmp;

    //Temp
    [SerializeField] private NotifyEntityRequestCollection _requestCollection;
    [SerializeField] private NotifyMusicRequestCollection _musicRequestCollection;

    // Start is called before the first frame update
    void Start()
    {
        //buttons = new List<AButton>();
        //StartLevelMusic();
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        int count = buttons.Count;
    //        if (count > 0)
    //        {
    //            for (int i = 0; i < count; i++)
    //            {
    //                _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.LINEONE, Camera.main.transform));
    //                buttons[i].gameObject.SetActive(false);
    //            }
    //            buttons.Clear();
    //        }
    //    }
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    tmp = other.gameObject.GetComponent<AButton>();
    //    if (tmp != null)
    //    {
    //        buttons.Add(tmp);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    tmp = other.gameObject.GetComponent<AButton>();
    //    if (tmp != null)
    //    {
    //        buttons.Remove(tmp);
    //    }
    //}

    private void StartLevelMusic()
    {
        Task.Delay(5000);
        _musicRequestCollection.Add(EntityMusicRequest.Request(ESources.LEVEL, EMusicTypes.INGAMEMUSIC, Camera.main.transform));
    }
}
