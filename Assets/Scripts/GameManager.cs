using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singelaton

    private static GameManager mInstance;

    public static GameManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = (GameManager)FindObjectOfType(typeof(GameManager));
            }

            if (mInstance == null)
            {
                Debug.LogError("GameManagar instance is null and not found gamemager on to the scene ! Please add Gamemanager to scene.");
            }
            return mInstance;
        }
    }

    #endregion

    private void Awake()
    {
        Cursor.visible = false;
    }

}
