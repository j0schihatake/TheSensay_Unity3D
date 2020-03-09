using UnityEngine;
using System.Collections;

public class ControllPanel : MonoBehaviour {
    //Наша контрольная панель
    private Main main = null;

    public AudioSource source = null;

    void Start() {
        main = Main.Instance;
    }

    //----------------------------------------------------------Методы контрольной панели:-------------------------------------------
    public void playAll()
    {
        main.stile.activeTaolu.lastCount = 0;
        //source.Play();
        //Запуск проигрывания всего таолу:
        main.playOnlyAnimation(main.unit, main.stile.activeTaolu.fullName);
    }
    //Запуск проигрываниятаолу по частям:
    public void playOne()
    {
        main.stile.activeTaolu.lastCount = 0;
        main.playOnlyAnimation(main.unit, main.stile.activeTaolu.actionTaoluName[main.stile.activeTaolu.lastCount]);
    }
    //кнопка играть следующее движение:
    public void playNext()
    {
        if (main.stile.activeTaolu.lastCount < (main.stile.activeTaolu.actionTaoluName.Count - 1))
        {
            main.stile.activeTaolu.lastCount += 1;
            main.playOnlyAnimation(main.unit, main.stile.activeTaolu.actionTaoluName[(main.stile.activeTaolu.lastCount)]);
        }
    }
    //Кнопка играть предидующее движение:
    public void playPrevious()
    {
        if (main.stile.activeTaolu.lastCount > (0))
        {
            main.stile.activeTaolu.lastCount -= 1;
            main.playOnlyAnimation(main.unit, main.stile.activeTaolu.actionTaoluName[(main.stile.activeTaolu.lastCount)]);
        }
    }
    //Кнопка назад:
    public void back()
    {
        main.controllPanel.SetActive(false);
        main.stile.allTaoluPanell.SetActive(true);
        main.stile.state = Stile.stateStile.selectTaolu;
        main.stile.main.randomAnimation(main.stile.main.unit, main.idleAnimationsName);
    }
}
