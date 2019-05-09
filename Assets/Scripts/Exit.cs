using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    // Start is called before the first frame update
    void OnClick()
    {
        Debug.Log("Button Clicked. ClickHandler.");
        Application.Quit();
    }
}
