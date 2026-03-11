using UnityEngine;

public class StartMessage : MonoBehaviour
{
    void Start()
    {
        TopMessageUI.Instance.ShowMessage("The aliens are attacking! Get inside the barn, save yourself!");
    }
}