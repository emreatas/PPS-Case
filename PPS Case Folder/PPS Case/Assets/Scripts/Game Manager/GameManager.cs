using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    private void Awake()
    {
        Instance = this;
    }


    public static event Action<int> OnBulletCounter;

    public void BulletCounter(int count)
    {
        if (OnBulletCounter != null)
        {
            OnBulletCounter(count);
        }
    }


}
