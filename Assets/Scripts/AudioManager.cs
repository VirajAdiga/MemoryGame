using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource mAudioSource;
    [SerializeField]
    private Slider mSlider;
    [SerializeField]
    private GameObject mPauseMenu,mGame,mWon,mLost;
    private AudioManager()
   {
        
   }
   static AudioManager m_Instance;
   public static AudioManager Instance
   {
        get
        {
            return m_Instance;
        }
   }
   void Awake(){
       m_Instance=this;
   }
    void Start(){
        mSlider.value=0.7f;
        mGame.SetActive(true);
        mPauseMenu.SetActive(false);
        mWon.SetActive(false);
        mLost.SetActive(false);
    }
    public void changeVolume(){
        mAudioSource.volume=mSlider.value;
    }

    public void resume(){
        mGame.SetActive(true);
        mPauseMenu.SetActive(false);
    }
    public void pause(){
        mGame.SetActive(false);
        mPauseMenu.SetActive(true);
    }
    public void backToMenu(){
        Application.LoadLevel("Menu");
    }
    public void won(){
        mGame.SetActive(false);
        mWon.SetActive(true);
    }
    public void lost(){
        mGame.SetActive(false);
        mLost.SetActive(true);
    }
    public void retry(){
        Application.LoadLevel("Game");
    }
}
