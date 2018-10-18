using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad  {
    public static List<Game> savedGames = new List<Game>();


    public static void Save()
    {
        Game.getInstance().BeforeSave();
        savedGames.Add(Game.getInstance());
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.jean");
        bf.Serialize(file, SaveLoad.savedGames);
        file.Close();
        string json = JsonUtility.ToJson(Game.getInstance());
        File.WriteAllText(Application.persistentDataPath + "/savedGames.json", json);
        Debug.Log(Application.persistentDataPath + "/savedGames.jean");
        Game.getInstance().AfterSaveAndLoad(false);
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/savedGames.jean"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.jean", FileMode.Open);
            SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
            if (savedGames != null && savedGames.Count > 0)
                Game.setGame(savedGames[0]);
            Game.getInstance().AfterSaveAndLoad(true);
        }
    }
}
