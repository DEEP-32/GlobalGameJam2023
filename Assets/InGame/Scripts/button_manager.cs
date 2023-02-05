using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_manager : MonoBehaviour
{
    [SerializeField] GameObject[] background;
    // Start is called before the first frame update
   


    public void sceneChange()
    {
        background[0].SetActive(false);
        background[1].SetActive(true);
    }

    public void startGame(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
}
