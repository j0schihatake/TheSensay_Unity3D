using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnityADSJ0schi : MonoBehaviour {
    //Скрипт для работы с Unity3d ADS

    public static int opend = 0;
    public GameObject adMob = null;

	public string zoneId = "1074342";

	//Ссылки на кнопку:
	public Button button = null;
	public GameObject buttonObject = null;
	public GameObject smalIconObject = null;

	void Start () {
        //проверяем поддерживается ли этим устройством
		if (UnityEngine.Advertisements.Advertisement.isSupported)
        {
			//button = GetComponent<Button>();​
			if (button) button.onClick.AddListener(delegate () { schoowADS(); });
			//Сраный bool может лишить денег ВНИМАТЕЛЬНО! Я его пока просто отключил.
			UnityEngine.Advertisements.Advertisement.Initialize(zoneId);
        }
        else
        {
            Debug.Log("не работает на этом устройстве");
        }
    }

	void Update(){
		//Когда реклама не готова скрываем ее
		onOfButton ();
	}

	//Теперь убираю кнопку если реклама не готова:
	private void onOfButton(){
		if (UnityEngine.Advertisements.Advertisement.IsReady ()) {
			buttonObject.SetActive (true);
			smalIconObject.SetActive (false);
		} else {
			smalIconObject.SetActive (true);
			buttonObject.SetActive (false);
		}
	}

	public void schoowADS() {
		//если готова к следующему показу
		if (UnityEngine.Advertisements.Advertisement.IsReady("rewardedVideo")) {

			UnityEngine.Advertisements.Advertisement.Show ("rewardedVideo", new UnityEngine.Advertisements.ShowOptions {
				resultCallback = result => {
					if (result == UnityEngine.Advertisements.ShowResult.Finished) {
						Main.Instance.unityChance += 1;
						//выыодим в гуи и сохраняем в префс
						Main.Instance.updateChance ();
						Main.Instance.backToStilePanel();
					}
				}
			});
		}
	}

	/*
	//Метод запускает проверку стадии видео.
	public void ShowAdPlacement(){
		//if (string.IsNullOrEmpty(zoneId)) { zoneId = null; }
		if()
		UnityEngine.Advertisements.ShowOptions options = new UnityEngine.Advertisements.ShowOptions();
		options.resultCallback = HandleShowResult;
		UnityEngine.Advertisements.Advertisement.Show(zoneId, options);
	}
	*/
    /*
    void Update() {
        
        if (adMob.gameObject.active)
        {
            if (Advertisement.IsReady())
            {
                adMob.SetActive(false);
            }
        }
        
    }
    */
	/*
	//метод выполняющий что-то в зависимости от того в какой стадии просмотр видео:
	private void HandleShowResult(UnityEngine.Advertisements.ShowResult result){
		switch (result){
		//видео просмотренно:
		case UnityEngine.Advertisements.ShowResult.Finished:
			schoowADS ();
			break;
		case UnityEngine.Advertisements.ShowResult.Skipped:
			Debug.LogWarning("Video was skipped.");
			break;
		case UnityEngine.Advertisements.ShowResult.Failed:
			Debug.LogError("Video failed to show.");
			break;
		}
	}*/
}
