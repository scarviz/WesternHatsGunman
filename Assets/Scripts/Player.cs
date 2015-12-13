using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	/// <summary>
	/// マズル フラッシュ
	/// </summary>
	private GameObject PfbMuzzleFlash;

	/// <summary>
	/// ショット音
	/// </summary>
	private AudioSource ShotSE;

	/// <summary>
	/// 振り返った時のY軸角度 範囲From
	/// </summary>
	public float BackAnglesYFm = 20;
	/// <summary>
	/// 振り返った時のY軸角度 範囲To
	/// </summary>
	public float BackAnglesYTo = 160;

	/// <summary>
	/// 正面のY軸角度 範囲From
	/// </summary>
	public float FrontAnglesYFm = 200;
	/// <summary>
	/// 正面のY軸角度 範囲To
	/// </summary>
	public float FrontAnglesYTo = 300;

	/// <summary>
	/// 360度
	/// </summary>
	private const float ANGLES_F = 360;

	// Use this for initialization
	void Start()
	{
		Cardboard.SDK.TapIsTrigger = true;
		PfbMuzzleFlash = transform.FindChild("MuzzleFlash").gameObject;
		ShotSE = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		SetLookBack();

		if ((Cardboard.SDK.Triggered || Input.GetButtonUp("Fire1"))
			&& (!GameMng.IsShownMenu))
		{
			PfbMuzzleFlash.SetActive(true);
			Invoke("ReMuzzleFlash", 2.5f);
			ShotSE.PlayOneShot(ShotSE.clip);

			// 自身の向きベクトルからY軸角度を取得
			float angleDir = transform.eulerAngles.y;
			// 正面を向いたどうか
			if (FrontAnglesYFm <= angleDir && angleDir <= FrontAnglesYTo)
			{
				Debug.Log("look front");
				GameMng.SetShotedFighter(GameMng.GunFighter.Player);
			}
		}
	}

	/// <summary>
	/// 決闘開始から振り返ったかどうかを設定する
	/// </summary>
	private void SetLookBack()
	{
		// 決闘が開始していない、または既に振り返っている場合
		if (!GameMng.StartedDuel || GameMng.PlayerData.IsLookBacked)
		{
			return;
		}

		// 自身の向きベクトルからY軸角度を取得
		float angleDir = transform.eulerAngles.y;
		// 振り返ったかどうか
		if (BackAnglesYFm <= angleDir && angleDir <= BackAnglesYTo)
		{
			Debug.Log("look back");
			GameMng.PlayerData.IsLookBacked = true;
		}
	}

	/// <summary>
	/// マズルフラッシュを消す
	/// </summary>
	private void ReMuzzleFlash()
	{
		PfbMuzzleFlash.SetActive(false);
	}
}
