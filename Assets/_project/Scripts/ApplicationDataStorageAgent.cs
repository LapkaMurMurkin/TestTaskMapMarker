using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ApplicationDataStorageAgent
{
    private string _savesDirectory;
    private string _saveFilePath;

    private BinaryFormatter _binaryFormatter;

    public ApplicationDataStorageAgent()
    {
        _savesDirectory = Application.dataPath + "/Saves";

        if (!Directory.Exists(_savesDirectory))
            Directory.CreateDirectory(_savesDirectory);

        _saveFilePath = _savesDirectory + "/ApplicationData.save";

        InitializeBinnaryFormatter();
    }

    private void InitializeBinnaryFormatter()
    {
        _binaryFormatter = new BinaryFormatter();

        SurrogateSelector selector = new SurrogateSelector();

        Vector2SerializationSurrogate vector2Surrogate = new Vector2SerializationSurrogate();
        PinInfoSerializationSurrogate pinInfoSurrogate = new PinInfoSerializationSurrogate();

        selector.AddSurrogate(typeof(Vector2), new StreamingContext(StreamingContextStates.All), vector2Surrogate);
        selector.AddSurrogate(typeof(PinInfo), new StreamingContext(StreamingContextStates.All), pinInfoSurrogate);

        _binaryFormatter.SurrogateSelector = selector;
    }

    public ApplicationData Load()
    {
        if (File.Exists(_saveFilePath))
        {
            FileStream file = File.Open(_saveFilePath, FileMode.Open);
            ApplicationData applicationData = _binaryFormatter.Deserialize(file) as ApplicationData;
            file.Close();
            return applicationData;
        }
        else
        {
            ApplicationData newApplicationData = new ApplicationData();
            return newApplicationData;
        }
    }

    public void Save(ApplicationData applicationData)
    {
        FileStream file = File.Create(_saveFilePath);
        _binaryFormatter.Serialize(file, applicationData);
        file.Close();
    }
}
