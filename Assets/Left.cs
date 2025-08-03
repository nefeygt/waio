using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class Left : MonoBehaviour
{
    private Button b;
    // Start is called before the first frame update
    void Start()
    {
        b = gameObject.GetComponent<Button>();
        b.onClick.AddListener(SimulateKeyPress);
    }

    void SimulateKeyPress()
    {
        // Simulate 'A' key press
        Input.(KeyCode.A);
    }
}
