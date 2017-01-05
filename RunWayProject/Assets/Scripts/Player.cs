using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //定义角色移动速度
    public float mMoveSpeed = 2.5f;
    public Animation mAnim;
    //左右移动的距离
    public float offetDistance = 1;
    public int slideDelicacy = 40; //滑动灵敏度
    private EplayerPos playerPos = EplayerPos._center;
    private bool isMovingTo = false; //当前是否正在滑动

    //摄像机
    private Transform mCamera;

    /// <summary>
    /// 背景图片
    /// </summary>
    private Transform mBG;

    //角色是否在奔跑
    private bool isRuning = true;
    //角色是否死亡
    private bool isDead = false;

    //收集的金币数目
    private int mCoinCount = 0;

    private enum EplayerPos
    {
        _left,
        _right,
        _center,
    }

    enum slideVector { nullVector, left, right, up, down };

    private Vector2 lastPos;//上一个位置  

    private Vector2 currentPos;//下一个位置  

    private slideVector currentVector = slideVector.nullVector;//当前滑动方向  
    private float timer;//时间计数器  

    public float offsetTime = 0.1f;//判断的时间间隔  


    public int CoinCount
    {
        get
        {
            return mCoinCount;
        }
    }

    //当前奔跑距离
    private int mRundistance = 0;
    public int Rundistance
    {
        get
        {
            return mRundistance;
        }
    }

    //当前得分
    private int mGrade = 0;
    public int Grade
    {
        get
        {
            return mGrade;
        }
    }

    // Use this for initialization
    void Start()
    {
        //获取相机
        mCamera = Camera.main.transform;
        //获取背景
        //mBackground = GameObject.Find( "Background" ).transform;

        mAnim = transform.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isRuning)
        {
            CheckMove();
            CreateCubeWay();
            UpdatePlayerData();
        }
        else
        {
            PlayerDeath();
        }
    }

    /// <summary>
    /// 角色死亡
    /// </summary>
    private void PlayerDeath()
    {
        if(isDead == false)
        {
            //播放死亡动画

        }
    }

    /// <summary>
    /// 更新角色信息
    /// </summary>
    private void UpdatePlayerData()
    {
        //计算奔跑距离
        mRundistance = (int)( ( transform.position.x + 25 ) * 25 );
        //计算玩家得分
        mGrade = (int)( mRundistance * 0.8 + mCoinCount * 0.2 );
    }

    //上跳
    private void Jump()
    {
        StartCoroutine( "RestJumpState" );
        transform.position += Vector3.up;
    }

    IEnumerator RestJumpState()
    {
        yield return new WaitForSeconds( 0.5f );
        currentVector = slideVector.nullVector;
    }
    //向左移动
    private void MoveLeft()
    {
        transform.position += Vector3.left;
    }
    //向右移动
    private void MoveRight()
    {
        transform.position += Vector3.right;
    }

    //加速下落并滑行
    private void QuickDown()
    {
        transform.position = new Vector3( transform.position.x, 0.75f, transform.position.z );
    }

    private void CreateCubeWay()
    {

    }

    //滑行
    private void SliderDown()
    {

    }

    private void CheckMove()
    {
        if(Input.GetMouseButtonDown( 0 ))
        {
            lastPos = Input.mousePosition;
            currentPos = Input.mousePosition;
            timer = 0;
        }
        if(Input.GetMouseButton( 0 ) && isMovingTo == false)
        {
            currentPos = Input.mousePosition;
            timer += Time.deltaTime;
            if(timer > offsetTime)
            {
                float offsetY = currentPos.y - lastPos.y;
                float offsetX = currentPos.x - lastPos.x;
                if(Mathf.Abs( offsetX ) < Mathf.Abs( offsetY ))
                {
                    if(offsetY >= slideDelicacy) // UP向上跳跃
                    {
                        if(currentVector == slideVector.up)
                        {
                            return;
                        }
                        currentVector = slideVector.up;
                        Jump();
                        isMovingTo = true;
                    }
                    else if(offsetY <= -slideDelicacy)
                    {
                        if(currentVector == slideVector.up)
                            QuickDown();
                        else
                            SliderDown();
                        currentVector = slideVector.down;
                        isMovingTo = true;
                    }
                }
                else
                {
                    if(offsetX <= -slideDelicacy)
                    {
                        if(playerPos == EplayerPos._left)
                        {
                            return;
                        }
                        currentVector = slideVector.left;
                        if(playerPos != EplayerPos._left)
                        {

                        }
                        if(playerPos == EplayerPos._center)
                        {
                            playerPos = EplayerPos._left;
                        }
                        else if(playerPos == EplayerPos._right)
                        {
                            playerPos = EplayerPos._center;
                        }
                        isMovingTo = true;
                        MoveLeft();
                    }
                    else
                    if(offsetX >= slideDelicacy)
                    {
                        if(playerPos == EplayerPos._right)
                        {
                            return;
                        }
                        //TODO trun right event  
                        currentVector = slideVector.right;
                        if(playerPos == EplayerPos._center)
                        {
                            playerPos = EplayerPos._right;
                        }
                        else if(playerPos == EplayerPos._left)
                        {
                            playerPos = EplayerPos._center;
                        }
                        isMovingTo = true;
                        MoveRight();
                    }
                }
                lastPos = currentPos;
                timer = 0;
            }
        }

        if(Input.GetMouseButtonUp( 0 ))
        {
            isMovingTo = false;
        }


        Vector3 vSpeed = new Vector3( this.transform.forward.x, this.transform.forward.y, this.transform.forward.z ) * mMoveSpeed;
        // Vector3 jumpSpeed = new Vector3( this.transform.up.x, this.transform.up.y, this.transform.up.z ) * jumpHeight * m_jumpState;
        //让角色开始奔跑
        //transform.Translate( Vector3.forward * mMoveSpeed * Time.deltaTime );
        transform.position += ( vSpeed ) * Time.deltaTime;
        //移动摄像机
        mCamera.Translate( Vector3.forward * mMoveSpeed * Time.deltaTime );
    }



    void OnTriggerEnter( Collider mCollider )
    {
        //如果碰到的是金币，则金币消失，金币数目加1;
        if(mCollider.gameObject.tag == "Coin")
        {
            Destroy( mCollider.gameObject );
            mCoinCount += 1;
        }
        //如果碰到的是障碍物，则游戏结束
        else if(mCollider.gameObject.tag == "Rock")
        {
            isRuning = false;
        }

    }
}
