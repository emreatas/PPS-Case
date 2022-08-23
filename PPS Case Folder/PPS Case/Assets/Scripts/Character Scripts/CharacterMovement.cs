using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _spawnBulletPosition;

    [SerializeField] private GameObject _secondGun;
    [SerializeField] private Transform _spawnSecondBulletPosition;


    private bool _doubleGun;



    private Vector3 _mouseWorldPos;
    ObjectPooler _pooler;

    private int _bulletCount = 0;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _pooler = ObjectPooler.Instance;
        _doubleGun = false;
    }





    private void Update()
    {
        HandleRotation();


        if (Input.GetMouseButtonDown(0))
        {
            Vector3 aimDir = (_mouseWorldPos - _spawnBulletPosition.position).normalized;
            _pooler.SpawnFromPool("Bullet", _spawnBulletPosition.position, _spawnBulletPosition.rotation);

            if (_doubleGun)
            {
                _pooler.SpawnFromPool("Bullet", _spawnSecondBulletPosition.position, _spawnSecondBulletPosition.rotation);
                _bulletCount++;
                GameManager.Instance.BulletCounter(_bulletCount);

            }

            _bulletCount++;
            GameManager.Instance.BulletCounter(_bulletCount);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!_doubleGun)
            {
                _doubleGun = true;
                _secondGun.SetActive(true);
            }
            else
            {
                _secondGun.SetActive(false);
                _doubleGun = false;


            }
        }


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 _input = transform.right * x + transform.forward * z;


        _rigidbody.MovePosition(transform.position + _input * Time.deltaTime * _movementSpeed);
    }

    void HandleRotation()
    {
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _layerMask))
        {
            transform.LookAt(new Vector3(_hit.point.x, transform.position.y, _hit.point.z));

            _mouseWorldPos = _hit.point;

        }
    }




}
