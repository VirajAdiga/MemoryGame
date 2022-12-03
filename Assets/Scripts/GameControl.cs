using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GameControl : MonoBehaviour
{
    [SerializeField]
    private Transform mPuzzleField;
    public static int sPermission;
    private string mNameOne,mNameTwo;
    private Sprite[] mSprites;
    private HashSet<Sprite> mPlayableSprites;
    public Sprite[] mSpriteArray;
    private Sprite[] mSpriteArr;
    [SerializeField]
    private Sprite mBackgroundSprite,mBlackSprite;
    private int index=0;
    private int mMovesLeft;
    public int mButtonsLeft;
    [SerializeField]
    private TextMeshProUGUI mText;
    [SerializeField]
    private ParticleSystem mParticleEffect;
    private ParticleSystem mParticles;
    private GameControl(){

    }
    static GameControl mInstance;
    public static GameControl Instance{
        get{
            return mInstance;
        }
    }
    void Awake(){
        mInstance=this;
    }
    void Start(){
        mSprites=Resources.LoadAll<Sprite>("Sprites");
        mParticles=Instantiate(mParticleEffect);
        mParticles.gameObject.SetActive(false);
        if (Constants.levelOne)
        {
            mMovesLeft = Constants.levelOneMoves;
            mButtonsLeft= Constants.levelOneGrids;
        }
        else if (Constants.levelTwo)
        {
            mMovesLeft = Constants.levelTwoMoves;
            mButtonsLeft = Constants.levelTwoGrids;
        }
        else if (Constants.levelThree)
        {
            mMovesLeft = Constants.levelThreeMoves;
            mButtonsLeft = Constants.levelThreeGrids;
        }
        else if (Constants.levelFour)
        {
            mMovesLeft = Constants.levelFourMoves;
            mButtonsLeft = Constants.levelFourGrids;
        }
        else
        {
            mMovesLeft = Constants.levelFiveMoves;
            mButtonsLeft = Constants.levelFiveGrids;
        }
        mText.text=mMovesLeft.ToString();
        sPermission=2;
        mPlayableSprites=new HashSet<Sprite>();
        System.Random random=new System.Random();
        while(mPlayableSprites.Count<CreateGrid.Instance.mButtons.Count/2){
            int i=random.Next(0,mSprites.Length);
            mPlayableSprites.Add(mSprites[i]);
        }
        mSpriteArr=new Sprite[mPlayableSprites.Count];
        mPlayableSprites.CopyTo(mSpriteArr,0);
        mSpriteArray=new Sprite[mPlayableSprites.Count*2];
        int j=0;
        for(int i=0;i<mSpriteArray.Length;i++){
            if(j==mSpriteArr.Length)j=0;
            mSpriteArray[i]=mSpriteArr[j];
            ++j;
        }
        Randomizer.Randomize<Sprite>(mSpriteArray);
        GameController();
    }
    private void MApplyBackGround(){
        int time=0;
        for(int i=0;i<CreateGrid.Instance.mButtons.Count;i++){
            Invoke("applyBackGround",(float)time*0.05f);
            ++time;
        }
        CreateGrid.Instance.mButtons[0].playFlipAudio();
    }

    private void applyBackGround(){
        CreateGrid.Instance.mButtonList[index].image.sprite=mBackgroundSprite;
        ++index;
    }

    private void MApplySprites(){
        for(int i=0;i<CreateGrid.Instance.mButtons.Count;i++){
           CreateGrid.Instance.mButtonList[i].image.sprite=mSpriteArray[i];
        }
    }
    private void MAddListeners(){
        for(int i=0;i<CreateGrid.Instance.mButtons.Count;i++){
            CreateGrid.Instance.mButtonList[i].onClick.AddListener(() => onButtonClick());
        }
    }
    private void onButtonClick(){
        if(sPermission>0){
            if(sPermission==2){
                mNameOne=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
                //CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].image.DOPunchRotation(new Vector3(1,1,1),1f,5,0.5f);
                CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].image.sprite=mSpriteArray[int.Parse(mNameOne)];
                CreateGrid.Instance.mButtons[int.Parse(mNameOne)].MChangeSprite();
                Invoke("applyBackSprite",3f);
            }
            else{
                mNameTwo=UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
                //CreateGrid.Instance.mButtonList[int.Parse(mNameTwo)].image.DOPunchRotation(new Vector3(1,1,1),1f,5,0.5f);
                CreateGrid.Instance.mButtonList[int.Parse(mNameTwo)].image.sprite=mSpriteArray[int.Parse(mNameTwo)];
                CreateGrid.Instance.mButtons[int.Parse(mNameTwo)].MChangeSprite();
                Invoke("applyBackSprite",4f);
            }
            --mMovesLeft;
            mText.text=mMovesLeft.ToString();
            if(mMovesLeft==0)AudioManager.Instance.lost();
        }
    }

    private void applyBackSprite(){
        if(sPermission==1){
            CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].image.sprite=mBackgroundSprite;
            CreateGrid.Instance.mButtons[int.Parse(mNameOne)].MNormalBackGround();
        }
        if(sPermission==0){
            if((int.Parse(mNameOne)!=int.Parse(mNameTwo))&&(CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].image.sprite== CreateGrid.Instance.mButtonList[int.Parse(mNameTwo)].image.sprite)){
                CreateGrid.Instance.mButtons[int.Parse(mNameOne)].MNormalBackGround();
                CreateGrid.Instance.mButtons[int.Parse(mNameTwo)].MNormalBackGround();
                CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].enabled=false;
                CreateGrid.Instance.mButtons[int.Parse(mNameOne)].enabled=false;
                CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].image.sprite=mBlackSprite;
                mSpriteArray[int.Parse(mNameOne)]=mBlackSprite;
                mParticleEffect.transform.position=new Vector3(0,0,0);
                mParticles.gameObject.SetActive(true);
                mPuzzleField.DOShakePosition(1f,15f,15,10f,true,true);
                Invoke("destroyParticles",1.5f);
                CreateGrid.Instance.mButtonList[int.Parse(mNameTwo)].enabled=false;
                CreateGrid.Instance.mButtons[int.Parse(mNameTwo)].enabled=false;
                CreateGrid.Instance.mButtonList[int.Parse(mNameTwo)].image.sprite=mBlackSprite;
                mSpriteArray[int.Parse(mNameTwo)]=mBlackSprite;
                mButtonsLeft-=2;
                if(mButtonsLeft==0)AudioManager.Instance.won();
            }else{
                CreateGrid.Instance.mButtonList[int.Parse(mNameOne)].image.sprite=mBackgroundSprite;
                CreateGrid.Instance.mButtons[int.Parse(mNameOne)].MNormalBackGround();
                CreateGrid.Instance.mButtonList[int.Parse(mNameTwo)].image.sprite=mBackgroundSprite;
                CreateGrid.Instance.mButtons[int.Parse(mNameTwo)].MNormalBackGround();
            }
        }
    }

    private void destroyParticles(){
        mParticles.gameObject.SetActive(false);
    }
    private void GameController(){
        MApplySprites();
        Invoke("MApplyBackGround",5f);
        MAddListeners();
    }
}