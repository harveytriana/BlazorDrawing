using SkiaSharp;
using System.Reflection;

namespace BlazorDrawing;

public class TypeFaceService
{
    readonly Dictionary<string, SKTypeface> _typeFaces = new();
    
    public SKTypeface GetTTF(string ttfName)
    {
        if (_typeFaces.ContainsKey(ttfName)) {
            return _typeFaces[ttfName];
        }
        if (LoadTypeFace(ttfName)) {
            return _typeFaces[ttfName];
        }
        return SKTypeface.Default;
    }

    bool LoadTypeFace(string ttfName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        try {
            var fileName = ttfName.ToLower() + ".ttf";
            foreach (var item in assembly.GetManifestResourceNames()) {
                if (item.ToLower().EndsWith(fileName)) {
                    Console.WriteLine("Loads {0}", ttfName);
                    var s = assembly.GetManifestResourceStream(item);
                    var f = SKTypeface.FromStream(s);
                    _typeFaces.Add(ttfName, f);
                    return true;
                }
            }
        }
        catch {/* missing resource */}
        return false;
    }
}

