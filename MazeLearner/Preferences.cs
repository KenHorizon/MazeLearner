using MazeLearner;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeLeaner
{
    public class Preferences
    {
        delegate void TextProcessAction(ref string text);

        private Dictionary<string, object> dataList = new Dictionary<string, object>();
        private readonly string path;
        private readonly JsonSerializerSettings serializerSettings;
        public readonly bool UseBson;
        private readonly object lockObject = new object();
        public bool AutoSave;

        public event Action<Preferences> OnSave;
        public event Action<Preferences> OnLoad;
        public Preferences(string path, bool parseAllTypes = false, bool useBson = false)
        {
            this.path = path;
            UseBson = useBson;
            if (parseAllTypes)
            {
                serializerSettings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
                    Formatting = Formatting.Indented
                };
            }
            else
            {
                serializerSettings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented
                };
            }
        }

        public bool Load()
        {
            lock (lockObject)
            {
                if (!File.Exists(path))
                    return false;

                try
                {
                    if (!UseBson)
                    {
                        string value = File.ReadAllText(path);
                        dataList = JsonConvert.DeserializeObject<Dictionary<string, object>>(value, serializerSettings);
                    }
                    else
                    {
                        using FileStream stream = File.OpenRead(path);
                        using BsonReader reader = new BsonReader(stream);
                        JsonSerializer jsonSerializer = JsonSerializer.Create(serializerSettings);
                        dataList = jsonSerializer.Deserialize<Dictionary<string, object>>(reader);
                    }

                    if (dataList == null)
                        dataList = new Dictionary<string, object>();

                    if (this.OnLoad != null)
                    {
                        this.OnLoad(this);
                    }

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool Save(bool canCreateFile = true)
        {
            lock (lockObject)
            {
                try
                {
                    if (this.OnSave != null)
                    {
                        this.OnSave(this);
                    }

                    if (!canCreateFile && !File.Exists(path))
                    {
                        return false;
                    }

                    Directory.GetParent(path).Create();
                    if (File.Exists(path))
                    {
                        File.SetAttributes(path, FileAttributes.Normal);
                    }

                    if (!UseBson)
                    {
                        string text = JsonConvert.SerializeObject(dataList, serializerSettings);
                        string bakPath = path + ".bak";
                        File.WriteAllText(bakPath, text);
                        File.Move(bakPath, path, overwrite: true);
                        File.Delete(bakPath);
                        File.SetAttributes(path, FileAttributes.Normal);
                    }
                    else
                    {
                        using FileStream stream = File.Create(path);
                        using BsonWriter jsonWriter = new BsonWriter(stream);
                        File.SetAttributes(path, FileAttributes.Normal);
                        JsonSerializer.Create(serializerSettings).Serialize(jsonWriter, dataList);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }

                return true;
            }
        }

        public void Clear()
        {
            dataList.Clear();
        }

        public void Put(string name, object value)
        {
            lock (lockObject)
            {
                dataList[name] = value;
                if (AutoSave)
                    Save();
            }
        }

        public bool Contains(string name)
        {
            lock (lockObject)
            {
                return dataList.ContainsKey(name);
            }
        }

        public T Get<T>(string name, T defaultValue)
        {
            lock (lockObject)
            {
                try
                {
                    if (dataList.TryGetValue(name, out var value))
                    {
                        if (value is T)
                            return (T)value;

                        if (value is JObject)
                            return JsonConvert.DeserializeObject<T>(((JObject)value).ToString());

                        return (T)Convert.ChangeType(value, typeof(T));
                    }

                    return defaultValue;
                }
                catch
                {
                    return defaultValue;
                }
            }
        }

        public void Get<T>(string name, ref T currentValue)
        {
            currentValue = Get(name, currentValue);
        }

        public List<string> GetAllKeys() => dataList.Keys.ToList();
    }
}
