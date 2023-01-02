using System.Collections.Generic;
using Data;
using UnityEngine;

public class SpinWeaponSpawner : WeaponSpawner
{
    List<GameObject> _spinWeapons = new List<GameObject>();
    private float _circleR = 4;
    private float _deg = 0;


    void Awake()
    {
        _weaponID = 3;
    }

    void Update()
    {
        SetSpinStat(_level);

        _deg += Time.deltaTime * _movSpeed;
        if (_deg < 360)
        {
            for (int i = 0; i < _createPerCount; i++)
            {
                var rad = Mathf.Deg2Rad * (_deg + (i * (360 / _createPerCount)));
                var x = _circleR * Mathf.Sin(rad);
                var y = _circleR * Mathf.Cos(rad);
                _spinWeapons[i].transform.position = transform.position + new Vector3(x, y);
                _spinWeapons[i].transform.rotation = Quaternion.Euler(0, 0, _deg * Mathf.Max(5f, _movSpeed/25));
            }
        }
        else
        {
            _deg = 0;
        }
    }

    //Todo
    protected void SetWeaponStat(GameObject weapon)
    {
        SpinWeapon spin = weapon.GetComponent<SpinWeapon>();
        spin.damage = _damage;
    }

    void SetSpinStat(int level)
    {
        base.SetWeaponStat();

        while(_spinWeapons.Count > _createPerCount)
        {
            GameObject spin = _spinWeapons[_spinWeapons.Count-1];
            Managers.Resource.Destroy(spin);
            _spinWeapons.RemoveAt(_spinWeapons.Count - 1);
        }

        while(_spinWeapons.Count < _createPerCount)
        {
            GameObject spinWeapon = Managers.Game.Spawn(Define.WorldObject.Weapon, "Weapon/Spin");
            SetWeaponStat(spinWeapon);
            _spinWeapons.Add(spinWeapon);
        }
    }
}