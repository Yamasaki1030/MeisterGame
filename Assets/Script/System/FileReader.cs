using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileReader : MonoBehaviour{

    private TextAsset csvFile;
    private List<string[]> csvDatas = new List<string[]>();
    private List<string> csvDatasIndex = new List<string>();

    // Use this for initialization
    void Start()
    {
        ResipeOpen();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResipeOpen()
    {
        csvFile = Resources.Load("Recipe") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);
        int i = 0;
        
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
            csvDatasIndex.Add(csvDatas[i][0]);
            i++;
        }
    }

    public List<string[]> GetResipeDatas()
    {
        return csvDatas;
    }

    public List<string> GetResipeDatasIndex()
    {
        return csvDatasIndex;
    }
}
