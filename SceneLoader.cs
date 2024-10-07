using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    float waiting_time;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load_Scene());
    }

    IEnumerator Load_Scene()
    {
        yield return new WaitForSeconds(waiting_time);
        int loading_level = PlayerPrefs.GetInt("level", 1);
        SceneManager.LoadScene(loading_level);
    }
}
