using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CreateGrid : MonoBehaviour
{
   [SerializeField]
   private Transform mPuzzleField;
   [SerializeField]
   private Button mButton;
   public List<Tile> mButtons=new List<Tile>();
   public List<Button> mButtonList=new List<Button>();
   private int mGrids;
   private CreateGrid()
   {
        
   }
   static CreateGrid m_Instance;
   public static CreateGrid Instance
   {
        get
        {
            return m_Instance;
        }
   }
   void Awake(){
      m_Instance=this;
      if(Constants.levelOne){
         mGrids=Constants.levelOneGrids;
      }else if(Constants.levelTwo){
         mGrids=Constants.levelTwoGrids;
      }else if(Constants.levelThree){
            mGrids = Constants.levelThreeGrids;
      }else if (Constants.levelFour)
        {
            mGrids = Constants.levelFourGrids;
        }
        else
        {
            mGrids = Constants.levelFiveGrids;
        }
      for(int i=0;i<mGrids;i++){
         Button button=Instantiate(mButton);
         Tile tile=button.GetComponent<Tile>();
         button.name=""+i;
         tile.name=""+i;
         button.transform.SetParent(mPuzzleField,false);
         mButtons.Add(tile);
         mButtonList.Add(button);
      }
      mPuzzleField.DOMove(Vector3.zero,0.4f,false);
      mPuzzleField.DOShakeScale(1f,new Vector3(1,1,1),10,20f,true);
   }
}