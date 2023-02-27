using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    [SerializeField] private float _vfxTime = 0.3f;
    [SerializeField] private VisualEffect _electricityOne;
    [SerializeField] private VisualEffect _electricityTwo;
    [SerializeField] private VisualEffect _electricityThree;
    [SerializeField] private VisualEffect _electricityFour;
    [SerializeField] private VisualEffect _electricityMomentum;

    private void Start()
    {
        //if (_electricityOne == null)
        //{
        //    GameObject gameObject = GameObject.FindWithTag("ElectOne");
        //    _electricityOne = gameObject.GetComponent<VisualEffect>();
        //}
        //if (_electricityTwo == null)
        //{
        //    GameObject gameObject = GameObject.FindWithTag("ElectTwo");
        //    _electricityTwo = gameObject.GetComponent<VisualEffect>();
        //}
        //if (_electricityThree == null)
        //{
        //    GameObject gameObject = GameObject.FindWithTag("ElectThree");
        //    _electricityThree = gameObject.GetComponent<VisualEffect>();
        //}
        //if (_electricityFour == null)
        //{
        //    GameObject gameObject = GameObject.FindWithTag("ElectFour");
        //    _electricityFour = gameObject.GetComponent<VisualEffect>();
        //}
        //if (_electricityMomentum == null)
        //{
        //    GameObject gameObject = GameObject.FindWithTag("ElectMom");
        //    _electricityMomentum = gameObject.GetComponent<VisualEffect>();
        //}
    }

    public void StartEffect(int lineNumber)
    {
        switch (lineNumber)
        {
            case 1:
                _electricityOne = CheckForNullReference(_electricityOne, "ElectOne");
                StartCoroutine(SpawnElecticity(_electricityOne));
                break;
            case 2:
                _electricityTwo = CheckForNullReference(_electricityTwo, "ElectTwo");
                StartCoroutine(SpawnElecticity(_electricityTwo));
                break;
            case 3:
                _electricityThree = CheckForNullReference(_electricityThree, "ElectThree");
                StartCoroutine(SpawnElecticity(_electricityThree));
                break;
            case 4:
                _electricityFour = CheckForNullReference(_electricityFour, "ElectFour");
                StartCoroutine(SpawnElecticity(_electricityFour));
                break;
            default:
                break;
        }
    }

    public void ContoleMomentum(bool isOn)
    {
        _electricityMomentum = CheckForNullReference(_electricityMomentum, "ElectMom");

        if (isOn)
        {
            if (_electricityMomentum.GetFloat("SpawnRate") == 200f)
                return;
            _electricityMomentum.SetFloat("SpawnRate", 200f);
        }
        else
        {
            if (_electricityMomentum.GetFloat("SpawnRate") == 0f)
                return;
            _electricityMomentum.SetFloat("SpawnRate", 0f);
        }
    }

    private VisualEffect CheckForNullReference(VisualEffect visualEffect, string tag)
    {
        if (visualEffect == null)
        {
            GameObject gameObject = GameObject.FindWithTag(tag);
            visualEffect = gameObject.GetComponent<VisualEffect>();
            return visualEffect;
        }
        return visualEffect;
    }

    private IEnumerator SpawnElecticity(VisualEffect visualEffect)
    {
        visualEffect.SetFloat("SpawnRate", 100f);
        yield return new WaitForSecondsRealtime(_vfxTime);
        visualEffect.SetFloat("SpawnRate", 0f);
    }
}
