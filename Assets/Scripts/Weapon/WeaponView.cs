using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponView : MonoBehaviour
{
    public EventHandler<EventArgs> OnShootInputDetected;
    [SerializeField]
    protected ShootType mShootType;
    protected bool mIsPlayerWeapon;
    public GameObject ProjectileObject;

    public void Init(ShootType type, bool isPlayerWeapon, GameObject projectileObject)
    {
        mShootType = type;
        mIsPlayerWeapon = isPlayerWeapon;
        ProjectileObject = projectileObject;
    }

    public virtual void Update()
    {
        InputDetect();
    }

    #region Note
    /* SemiautomaticWeaponView
     * BurstWeaponView
     * ayrı ayrı viewlar oluşturabilinir.
    public virtual void InputDetect()
    {
        switch (mShootType)
        {
            case ShootType.Semiautomatic:
                if (Input.GetMouseButtonDown(0))
                {
                    //mWeapon.Shoot();
                    OnShootDetected.Invoke(this, new EventArgs());
                    //OnShootDetected(this, new EventArgs());
                }
                break;
            case ShootType.Beam:
                break;
            case ShootType.Charged:
                break;
            case ShootType.Burst:
                break;
            default:
                break;
        }
    }

    */
    #endregion

    public abstract void InputDetect();

    public virtual void Shoot()
    {
        Instantiate(ProjectileObject, this.transform.position, Quaternion.identity);
    }
}