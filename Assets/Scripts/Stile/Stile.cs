using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stile : MonoBehaviour {

    //ссылка на основной скрипт (отрисовка меню по возвращении назад)
    public Main main = null;

    public string nameStile = "";

    //Список всех таолу стиля:
    public List<Taolu> stileTaolu;

    //Обьект содержащий все кнопки перехода на таолу именно для этого стиля
    public GameObject allTaoluPanell = null;

    public Taolu activeTaolu;

    //Состояние стиля:
    public stateStile state;
    public enum stateStile {
        selectTaolu,
        taolu,
    }

    void Start() {
        main = Main.Instance;
    }

    //Метод выбирает конкретный таолу:
    public void selectTaolu(Taolu taolu) {
		activeTaolu = taolu;
        //проверяем а открыто ли таолу:
		if (taolu.state == Taolu.stateTaolu.open & taolu.loockedState == 0)
        {
            activeTaolu = taolu;
            main.stilePanel.SetActive(false);
            main.leftPanel.SetActive(false);
            allTaoluPanell.SetActive(false);
            main.controllPanel.SetActive(true);
        }
        else {
			//на текщий момент стоимость каждого таолу равна 3!
			//Debug.Log(main.unityChance);
			if (main.unityChance > 0 & taolu.loockedState > 0) {
				main.unityChance -= 1;
				taolu.loockedState -= 1;
				PlayerPrefs.SetInt (taolu.prefsName, PlayerPrefs.GetInt(taolu.prefsName)-1);
				main.updateChance ();
				taolu.updateLockSmile ();
				//Убирем смайлы с кнопки:
				taolu.updateLockSmile ();
				if (taolu.loockedState == 0) {
					main.controllPanel.SetActive (true);
					//main.controllPanellClass.source.clip = activeTaolu.source; 
					taolu.openTaolu ();
					main.stilePanel.SetActive (false);
					main.leftPanel.SetActive (false);
					allTaoluPanell.SetActive (false);
				} else {
					activeTaolu = null;
				}
			}
        }
    }
}
