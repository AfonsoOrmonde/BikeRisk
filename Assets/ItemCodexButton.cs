
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class ItemCodexButton : MonoBehaviour
{

    [SerializeField] ItemData itemAssociated;
    [SerializeField]Image image;

    public UnityEvent ItemCodexButtonClicked;
    // Start is called before the first frame update
    void Start()
    {
       image.sprite = itemAssociated.itemImage; 
    }

    public void Clicked()
    {
        Debug.Log($"Item button has been clicked for : {itemAssociated.itemName}");
        ItemCodexButtonClicked.Invoke();
    }

    public string getItemDescription()
    {
        return itemAssociated.description;
    }
}
