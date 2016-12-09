using System;

[Serializable]
public class ConfigFile
{
    public ConfigFile(string fileName, string filePath, bool isImmutable)
    {
        FileName = fileName;
        FilePath = filePath;
        IsImmutable = isImmutable;
    }

    public string FilePath { get; set; }
    public string FileName { get; set; }
    public bool IsImmutable { get; set; }
}