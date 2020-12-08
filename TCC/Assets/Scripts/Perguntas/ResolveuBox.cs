using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolveuBox : MonoBehaviour
{


    public GameObject resolveuBox;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btn_fechar()
    {
        resolveuBox.SetActive(false);
        FindObjectOfType<controle>().vel = 5f;
    }
}
