using System;
using System.Collections.Generic;
using OpenTK;

namespace Prosper.ContentPipeline.WavefrontObj
{
    public class WavefrontObjLoaderResult
    {
        public string ObjectName { get; internal set; }

        public IReadOnlyList<WavefrontObjObject> Objects => _objects.AsReadOnly();

        private readonly List<WavefrontObjObject> _objects = new List<WavefrontObjObject>();
        private WavefrontObjObject _activeObject;

        internal WavefrontObjLoaderResult() { }

        internal void AddVertex(Vector3 v)
        {
            EnsureActiveObject();
            _activeObject.AddVertex(v);
        }

        internal void AddNormal(Vector3 v)
        {
            _activeObject.AddNormal(v);
        }

        internal void AddTextureCoords(Vector2 v)
        {
            _activeObject.AddTextureCoords(v);
        }

        internal WavefrontObjObject GetActiveObject()
        {
            return _activeObject;
        }

        private void EnsureActiveObject()
        {
            if (_activeObject != null)
                return;

            _activeObject = new WavefrontObjObject();
            _objects.Add(_activeObject);
        }
    }
}
