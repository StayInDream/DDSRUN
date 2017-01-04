using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;


/// <summary>
/// 游戏的所有数据管理
/// </summary>
public class DataManager : QSingleton<DataManager>
{

    private DataManager() { }

    public void ReadText( string textname )
    {

        string path = Application.dataPath + "/Resources/TextFiles/" + textname + ".txt";

        StreamReader sr = new StreamReader( path, Encoding.Default );
        string line;
        while(( line = sr.ReadLine() ) != null)
        {
            print( line );
        }
    }
}
