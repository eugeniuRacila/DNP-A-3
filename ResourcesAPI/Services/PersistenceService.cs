using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ResourcesAPI.Services
{
    public class PersistenceService
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions();
        
        private string _path { get; }

        public PersistenceService(string path)
        {
            _path = path;
            _jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
       
        public async Task CreatePersistanceFile()
        {
            using (FileStream fs = File.Create(_path))
            {
                await JsonSerializer.SerializeAsync(fs, new string[0]);
            }
        }

        public async Task WriteList<T>(List<T> list)
        {
            if (!File.Exists(_path))
                await CreatePersistanceFile();

            using (StreamWriter sw = File.CreateText(_path))
            {
                await sw.WriteAsync(JsonSerializer.Serialize(list, _jsonSerializerOptions));
            }
        }

        public async Task<List<T>> ReadList<T>()
        {
            if (!File.Exists(_path))
                await CreatePersistanceFile();

            List<T> list;

            using (FileStream fs = File.Open(_path, FileMode.Open))
            {
                list = await JsonSerializer.DeserializeAsync<List<T>>(fs, _jsonSerializerOptions);
            }

            return list;
        }

        public List<T> InitializeData<T>()
        {
            if (!File.Exists(_path))
            {
                List<T> list = new List<T>();
                File.WriteAllText(_path, JsonSerializer.Serialize(list));

                return list;
            }

            return JsonSerializer.Deserialize<List<T>>(File.ReadAllText(_path), _jsonSerializerOptions);
        }
    }
}
