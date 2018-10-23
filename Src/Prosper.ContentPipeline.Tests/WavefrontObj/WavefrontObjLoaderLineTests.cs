using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenTK;
using Prosper.ContentPipeline.WavefrontObj;

namespace Prosper.ContentPipeline.Tests.WavefrontObj
{
    [TestClass]
    public class WavefrontObjLoaderLineTests
    {
        [TestMethod]
        public void Comment()
        {
            const string input = @"# This is a comment";

            var lines = new[] { input };
            var result = WavefrontObjLoader.LoadObj(lines);

            Assert.IsNotNull(result?.Objects);
            Assert.AreEqual(0, result.Objects.Count);
        }

        [TestMethod]
        public void Object()
        {
            const string input = @"o MyObject";

            var lines = new[] { input };
            var result = WavefrontObjLoader.LoadObj(lines);

            Assert.IsNotNull(result?.Objects);
            Assert.AreEqual(0, result.Objects.Count);
            Assert.AreEqual("MyObject", result.ObjectName);
        }

        [TestMethod]
        public void Vertex()
        {
            const string input = @"v 0.1 0.2 -0.3";

            var lines = new[] { input };
            var result = WavefrontObjLoader.LoadObj(lines);

            Assert.IsNotNull(result?.Objects);
            Assert.AreEqual(1, result.Objects.Count);

            var obj = result.Objects[0];
            Assert.IsNotNull(obj.Vertices);
            Assert.AreEqual(1, obj.Vertices.Count);
            Assert.AreEqual(new Vector3(0.1f, 0.2f, -0.3f), obj.Vertices.First());
        }
    }
}
