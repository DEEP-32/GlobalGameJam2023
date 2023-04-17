using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timeline : MonoBehaviour
{

    private void OnEnable()
    {
        SceneManager.LoadScene(2);
    }
}
