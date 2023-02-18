using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;

namespace AudioManaging
{
    public class AudioObject : MonoBehaviour, IPoolable<AudioObject>
    {
        public AudioSource Source => m_source;

        private ObjectPool<AudioObject> m_pool;
        private AudioSource m_source;

        public async void SetCountdown(int _delay)
        {
            await Task.Delay(_delay);

            if (m_pool is not null)
            {
                m_pool.ReturnItem(this);
            }
        }

        public async void SetCountdown(int _delay, Action<bool> action)
        {
            await Task.Delay(_delay);
            if (this == null || this.gameObject == null || !this.gameObject.activeSelf)
                return;

            if (action != null)
                action.Invoke(true);

            if (m_pool != null)
            {
                m_pool.ReturnItem(this);
            }
        }

        public void Initialize(ObjectPool<AudioObject> _pool)
        {
            m_pool = _pool;
            m_source = GetComponent<AudioSource>();
        }

        public void Reset()
        {
            gameObject?.SetActive(true);
        }

        public void Deactivate()
        {
            if (this != null && gameObject != null && gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }

        public void StopSound()
        {
            if (m_source != null)
            {
                m_source.Stop();
            }
        }
    } 
}
