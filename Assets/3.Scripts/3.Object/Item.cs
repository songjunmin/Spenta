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
                    case "�����ǺҲ�":
                        GameManager.instance.ChangeSparkOfKnowledge(1);
                        break;

                    case "������������":
                        GameManager.instance.ChangePieceOfEnlightenment(1); 
                        break;
                }


                Destroy(gameObject);
            }
        }

        
    }
}
