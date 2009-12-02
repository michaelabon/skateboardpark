using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SkatePark.Drawables;
using SkatePark.Primitives;
using System.IO;

namespace SkatePark.Drawables
{
    public static class CubletWarehouse
    {
        private static string pathToDataFiles = @"Raw Models\DAE to OBJ\";
        public static Dictionary<string, CubletRenderingData> cubletDictionary = new Dictionary<string, CubletRenderingData>();
        public static Dictionary<string, int> textureDictionary = new Dictionary<string,int>();

        private static void LoadData(string cubletName)
        {
            ModelImporter importer = new ModelImporter(pathToDataFiles + cubletName + @"\" + cubletName + ".obj");
            CubletRenderingData data = new CubletRenderingData();
            data.normalArray = importer.normalArray;
            data.texelArray = importer.texelArray;
            data.triangleArray = importer.triangleArray;
            data.vertexArray = importer.vertexArray;
            cubletDictionary.Add(cubletName, data);
        }

        public static void LoadAllData()
        {
            DirectoryInfo directory = new DirectoryInfo(pathToDataFiles);
            DirectoryInfo[] modelDirectories = directory.GetDirectories();
            foreach (DirectoryInfo modelDirectory in modelDirectories)
            {
                LoadData(modelDirectory.Name.ToLower());
            }
        }
    }

    public struct CubletRenderingData
    {
        public List<Vector3f> vertexArray { get; set; }
        public List<Vector2f> texelArray { get; set; }
        public List<Vector3f> normalArray { get; set; }
        public List<Triangle> triangleArray { get; set; }
    }


}
