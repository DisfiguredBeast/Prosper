using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using OpenTK;
using Prosper.Library;

namespace Prosper.ContentPipeline.WavefrontObj
{
    public class WavefrontObjLoader
    {
        private delegate bool Handler(string value, WavefrontObjLoaderResult result, out string error);

        private readonly string[] _partSplitter = new[] { " " };

        private readonly Dictionary<string, Handler> _handlers;
        private bool _nameSet;

        private WavefrontObjLoader()
        {
            _handlers = new Dictionary<string, Handler>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"#", HandleComment },
                {"o", HandleObjectName },
                {"v", HandleVertex },
                //{"vn", HandleNormal },
                //{"vt", HandleTextureCoords },
                //{"usemtl", HandleUseMaterial },
                //{"s", HandleUseMaterial },
                //{"f", HandleFace },
            };
        }

        private bool TryLoad(IEnumerable<string> lines, out WavefrontObjLoaderResult result, out string error)
        {
            result = new WavefrontObjLoaderResult();
            error = null;

            var index = 0;
            foreach (var line in lines)
            {
                index++;
                if (string.IsNullOrWhiteSpace(line?.Trim()))
                    continue;

                var parts = line.Split(_partSplitter, StringSplitOptions.RemoveEmptyEntries);
                var key = parts[0];
                var value = string.Join(" ", parts.Skip(1));

                if (!_handlers.TryGetValue(key, out Handler handler))
                {
                    error = $"Unknown key \"{key}\" in line {index}.";
                    return false;
                }

                if (handler == null)
                {
                    error = $"No value handler registered for key \"{key}\".";
                    return false;
                }

                if (!handler(value, result, out error))
                    return false;
            }

            return true;
        }

        public static WavefrontObjLoaderResult LoadObj(string path, Encoding encoding)
        {
            Guard.NotNullOrWhiteSpace(path, nameof(path));
            Guard.FileExists(path);

            var lines = File.ReadLines(path, encoding);
            return LoadObj(lines);
        }

        public static WavefrontObjLoaderResult LoadObj(IEnumerable<string> lines)
        {
            Guard.NotNull(lines, nameof(lines));

            var loader = new WavefrontObjLoader();
            if (!loader.TryLoad(lines, out WavefrontObjLoaderResult result, out string error))
                throw new ArgumentException(error, nameof(lines));

            return result;
        }

        #region Handlers

        private static bool HandleComment(string value, WavefrontObjLoaderResult result, out string error)
        {
            // Just ignore comments
            error = null;
            return true;
        }

        private bool HandleObjectName(string value, WavefrontObjLoaderResult result, out string error)
        {
            error = null;
            if (_nameSet)
            {
                error = "Object has already a name set.";
                return false;
            }

            result.ObjectName = value;
            _nameSet = true;
            return true;
        }

        private bool HandleVertex(string value, WavefrontObjLoaderResult result, out string error)
        {
            if (!TryParseVector3(value, out Vector3 vertex, out error))
                return false;

            result.AddVertex(vertex);
            return true;
        }

        //private static void HandleNormal(string value, WavefrontObjLoaderResult result)
        //{
        //    result.AddNormal(ParseVector3(value, "Normal"));
        //}

        //private static void HandleTextureCoords(string value, WavefrontObjLoaderResult result)
        //{
        //    result.AddTextureCoords(ParseVector2(value, "Texture coords"));
        //}
        
        private static void HandleUseMaterial(string value, WavefrontObjLoaderResult result)
        {
            result.GetActiveObject().ActivateGroupByMaterial(value);
        }

        private static void HandleSmoothing(string value, WavefrontObjLoaderResult result)
        {
            var s = string.Equals("off", value.Trim(), StringComparison.InvariantCultureIgnoreCase) ? 0 : int.Parse(value);
            result.GetActiveObject().ActivateGroupBySmoothing(s);
        }

        //private static void HandleFace(string value, WavefrontObjLoaderResult result)
        //{
        //    var components = value.Split(new[] { '/' });
        //    var activeObject = result.GetActiveObject();
        //    var o = activeObject.Vertices
        //}

        #endregion

        #region Util

        private bool TryParseVector2(string value, out Vector2 vector, out string error)
        {
            vector = Vector2.Zero;
            if (!TryParseFloats(value, 2, out float[] values, out error))
                return false;

            vector = new Vector2(values[0], values[1]);
            return true;
        }

        private bool TryParseVector3(string value, out Vector3 vector, out string error)
        {
            vector = Vector3.Zero;
            if (!TryParseFloats(value, 3, out float[] values, out error))
                return false;

            vector = new Vector3(values[0], values[1], values[2]);
            return true;
        }

        private bool TryParseFloats(string value, int count, out float[] values, out string error)
        {
            Guard.GreaterOrEqual(count, 0, nameof(count));

            error = null;
            values = new float[count];

            var parts = value.Split(_partSplitter, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != count)
            {
                error = $"Expected {count} values but had {parts.Length}.";
                return false;
            }

            for (var i = 0; i < parts.Length; i++)
            {
                var part = parts[i];
                if (!float.TryParse(part, NumberStyles.Any, CultureInfo.InvariantCulture, out float v))
                {
                    error = $"Value {i + 1} could not be parsed as float.";
                    return false;
                }

                values[i] = v;
            }

            return true;
        }

        #endregion
    }
}
