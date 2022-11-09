using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitLineTowby : MonoBehaviour
{
    private List<AButton> buttons;
    private AButton tmp;

    //Temp
    [SerializeField] private NotifyEntityRequestCollection _requestCollection; 

    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<AButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (buttons.Count > 0)
            {
                Debug.Log("Pressed");
                buttons[0].gameObject.SetActive(false);
                buttons.Remove(buttons[0]);
            }
            _requestCollection.Add(EntityAudioRequest.Request(ESources.KEY, ESoundTypes.ACTION, transform));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tmp = other.gameObject.GetComponent<AButton>();
        if (tmp != null)
        {
            buttons.Add(tmp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tmp = other.gameObject.GetComponent<AButton>();
        if (tmp != null)
        {
            buttons.Remove(tmp);
        }
    }
}
