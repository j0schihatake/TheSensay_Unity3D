using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Taolu : MonoBehaviour {
    //Класс представляет собой таолу и содержит анимации как для демонстрации так и для процесса обучения:
    public Stile stile;

    public int cost = 3;                                                        //стоимость данного таолу

	public AudioClip source = null;											//мелодия которая бует проигрываться при полном просмотре

    //полная не нарезанная анимация:
    public string fullName;                                                     //Название анимации всего таолу целиком в аниматоре
    public string prefsName;
    //Для автоматизации указываем список названий(должно оканчиваться на _)
    public string nameAction = "";
    public int maxCount = 0;                                                    //номер конечной анимации

	//Текущая стадия разблокировки:
	public int loockedState = 3;
	public bool emptyOpen = false;												//является ли таолу сразу доступным
	//Каждое новое таолу залочено 3 смайлами.
	public GameObject block_1 = null;
	public GameObject block_2 = null;
	public GameObject block_3 = null;

    //Нарезки таолу по коротким движениям:
    public List<string> actionTaoluName = new List<string>();                   //Список названий всех частей анимации таолу

    public int lastCount = 0;                                                  //текущий клип на котором остановлено обучение
	public bool updateLockeds = false;
	public bool debugPrefses = false;

    //текущее состояние таолу:
    public stateTaolu state;
    public enum stateTaolu {                                          
        locked,
        open,
    }

    void Start() {
        for (int i = 0; i < maxCount; i++) {
            actionTaoluName.Add(nameAction+i);
        }
        //определяем открыл ли нас пользователь
        StartTaolu();
    }

    //С этого метода начанается работа с таолу:
    public void StartTaolu() {
        //проверяем а доступно ли таолу
        if (state != stateTaolu.open) {
            if (PlayerPrefs.HasKey(prefsName))
            {
				if(PlayerPrefs.GetInt(prefsName)==0){
                //по идее таолу куплен
                state = stateTaolu.open;
				}
            }
            else {
                state = stateTaolu.locked;
            }
        }
    }
	/*
	void Update(){
		if (updateLockeds) {
			updateLockeds = false;
			updateLockSmile ();
		}
		if(debugPrefses){
			debugPrefses = false;
			debugPrefs ();
		}
	}
	*/
	public void debugPrefs(){
		Debug.Log (this.prefsName + PlayerPrefs.GetInt(this.prefsName));
	}

	public void updateLockSmile(){
		loadLoockedState ();
		switch(this.loockedState){
			//Таолу полностью закрыт:
		case 3:
			block_1.SetActive (true);
			block_2.SetActive (true);
			block_3.SetActive (true);
			break;
		case 2:
			block_1.SetActive (false);
			block_2.SetActive (true);
			block_3.SetActive (true);
			break;
		case 1:
			block_1.SetActive (false);
			block_2.SetActive (false);
			block_3.SetActive (true);
			break;
		case 0:
			block_1.SetActive (false);
			block_2.SetActive (false);
			block_3.SetActive (false);
			break;
		}
	}

	public void loadLoockedState(){
		if (!this.emptyOpen) {
			if (PlayerPrefs.HasKey(this.prefsName)) {
				this.loockedState = PlayerPrefs.GetInt (this.prefsName);
			} else {
				this.loockedState = 3;
				PlayerPrefs.SetInt (this.prefsName, 3);
			}
		} else {
			this.loockedState = 0;
			this.state = stateTaolu.open;
		}
	}

    public void openTaolu() {
        state = stateTaolu.open;
        PlayerPrefs.SetInt(prefsName, 0);
    }
}
