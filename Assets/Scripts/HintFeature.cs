using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HintFeature : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private Sprite mSprite;
    private int mHelp;
    void Start(){
        if(Constants.levelOne){
         mHelp=Constants.levelOneHints;
        }else if(Constants.levelTwo){
         mHelp=Constants.levelTwoHints;
        }else if(Constants.levelThree){
         mHelp=Constants.levelThreeHints;
      }else if (Constants.levelFour)
        {
            mHelp = Constants.levelFourHints;
        }
        else
        {
            mHelp = Constants.levelFiveHints;
        }
      mText.text=mHelp.ToString();
    }

    public void Help(){
        if(mHelp>0){
            string random=null; 
            bool found=false;
            System.Random rand=new System.Random();
            int index=0,index1=0;
            while(!found){
                index=rand.Next(0,GameControl.Instance.mSpriteArray.Length);
                if(GameControl.Instance.mSpriteArray[index].name==mSprite.name)continue;
                else{
                    random=GameControl.Instance.mSpriteArray[index].name;
                    found=true;
                }
            }
            for(int i=0;i<GameControl.Instance.mSpriteArray.Length;i++){
                if(i!=index){
                    if(GameControl.Instance.mSpriteArray[i].name==random){
                        index1=i;
                        break;
                    }
                }
            }
            CreateGrid.Instance.mButtonList[index].enabled=false;
            CreateGrid.Instance.mButtons[index].enabled=false;
            CreateGrid.Instance.mButtonList[index].image.sprite=mSprite;
            GameControl.Instance.mSpriteArray[index]=mSprite;
            CreateGrid.Instance.mButtonList[index1].enabled=false;
            CreateGrid.Instance.mButtons[index1].enabled=false;
            CreateGrid.Instance.mButtonList[index1].image.sprite=mSprite;
            GameControl.Instance.mSpriteArray[index1]=mSprite;
            GameControl.Instance.mButtonsLeft-=2;
            --mHelp;
            mText.text=mHelp.ToString();
        }
    }
}
