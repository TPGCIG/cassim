using UnityEditor;
using UnityEngine;
using System.IO;

public class MetaFileCleanup
{
    [MenuItem("Tools/Clean Orphaned Meta Files")]
    public static void CleanOrphanedMetaFiles()
    {
        string[] allMetaFiles = Directory.GetFiles("Assets", "*.meta", SearchOption.AllDirectories);
        foreach (string metaFile in allMetaFiles)
        {
            string assetPath = metaFile.Substring(0, metaFile.Length - 5); // Remove ".meta" from path
            if (!File.Exists(assetPath) && !Directory.Exists(assetPath))
            {
                // If the asset or directory no longer exists, delete the orphaned .meta file
                Debug.Log("Deleting orphaned .meta file: " + metaFile);
                File.Delete(metaFile);
            }
        }
    }
}
 