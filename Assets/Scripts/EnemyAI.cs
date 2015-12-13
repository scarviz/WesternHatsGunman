using UnityEngine;
using System.Collections;
using System.Threading;

public class EnemyAI : GameMng{
    [SerializeField]
    private Animator animator;

       public float WaitTime;//カウント後の待機時間
       private int WalkId;
       private int IdleId;
       //private int TurnId;
       private int ShotId;
       private int DownId;


    // Use this for initialization
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        WalkId = Animator.StringToHash("Walk");//歩きアニメーション
        IdleId = Animator.StringToHash("Idle");//アイドルアニメーション
        //TurnId = Animator.StringToHash("Turn");//方向転換アニメーション
        ShotId = Animator.StringToHash("Shot");//撃つアニメーション
        DownId = Animator.StringToHash("Down");//やられアニメーション 
//アニメーション状態オンオフ切り替え（遷移）
        animator.SetBool(WalkId, false);
        animator.SetBool(IdleId, true);
        //animator.SetBool(Enemy_AI.gunman.TurnId, false);
        animator.SetBool(ShotId, false);
        animator.SetBool(DownId, false);
        //////////////////////////////////////////////////////

        WaitTime = 0.0f;

    }

	public void Enemy()
	{
		//後ろを向く
		if (gameObject.transform.eulerAngles.y <= 270.0f)
		{
			animator.SetBool(IdleId, false);//アイドルモーションオフ
			gameObject.transform.Rotate(0, Time.deltaTime * 1.0f, 0);//敵キャラ回転
			animator.SetBool(WalkId, true);//歩きモーションオン
		}
		else
		{
			animator.SetBool(WalkId, false);//歩きモーションオフ
			animator.SetBool(IdleId, true);//アイドルモーションオン
		}

		////経過時間を引いた時間が0になったら
		//if (WaitTime - GameMng.ElapsedTime <= 0)
		//{
		//	animator.SetBool(IdleId, false);//アイドルモーションオフ
		//	GameMng.StartedDuel = true;//
		//	while (gameObject.transform.localRotation.y <= 90.0f)
		//	{
		//		//正面に向くまで
		//		animator.SetBool(WalkId, true);//歩きモーションオン
		//		gameObject.transform.Rotate(0, Time.deltaTime * 5.0f, 0);//敵キャラ回転
		//		//animator.SetBool(Enemy_AI.gunman.TurnId, true);
		//	}
		//	animator.SetBool(WalkId, false);//歩きアニメーションオフ

		//	animator.SetBool(ShotId, true);//撃つアニメーションオン
		//	SetShotedFighter(GameMng.GunFighter.Enemy);//勝者は敵
		//}
	}
	
	// Update is called once per frame
	void Update ()
    {
        Enemy();	
	}
}
