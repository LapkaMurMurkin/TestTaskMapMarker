using System.Runtime.Serialization;
using UnityEngine;

public class PinInfoSerializationSurrogate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        PinInfo pinInfo = obj as PinInfo;
        info.AddValue("PlaceName", pinInfo.PlaceName);
        info.AddValue("Description", pinInfo.Description);
        info.AddValue("PhotoInBytes", pinInfo.Photo.EncodeToPNG());
        info.AddValue("Coords", pinInfo.Coords);
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        PinInfo pinInfo = obj as PinInfo;
        pinInfo.PlaceName = info.GetValue("PlaceName", typeof(string)) as string;
        pinInfo.Description = info.GetValue("Description", typeof(string)) as string;

        pinInfo.Photo = new Texture2D(2, 2);
        byte[] textureInBytes = info.GetValue("PhotoInBytes", typeof(byte[])) as byte[];
        ImageConversion.LoadImage(pinInfo.Photo, textureInBytes);

        pinInfo.Coords = (Vector2)info.GetValue("Coords", typeof(Vector2));

        return pinInfo;
    }
}