using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[System.Serializable]
public class W3UberSplatDataConfigData
{
    public string name;
    public string dir;
    public string file;

    public float scale;

}


public class W3UberSplatDataConfig : SingletonMono<W3UberSplatDataConfig>
{
    Dictionary< string, W3UberSplatDataConfigData > data = new Dictionary< string , W3UberSplatDataConfigData >();
    public List< W3UberSplatDataConfigData > list;

    public override void initSingletonMono()
    {
        for (int i = 0; i < list.Count; i++)
        {
            data.Add( list[ i ].name , list[ i ] );
        }

        list.Clear();
        list = null;
    }
    
    public W3UberSplatDataConfigData getData( string id )
    {
        if ( data.ContainsKey( id ) )
        {
            return data[ id ];
        }

        return null;
    }

    public void initConfig()
    {

    }

    public void clearConfig()
    {
        list.Clear();
    }

#if UNITY_EDITOR

    public void load(byte[] bytes)
    {
        string text = UTF8Encoding.Default.GetString(bytes);

        list = new List<W3UberSplatDataConfigData>();

        string[] lineArray = text.Replace( "\r" , "" ).Split( '\n' );

        for ( int i = 1; i < lineArray.Length; i++)
        {
            W3UberSplatDataConfigData d = new W3UberSplatDataConfigData();

            string[] array = lineArray[ i ].Split( '\t' );

            if ( array.Length < 12)
            {
                continue;
            }

            d.name = array[0];
            d.dir = array[2].Replace( "\\" , "/" );
            d.file = array[3];
            d.scale = float.Parse(array[5]);
            
            list.Add(d);
        }

        Debug.Log("W3 Uber Splat Data loaded num=" + list.Count);
    }

#endif

}
