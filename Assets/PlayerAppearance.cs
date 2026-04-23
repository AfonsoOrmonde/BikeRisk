using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField]private List<GameObject> Skins;
    void Start()
    {
        Skins[CharacterSelector.Instance.getSkin()].SetActive(true);
    }
}
