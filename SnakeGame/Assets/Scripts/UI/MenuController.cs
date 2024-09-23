using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;
    public static MenuController Intance;
    // Start is called before the first frame update
    void Start()
    {
        menuCanvas.SetActive(false);
        if (Intance != null) 
        {
            Destroy(this.gameObject);
            return;
        }
        Intance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuCanvas.SetActive(!menuCanvas.activeSelf);
        }
    }
}
