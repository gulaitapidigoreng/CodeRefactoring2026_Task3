using System.Text.Json;

namespace AutoServiceApp.Storage;

public class JsonFileStore<T> : IDataProvider<T>
{
    private const string AppFolderName = "AutoServiceApp";
    private const string BrokenFileExtension = ".broken.";

    private readonly string _folder;
    private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

    public JsonFileStore()
    {
        var root = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        _folder = Path.Combine(root, AppFolderName);
        Directory.CreateDirectory(_folder);
    }

    public List<T> Load(string name)
    {
        var file = Path.Combine(_folder, name);
        if (!File.Exists(file))
            return new List<T>();

        try
        {
            var json = File.ReadAllText(file);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch
        {
            var bad = Path.Combine(_folder, name + BrokenFileExtension + DateTime.Now.Ticks);
            try { File.Copy(file, bad); } catch { }
            return new List<T>();
        }
    }

    public void Save(string name, List<T> values)
    {
        var file = Path.Combine(_folder, name);
        var json = JsonSerializer.Serialize(values, _options);
        File.WriteAllText(file, json);
    }
}