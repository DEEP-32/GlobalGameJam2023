using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class button_manager : MonoBehaviour
{
    [SerializeField] GameObject[] background;
    public Ease SetEase_bg1, SetEase_bg2;
    // Start is called before the first frame update
   


    public void sceneChange()
    {
        background[0].SetActive(false);
        background[0].transform.DOMoveY(1000, 1).SetEase(SetEase_bg1);
        background[1].SetActive(true);
        background[1].transform.DOMoveY(520, 1).SetEase(SetEase_bg2);
    }

    public void startGame(int sceneid)
    {
        SceneManager.LoadScene(sceneid);
    }
    public void Quit()
    {
        Application.Quit(); 
    }

    public void GoBack()
    {
        background[0].SetActive(true);
        background[0].transform.DOMoveY(520, 1).SetEase(SetEase_bg1);
        background[1].SetActive(false);
        background[1].transform.DOMoveY(-520, 1).SetEase(SetEase_bg1);
    

}
    private void OnDisable()
    {
        background[0].SetActive(true);
        background[1].SetActive(false);
    }
}
