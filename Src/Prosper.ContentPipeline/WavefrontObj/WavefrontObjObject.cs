using System.Collections.Generic;
using System.Linq;
using OpenTK;

namespace Prosper.ContentPipeline.WavefrontObj
{
    public class WavefrontObjObject
    {
        public IReadOnlyCollection<Vector3> Vertices => _vertices.AsReadOnly();
        public IReadOnlyCollection<Vector3> Normals => _normals.AsReadOnly();
        public IReadOnlyCollection<Vector2> TextureCoords => _textureCoords.AsReadOnly();
        public IReadOnlyCollection<WavefrontObjGroup> Groups => _groups.AsReadOnly();

        private readonly List<Vector3> _vertices = new List<Vector3>();
        private readonly List<Vector3> _normals = new List<Vector3>();
        private readonly List<Vector2> _textureCoords = new List<Vector2>();
        private readonly List<WavefrontObjGroup> _groups = new List<WavefrontObjGroup>();

        private WavefrontObjGroup _activeGroup;

        internal WavefrontObjObject() { }

        internal void AddVertex(Vector3 v)
        {
            _vertices.Add(v);
        }

        internal void AddNormal(Vector3 v)
        {
            _normals.Add(v);
        }

        internal void AddTextureCoords(Vector2 v)
        {
            _textureCoords.Add(v);
        }

        internal void ActivateGroupByMaterial(string mtl)
        {
            if (_activeGroup == null || _activeGroup.HasFaces || !string.IsNullOrWhiteSpace(_activeGroup.Material))
            {
                _activeGroup = new WavefrontObjGroup { Material = mtl };
                return;
            }

            _activeGroup.Material = mtl;
        }

        internal void ActivateGroupByName(string name)
        {
            if (_activeGroup == null || _activeGroup.HasFaces || !string.IsNullOrWhiteSpace(_activeGroup.Name))
            {
                _activeGroup = new WavefrontObjGroup { Name = name };
                return;
            }

            _activeGroup.Name = name;
        }

        internal void ActivateGroupBySmoothing(int smoothing)
        {
            var obj = _groups.FirstOrDefault(o => o.Smoothing == smoothing);
            if (obj == null)
            {
                obj = new WavefrontObjGroup { Smoothing = smoothing };
                _groups.Add(obj);
            }

            _activeGroup.Smoothing = smoothing;
        }
    }
}
