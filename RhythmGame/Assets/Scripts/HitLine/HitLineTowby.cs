using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class HitLineTowby : MonoBehaviour
{
    private List<ButtonTowby> buttons;
    private ButtonTowby tmp;

    // Start is called before the first frame update
    void Start()
    {
        buttons = new List<ButtonTowby>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (buttons.Count > 0)
            {
                buttons[0].gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        tmp = other.gameObject.GetComponent<ButtonTowby>();
        if (tmp != null)
        {
            buttons.Add(tmp);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tmp = other.gameObject.GetComponent<ButtonTowby>();
        if (tmp != null)
        {
            buttons.Remove(tmp);
        }
    }
}
