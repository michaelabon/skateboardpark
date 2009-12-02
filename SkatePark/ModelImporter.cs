using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using SkatePark.Primitives;
using System.Drawing;
using Tao.OpenGl;

namespace SkatePark
{
    class ModelImporter
    {
        public List<Vector3f> vertexArray { get; set; }
        public List<Vector2f> texelArray { get; set; }
        public List<Vector3f> normalArray { get; set; }
        public List<Triangle> triangleArray { get; set; }

        /// <summary>
        /// Create a new ModelImporter and parse the OBJ file.
        /// </summary>
        /// <param name="relativeFilePath">The relative path to the OBJ file. For example: @"Raw Models\DAE to OBJ\Cube\cube.obj"</param> 
        public ModelImporter(string relativeFilePath)
        {
            parseObjFile(relativeFilePath);
        }

        /// <summary>
        /// Parses an MTL file
        /// </summary>
        /// <param name="filename">The absolute path to the MTL file. For example: @"C:\Users\Mike\Documents\Visual Studio 2008\Projects\SkatePark\SkatePark\Raw Models\DAE to OBJ\Cube\cube.mtl". Hint: Derive the path from the OBJ file.</param> 
        /// <returns>A Dictionary of Material IDs to their Materials</returns>
        private Dictionary<string, Material> parseMtlFile(string filename)
        {
            Dictionary<string, Material> materialDict = new Dictionary<string, Material>();
            Material currentMaterial = new Material();
            using (StreamReader reader = File.OpenText(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split(' ');
                    switch (items[0])
                    {
                        case "#":
                            continue;
                        case "newmtl":
                            if (currentMaterial.id != null)
                            {
                                materialDict.Add(currentMaterial.id, currentMaterial);
                            }
                            currentMaterial = new Material();
                            currentMaterial.id = items[1];
                            break;
                        case "Ka":
                            currentMaterial.ambient = new Vector3f(items[1], items[2], items[3]);
                            break;
                        case "Kd":
                            currentMaterial.diffuse = new Vector3f(items[1], items[2], items[3]);
                            break;
                        case "Ks":
                            currentMaterial.specular = new Vector3f(items[1], items[2], items[3]);
                            break;
                        case "map_Kd":
                            FileInfo fileInfo = new FileInfo(filename);
                            currentMaterial.fileName = fileInfo.DirectoryName + @"\" + items[1];
                            break;
                    }
                }
            }
            materialDict.Add(currentMaterial.id, currentMaterial);
            LoadGLTextures(filename, materialDict);
            return materialDict;
        }

        private void LoadGLTextures (string fileName, Dictionary<string, Material> materialDict)
        {
            foreach (KeyValuePair<string, Material> entry in materialDict)
            {
                Bitmap image = null;
                string file = entry.Value.fileName;
                image = new Bitmap(file);

                if (image == null) { continue; }

                System.Drawing.Imaging.BitmapData bitmapData;
                Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);

                bitmapData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                
                uint[] textureNames = new uint[1];
                Gl.glGenTextures(1, textureNames);
                entry.Value.GL_ID = textureNames[0];

                Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureNames[0]);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MAG_FILTER, Gl.GL_LINEAR);
                Gl.glTexParameteri(Gl.GL_TEXTURE_2D, Gl.GL_TEXTURE_MIN_FILTER, Gl.GL_LINEAR);
                Gl.glTexImage2D(Gl.GL_TEXTURE_2D, 0, (int)Gl.GL_RGB, image.Width, image.Height, 0, Gl.GL_BGR_EXT, Gl.GL_UNSIGNED_BYTE, bitmapData.Scan0);

                image.UnlockBits(bitmapData);
                image.Dispose();
            }

        }

        /// <summary>
        /// Parses an OBJ file
        /// </summary>
        /// <param name="fileName"></param>
        private void parseObjFile(string fileName)
        {
            vertexArray = new List<Vector3f>();
            texelArray = new List<Vector2f>();
            normalArray = new List<Vector3f>();
            triangleArray = new List<Triangle>();
            Dictionary<string, Material> materialDict = null;
            Material currentMaterial = new Material();
            FileInfo fileInfo = new FileInfo(fileName);
            using (StreamReader reader = File.OpenText(fileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] items = line.Split(' ');
                    switch (items[0])
                    {
                        case "#":
                            continue;
                        case "v":
                            vertexArray.Add(new Vector3f(items[1], items[2], items[3]));
                            break;
                        case "vt":
                            texelArray.Add(new Vector2f(items[1], items[2]));
                            break;
                        case "vn":
                            normalArray.Add(new Vector3f(items[1], items[2], items[3]));
                            break;
                        case "f":
                            Debug.Assert(currentMaterial.id != null);
                            triangleArray.Add(new Triangle(items[1], items[2], items[3], currentMaterial));
                            break;
                        case "mtllib":
                            materialDict = parseMtlFile(fileInfo.DirectoryName + @"\" + items[1]);
                            break;
                        case "usemtl":
                            Debug.Assert(materialDict != null);
                            bool success = materialDict.TryGetValue(items[1], out currentMaterial);
                            Debug.Assert(success);
                            break;
                    }
                }
            }
        }
    }
}