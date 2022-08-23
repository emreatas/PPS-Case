using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Panel : MonoBehaviour
{

    [SerializeField] private TextMeshPro _bulletCount;

    private void Start()
    {
        _bulletCount.text = "Bullet Count: 0";
    }
    private void OnEnable()
    {
        GameManager.OnBulletCounter += GameManager_OnBulletCounter;
    }

    private void GameManager_OnBulletCounter(int obj)
    {
        _bulletCount.text = "Bullet Count: " + obj;

    }

    private void OnDisable()
    {
        GameManager.OnBulletCounter -= GameManager_OnBulletCounter;
    }
}
