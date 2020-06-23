using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    Semiautomatic, //  Default olarak projectile atma
    Beam, // Basılı tutarak ışın atma
    Charged, // Basılı tutup bırakarak atma
    Burst,
    Automatic
}
[CreateAssetMenu(fileName = "Weapon",menuName = "Create Weapon", order = 51)]
public class WeaponModel : ScriptableObject
{
    [SerializeField]
    private Guid ID = new Guid();
    [SerializeField]
    private string Name;
    [SerializeField]
    private ShootType ShootType;
    [SerializeField]
    private int MagazineSize;
    [SerializeField]
    private int MaxAmmo;
    [SerializeField]
    private int Damage;
    [SerializeField]
    private float Range;
    [SerializeField]
    private float FireRate;
    [SerializeField]
    private float ReloadTime;
    [SerializeField]
    private int Force;
    [SerializeField]
    private int Spread;
    [SerializeField]
    private Sprite View;
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private GameObject ProjectileObject;

    [Header("Events")]
    public EventHandler<EventArgs> OnMagazineSizeChanged;

    public WeaponModel GetWeaponDataInstance()
    {
        WeaponModel holder = new WeaponModel()
        {
            ID               = this.ID,
            Name             = this.Name,
            ShootType        = this.ShootType,
            MagazineSize     = this.MagazineSize,
            MaxAmmo          = this.MaxAmmo,
            Damage           = this.Damage,
            Range            = this.Range,
            FireRate         = this.FireRate,
            ReloadTime       = this.ReloadTime,
            Force            = this.Force,
            Spread           = this.Spread,
            View             = this.View,
            Animator         = this.Animator,
            ProjectileObject = this.ProjectileObject
        };
        
        return holder;
    }
}