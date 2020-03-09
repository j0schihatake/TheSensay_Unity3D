using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
    //Ядро программы:

    public GameObject unit;                                                     //префаб бойца исполняющего боевые таолу:

    public static Main Instance = null;

    //Выбираем тип рекламы который мы будем использовать
    public adsType ads;
    public enum adsType {
        AdMob,
        UnityADS,
    }

    public GameObject unityADS = null;
    public GameObject adMobPrefab = null;

	public bool cleanPrefs = false;

    public int chance = 0;
    public int unityChance = 0;
    public Text unityChanceText = null;
    public Text chanceText = null;

    //Изменение на Аниматор(ссылка на аниматор персонажа):
    public Animator anim;

    public List<string> idleAnimationsName;

    public bool iddle = true;

    //Все известные стили:
    public List<Stile> allStile = new List<Stile>();
    //текущий стиль:
    public Stile stile = null;

    //Обьект содержащий кнопки всех имеющихся стилей:
    public GameObject stilePanel = null;
    public GameObject leftPanel = null;
    public GameObject controllPanel = null;

    //------------------------------------------------------------------переменные для определения открыты ли стили
    public bool WinChunChamCUI = false;
    public bool WinChunBiuDjie = false;

    //Состояние меню игры:
    public programState state;
    public enum programState {
        selectStile,
        stileMenu,
        nonidle,
    }

    void Awake() {
        Instance = this;
        state = programState.selectStile;
        //Оставляем одну из реклам

        switch (ads)
        {
            case adsType.AdMob:
                unityADS.SetActive(false);
                break;
            case adsType.UnityADS:
                adMobPrefab.SetActive(false);
                break;
        }
        
//------------------------------------------------------------------------Подгружаем префсы--------------------------------------------------------------
        //здесь же проверяем есть ли сейчас какоето значение в плауерпрефс
        if (PlayerPrefs.HasKey("unityChance"))
        {
            unityChance = PlayerPrefs.GetInt("unityChance");
			updateChance ();
        }
		//обновляем блокировки на таолу:
		for(int i = 0; i < allStile.Count; i++){
			Stile selectStile = allStile [i];
			for(int j = 0; j < selectStile.stileTaolu.Count;j++){
				Taolu selectTaolu = selectStile.stileTaolu [j];
				selectTaolu.updateLockSmile ();
			}
		}
    }

    void Start()
    {
        chanceText.text = chance.ToString();
        anim = unit.GetComponent<Animator>();
    }

    void Update() {
        switch (state) {
            //Выбор стиля:
            case programState.selectStile:
                randomAnimation(unit, idleAnimationsName);
                break;
            //режим отображения GUI конкретного стиля:
            case programState.stileMenu:
                //Так-же воспроизводим случайные статические анимации:
                randomAnimation(unit, idleAnimationsName);
                break;
        }
		if(cleanPrefs){
			cleanPrefs = false;
			cleanPrefses ();
		}
    }

	//Метод чистит префсы:
	void cleanPrefses(){
		for(int i = 0; i < allStile.Count; i++){
			Stile selectStile = allStile [i];
			for(int j = 0; j < selectStile.stileTaolu.Count;j++){
				Taolu selectTaolu = selectStile.stileTaolu[j];
				if (!selectTaolu.emptyOpen) {
					PlayerPrefs.SetInt (selectTaolu.prefsName, 3);
				} else {
					PlayerPrefs.SetInt (selectTaolu.prefsName, 0);
					selectTaolu.state = Taolu.stateTaolu.open;
					selectTaolu.loockedState = 0;
				}
				//Debug.Log (selectTaolu.prefsName + " = " + PlayerPrefs.GetInt(selectTaolu.prefsName));
			}
		}
		PlayerPrefs.SetInt("unityChance", 0);
	}

    //обновление значения в gui и сохранение в реестр:
    public void updateChance() {
        chanceText.text = chance.ToString();
        unityChanceText.text = unityChance.ToString();
        //Обновляем префс:
        PlayerPrefs.SetInt("unityChance", unityChance);
    }

    //Меню выбора стиля:
    public void selectStile(Stile stile) {
        this.stile = stile;
        //теперь отображаем панель выборатаолу именно для этого стиля
        stilePanel.gameObject.SetActive(false);
        this.stile.allTaoluPanell.SetActive(true);
		for(int i = 0 ; i < stile.stileTaolu.Count; i ++){
			Taolu t = stile.stileTaolu [i];
			t.updateLockSmile ();
		}
    }

    //Возврат к выбору стилей:
    public void backToStilePanel() {
        playOnlyAnimation(unit, idleAnimationsName[0]);
        stilePanel.SetActive(true);
		if(stile != null){
        	stile.allTaoluPanell.SetActive(false);
		}
    }

    //-----------------------------------------------АНИМАЦИЯ---------------------------------------------------------------
    public void randomAnimation(GameObject meshObject, List<string> list)
    {
        int index = (int)Random.Range(0.0f, list.Count);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Scelet.001|Idle"))
        {
            playOnlyAnimation(unit, list[index]);
        }
    }

    public void playOnlyAnimation(GameObject meshObject, string animationState)
    {
        meshObject.GetComponent<Animator>().ResetTrigger(animationState);
        meshObject.GetComponent<Animator>().Play(animationState, 0);
    }
}
