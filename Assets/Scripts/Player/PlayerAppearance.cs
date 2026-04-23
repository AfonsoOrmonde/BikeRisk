using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAppearance : MonoBehaviour
{
    [SerializeField]private List<GameObject> Skins;
    [SerializeField] private Color hitColor;
    public Renderer[] renderers;
    private Color[] originalColors;
    void Start()
    {
        Skins[CharacterSelector.Instance.getSkin()].SetActive(true);
        setOriginalColors();
    }

    public void HitFeeback()
    {
        StartCoroutine(FlashDamage());
    }


    private IEnumerator FlashDamage()
    {
        for(int i = 0; i<renderers.Length; i++)
        {
           renderers[i].material.color = hitColor;
        }
        yield return new WaitForSeconds(0.1f);
        for(int i = 0; i<renderers.Length; i++)
        {
           renderers[i].material.color = originalColors[i];
        }
    }

    private void setOriginalColors()
    {
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            originalColors[i] = renderers[i].material.color;
        }
    }
}
