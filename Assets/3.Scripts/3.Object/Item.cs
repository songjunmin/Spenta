using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "Player")
            {
                string itemName = gameObject.name;

                switch (itemName)
                {
                    case "지식의불꽃":
                        GameManager.instance.ChangeSparkOfKnowledge(1);
                        break;

                    case "깨달음의조각":
                        GameManager.instance.ChangePieceOfEnlightenment(1); 
                        break;
                }


                Destroy(gameObject);
            }
        }

        
    }
}
