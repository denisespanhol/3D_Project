using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UIGunUpdater> uiGunUpdaters;

    [SerializeField] private GunBase firstGun;
    [SerializeField] private GunBase secondGun;

    public Transform gunPosition;

    private GunBase _currentFirstGun;
    private GunBase _currentSecondGun;
    private GunBase _gunActive;


    private void Update()
    {
        SwipeGuns();
    }

    protected override void Init()
    {
        base.Init();

        CreateGun();
        _currentSecondGun.gameObject.SetActive(false);
        _gunActive = _currentFirstGun;

        inputs.Gameplay.Shoot.performed += cts => StartShoot();
        inputs.Gameplay.Shoot.performed += cts => CancelShoot();
    }

    private void SwipeGuns()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentFirstGun.gameObject.SetActive(true);
            _currentSecondGun.gameObject.SetActive(false);
            _gunActive = _currentFirstGun;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentFirstGun.gameObject.SetActive(false);
            _currentSecondGun.gameObject.SetActive(true);
            _gunActive = _currentSecondGun;
        }
    }

    private void CreateGun()
    {
        _currentFirstGun = Instantiate(firstGun, gunPosition);

        _currentFirstGun.transform.localPosition = _currentFirstGun.transform.localEulerAngles = Vector3.zero;

        _currentSecondGun = Instantiate(secondGun, gunPosition);

        _currentSecondGun.transform.localPosition = _currentSecondGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _gunActive.StartShoot();
        Debug.Log("Start Shoot");
    }

    private void CancelShoot()
    {
        _gunActive.StopShoot();
        Debug.Log("Cancel Shoot");
    }
}
