using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum buttonStatus{normal,flipped};
public class Tile : MonoBehaviour
{
    public buttonStatus mButtonStatus;
    [SerializeField]
    private AudioSource mAudioSource;
    [SerializeField]
    private AudioClip mClick,mFlip;

    public void MChangeSprite(){
        mButtonStatus=buttonStatus.flipped;
        mAudioSource.PlayOneShot(mClick,1f);
        GameControl.sPermission-=1;
    }
    public void MNormalBackGround(){
        mButtonStatus=buttonStatus.normal;
        mAudioSource.PlayOneShot(mFlip,1f);
        GameControl.sPermission+=1;
    }
    public void playFlipAudio(){
        mAudioSource.PlayOneShot(mFlip,1f);
    }
}
