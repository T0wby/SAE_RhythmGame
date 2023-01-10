using AudioManaging;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortButton : AButton, IPoolable<ShortButton>
{
    private ObjectPool<ShortButton> _pool;
    public override void Deactivate()
    {
        gameObject?.SetActive(false);
    }

    public void Initialize(ObjectPool<ShortButton> pool)
    {
        _pool = pool;
    }

    public override void Reset()
    {
        gameObject?.SetActive(true);
    }
}
