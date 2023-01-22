using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVisualization : MonoBehaviour
{
    private float[] _samples = new float[512];
    private float[] _freqGroups = new float[8];
    private AudioSource _audioSource;
    private MusicManager _musicManager;
    private bool _gotMusic = false;

    public float[] FreqGroups { get => _freqGroups; }

    void Awake()
    {
        _musicManager = FindObjectOfType<MusicManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gotMusic)
        {
            GetSpectrum();
            CreateFreqGroups();
        }
    }

    public void GetMusic()
    {
        _audioSource = _musicManager.LastCreatedMusicObject.Source as AudioSource;
        if (_audioSource == null)
            return;

        _gotMusic = true;
    }

    private void GetSpectrum()
    {
        _audioSource.GetSpectrumData(_samples, 0,FFTWindow.Blackman);
    }

    private void CreateFreqGroups()
    {
        int count = 0;
        float average = 0;

        for (int i = 0; i < _freqGroups.Length; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if (i == 7)
                sampleCount += 2;
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count - 1);
                count++;
            }
            average /= count;

            _freqGroups[i] = average * 10;
        }
    }
}
