using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUIView : MonoBehaviour
{
    public Text MagazineText;

    public void UpdateMagazineText(int value)
    {
        Debug.LogAssertion("Test = " + value);
        MagazineText.text = "Magazine : " + value.ToString();
    }
}
