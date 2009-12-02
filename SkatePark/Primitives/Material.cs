using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkatePark.Primitives
{
    public class Material
    {
        public Material()
        {
            GL_ID = 0;
        }

        public Material(String id, String fileName, uint GL_ID)
        {
            this.id = id;
            this.fileName = fileName;
            this.GL_ID = GL_ID;
            ambient = new Vector3f();
            diffuse = new Vector3f();
            specular = new Vector3f();
        }

        public Material(String id, String fileName, uint GL_ID, Vector3f ambient, Vector3f diffuse, Vector3f specular)
        {
            this.id = id;
            this.fileName = fileName;
            this.GL_ID = GL_ID;
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        /// <summary>
        /// A UID that is unique to the model.
        /// Must be set for Material to be considered valid.
        /// (i.e. a null id means Material is null)
        /// </summary>
        public string id;

        /// <summary>
        /// An absolute path to the texture's image.
        /// </summary>
        public string fileName;

        public uint GL_ID;

        /// <summary>
        /// The amount of light on this model. May or may not be relevant.
        /// </summary>
        public Vector3f ambient, diffuse, specular;
    }
}
