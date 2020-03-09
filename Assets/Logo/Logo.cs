using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Logo : MonoBehaviour {
//Мой собственный логотип:
private GameObject cub;
//Список кубов входящих в логотип:
public List<GameObject> logoObject;
    public int iteration_count = 0;
    private int iteration = 0;
	public int load_level_count = 1;

    void Update()
    {
        goNext();
    }

    void FixedUpdate(){
	    //Получаем случайный куб:
	    int rand = (int)Random.Range(0,logoObject.ToArray().Length);
	    //Отключаем/включаем полученный куб:
	    cub = logoObject[rand]; 
	    if(cub.GetComponent<Renderer>().enabled == true){cub.GetComponent<Renderer>().enabled = false;}else{cub.GetComponent<Renderer>().enabled = true;}
    }

    void goNext() {
        StartCoroutine(nexts());
    }

    /// <summary>
    /// Выжидаем паузу:
    /// </summary>
    /// <returns></returns>
    public IEnumerator nexts() {
        for (int i = 0; i < iteration_count; i++) {
            yield return new WaitForSeconds(3);
        }
		Application.LoadLevel(load_level_count);
        yield return null;
    }
}
