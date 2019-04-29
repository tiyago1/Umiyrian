using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class WeaponModel
{
    private int magazineSize;

    public EventHandler<EventArgs> OnMagazineSizeChanged;

    public Guid ID;
    public string Name;
    public ShootType ShootType;
    public int MagazineSize
    {
        get
        {
            return magazineSize;
        }
        set
        {
            if (magazineSize != value)
            {
                magazineSize = value;
                if (OnMagazineSizeChanged != null)
                {
                    OnMagazineSizeChanged(this, new EventArgs());
                }
            }
        }
    }
    public int MaxAmmo;
    public int Damage;
    public float Range;
    public float FireRate;
    public float ReloadTime;
    public int Force;
    public int Spread;
    public Sprite View;
    public Animator Animator;
    public GameObject ProjectileObject;
}