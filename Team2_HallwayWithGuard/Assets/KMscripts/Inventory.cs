using UnityEngine;

public class Inventory : MonoBehaviour
{



    bool hasKey = false;
    public GameObject winText;
    


    public void AddKey()
    {

        hasKey = true;
        //Debug.Log(" key pickup in inv ");


    }

    // check if player has key, and either open door and win or do nothing 
    public void UseKey()
    {
        if (hasKey)
        {

            winText.SetActive(true);

            //Debug.Log("opened!!");

        }
        else
        {
            Debug.Log("locked!");
        }
    }




    void Start()
    {
        winText.SetActive(false);


    }

    void Update()
    {
        
    }
}
