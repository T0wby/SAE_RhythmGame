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

    public void StartEffect(int lineNumber)
    {
        switch (lineNumber)
        {
            case 1:
                StartCoroutine(SpawnElecticity(_electricityOne));
                break;
            case 2:
                StartCoroutine(SpawnElecticity(_electricityTwo));
                break;
            case 3:
                StartCoroutine(SpawnElecticity(_electricityThree));
                break;
            case 4:
                StartCoroutine(SpawnElecticity(_electricityFour));
                break;
            default:
                break;
        }
    }

    private IEnumerator SpawnElecticity(VisualEffect visualEffect)
    {
        visualEffect.SetFloat("SpawnRate", 45f);
        yield return new WaitForSecondsRealtime(_vfxTime);
        visualEffect.SetFloat("SpawnRate", 0f);
    }
}
