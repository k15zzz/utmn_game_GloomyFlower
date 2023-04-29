using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObject : MonoBehaviour
{
    public GameObject[] objectsToToggle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject obj in objectsToToggle)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
