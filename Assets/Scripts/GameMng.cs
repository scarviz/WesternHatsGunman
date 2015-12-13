using UnityEngine;
using System.Collections;

public class GameMng : MonoBehaviour
{
	/// <summary>
	/// タイトル
	/// </summary>
	private GameObject Title;
	/// <summary>
	/// リザルト
	/// </summary>
	private GameObject Result;
	/// <summary>
	/// Win
	/// </summary>
	private GameObject Win;
	/// <summary>
	/// Lose
	/// </summary>
	private GameObject Lose;

	#region enum
	/// <summary>
	/// GunFighterの識別用
	/// </summary>
	public enum GunFighter
	{
		None = -1,
		Player = 1,
		Enemy,
	}
	#endregion

	#region プロパティ
	/// <summary>
	/// Menuが表示しているかどうか
	/// </summary>
	public static bool IsShownMenu { get; set; }

	private static bool _startedDuel = false;
	/// <summary>
	/// 決闘が開始したかどうか
	/// </summary>
	/// <remarks>
	/// 敵キャラが振り返り始めた時にtrueにする
	/// </remarks>
	public static bool StartedDuel {
		get
		{
			return _startedDuel;
        }
		set
		{
			if (value)
			{
				Clear();
            }
			_startedDuel = value;
        }
	}

	/// <summary>
	/// 経過時間
	/// </summary>
	public static float ElapsedTime { get; set; }

	/// <summary>
	/// 勝者
	/// </summary>
	public static GunFighter Winner { get; set; }

	/// <summary>
	/// プレイヤーデータ
	/// </summary>
	public static PlayerData PlayerData { get; set; }
	#endregion

	// Use this for initialization
	void Start ()
	{
		// 自動スリープを無効にする場合
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		Cardboard.SDK.TapIsTrigger = true;
		ClearPlayerData();
		Clear();

		Title = transform.FindChild("Title").gameObject;
		Result = transform.FindChild("Result").gameObject;
		Win = transform.FindChild("Result").FindChild("Win").gameObject;
		Lose = transform.FindChild("Result").FindChild("Lose").gameObject;

		Title.SetActive(true);
		Result.SetActive(false);
		IsShownMenu = true;
    }

	// Update is called once per frame
	void Update()
	{
		if (StartedDuel)
		{
			ElapsedTime += Time.deltaTime;
		}

		if (GameMng.IsShownMenu)
		{
			if ((Cardboard.SDK.Triggered || Input.GetButtonUp("Fire1")))
			{
				// 勝者が決まっていない(タイトル画面の場合)
				if (Winner == GunFighter.None)
				{
					Debug.Log("Start");
					Invoke("RemTitle", 0.5f);
				}
				// 勝者が決まっている(リザルト表示の場合)
				else
				{
					Debug.Log("ReStart");
					Application.LoadLevel(Application.loadedLevel);
				}
			}
		}
		else if (Winner != GunFighter.None)
		{
			Invoke("ViewResult", 0.5f);
		}
	}

	private static System.Object lockThis = new System.Object();
	/// <summary>
	/// 撃った人を設定する
	/// </summary>
	/// <param name="fighter">撃った人</param>
	public static void SetShotedFighter(GunFighter fighter)
	{
		lock (lockThis)
		{
			// 未設定の場合
			if (Winner == GunFighter.None)
			{
				Winner = fighter;
			}
		}
    }

	#region private
	/// <summary>
	/// シーン毎の初期化
	/// </summary>
	private static void Clear()
	{
		ElapsedTime = 0f;
		Winner = GunFighter.None;

		PlayerData.LookBackTime = 0f;
		PlayerData.IsLookBacked = false;
	}

	/// <summary>
	/// プレイヤーデータの初期化
	/// </summary>
	private static void ClearPlayerData()
	{
		var data = new PlayerData();
		data.TriggerCnt = 0;
		data.LookBackTime = 0f;
		data.IsLookBacked = false;

		PlayerData = data;
	}

	/// <summary>
	/// タイトルを取り除く
	/// </summary>
	private void RemTitle()
	{
		Title.SetActive(false);
		IsShownMenu = false;
	}

	/// <summary>
	/// リザルトを表示する
	/// </summary>
	private void ViewResult()
	{
		if (Winner == GunFighter.Player)
		{
			Win.SetActive(true);
			Lose.SetActive(false);
		}
		else
		{
			Win.SetActive(false);
			Lose.SetActive(true);
		}
		Result.SetActive(true);
		IsShownMenu = true;
	}
	#endregion
}
