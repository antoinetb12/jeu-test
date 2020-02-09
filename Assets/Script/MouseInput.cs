using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseInput : MonoBehaviour
{
    public Text messageText;
    // Start is called before the first frame update
    private Camera cam;
    void Start()
    {
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 WorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
            messageText.text = "click gauche " + WorldPos;
        }
    }
}
