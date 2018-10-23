using System.Collections.Generic;
using System.Linq;

namespace Prosper.ContentPipeline.WavefrontObj
{
    public class WavefrontObjGroup
    {
        public string Name { get; internal set; }
        public int? Smoothing { get; internal set; }
        public string Material { get; internal set; }

        internal bool HasFaces => _faces.Any();

        private readonly List<object> _faces = new List<object>();
    }
}
