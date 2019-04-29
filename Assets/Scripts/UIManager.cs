using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Singelaton

    private static UIManager mInstance;

    public static UIManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = (UIManager)FindObjectOfType(typeof(UIManager));
            }

            if (mInstance == null)
            {
                Debug.LogError("UIManager instance is null and not found UIManager on to the scene ! Please add UIManager to scene.");
            }
            return mInstance;
        }
    }

    #endregion

    public WeaponUIView UIView;
}
