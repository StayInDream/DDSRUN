using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 本地数据存储
/// </summary>
public class PrefsAPI : QSingleton<PrefsAPI>
{
    public const string USER_NAME = "username";  //用户名
    public const string USER_LEVEL = "userlevel";  //用户等级

    public const string BGM_VOl = "bgmVol";
    public const string SE_VOl = "seVol";
    public const string VOICE_VOL = "voiceVol";

    #region 玩家数据
    public const string USER_COIN = "usercoin";
    public const string USER_DIAMOND = "userdiamond";
    public const string USER_PLAYERS = "userplayers";
    public const string USER_HEIGHTSCORE = "heightscore";
    public const string CUR_PLAYERNAME = "curplayername";

    //获得玩家名
    public static string GetUserName()
    {
        return PlayerPrefs.GetString( USER_NAME, string.Empty );
    }
    //保存玩家名
    public static void SetUserName( string name )
    {
        PlayerPrefs.SetString( USER_NAME, name );
    }
    //获得玩家等级
    public static int GetUserLevel()
    {
        return PlayerPrefs.GetInt( USER_LEVEL, 1 );
    }
    //保存玩家等级
    public static void SetUserLevel( int lv )
    {
        PlayerPrefs.SetInt( USER_LEVEL, lv );
    }
    //获得玩家最高分
    public static int GetHeightScore()
    {
        return PlayerPrefs.GetInt( USER_HEIGHTSCORE, 0 );
    }
    //保存玩家最高分
    public static void SetHeightScore( int heightscore )
    {
        PlayerPrefs.SetInt( USER_HEIGHTSCORE, heightscore );
    }
    //获得玩家金币数量
    public static int GetCoins()
    {
        return PlayerPrefs.GetInt( USER_COIN, 0 );
    }
    //保存玩家金币数量
    public static void SetCoins( int coins )
    {
        PlayerPrefs.SetInt( USER_COIN, coins );
    }
    //获得玩家钻石数量
    public static int GetDiamonds()
    {
        return PlayerPrefs.GetInt( USER_DIAMOND, 0 );
    }
    //保存玩家钻石数量
    public static void SetDiamonds( int dia )
    {
        PlayerPrefs.SetInt( USER_DIAMOND, dia );
    }

    /// <summary>
    /// 获得玩家已解锁的角色名（保存形式为"xxx,xxxx" 注：逗号为英文的逗号）
    /// </summary>
    /// <returns></returns>
    public static string[] GetPlayers()
    {
        string[] playernames = { };
        string str = PlayerPrefs.GetString( USER_PLAYERS, string.Empty );
        if(!string.IsNullOrEmpty( str ))
        {
            playernames = str.Split( ',' );
        }
        return playernames;
    }

    public static void SetPlayers( string newplayername )
    {
        string newnames = PlayerPrefs.GetString( USER_PLAYERS, string.Empty );
        if(!string.IsNullOrEmpty( newplayername ))
        {
            newnames += "," + newplayername;
            PlayerPrefs.SetString( USER_PLAYERS, newnames );
        }
        else
        {
            Debug.Log( "ERROR ==> 保存角色时传参有误：参数为空 or null " );
        }
    }

    //获得当前玩家出场角色
    public static string CurPlayerName()
    {
        return PlayerPrefs.GetString( CUR_PLAYERNAME, string.Empty );
    }

    public static void SetCurPlayer( string playername )
    {
        if(!string.IsNullOrEmpty( playername ))
        {
            PlayerPrefs.SetString( CUR_PLAYERNAME, playername );
        }
        else
        {
            Debug.Log( "ERROR ==> 保存当前角色时传参有误：参数为空 or null " );
        }
    }

    #endregion

    #region 角色数据
    private const int maxSkillLev = 10; //技能的最大等级
    private const int maxSkillCount = 4;  //技能个数

    //获得某角色的技能等级，目前有四个技能
    public static int[] GetPlayerSkillLEV( string playername )
    {
        int[] skills = new int[maxSkillCount];
        string str = PlayerPrefs.GetString( playername.ToUpper(),string.Empty );
        if(!string.IsNullOrEmpty(str))
        {
            for(int i = 0; i < str.Split(',').Length ; i++)
            {
                if(i< maxSkillCount - 1)
                {
                    skills[i] = int.Parse( str.Split( ',' )[i] );
                }
            }
        }
        return skills;
    }

    //保存某角色的技能等级信息
    public static void SavePlayerSkill( string playername ,string skillInfo)
    {
        PlayerPrefs.SetString( playername.ToUpper(), skillInfo );
    }
    #endregion

    #region 音量
    public static float GetBgmVolume()
    {
        return PlayerPrefs.GetFloat( BGM_VOl, 1f );
    }

    public static void SetBgmVolume( float vol )
    {
        PlayerPrefs.SetFloat( BGM_VOl, vol );
    }

    public static float GetSeVolume()
    {
        return PlayerPrefs.GetFloat( SE_VOl, 1f );
    }

    public static void SetSeVolume( float vol )
    {
        PlayerPrefs.SetFloat( SE_VOl, vol );
    }

    public static float GetVoiceVolume()
    {
        return PlayerPrefs.GetFloat( VOICE_VOL, 1f );
    }

    public static void SetVoiceVolume( float vol )
    {
        PlayerPrefs.SetFloat( VOICE_VOL, vol );
    }
    #endregion

    public static void DeletePlayPrefs( string str )
    {
        PrefsAPI.DeletePlayPrefs( str );
    }
}
