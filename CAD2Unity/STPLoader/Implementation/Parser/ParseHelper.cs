using System.Globalization;
using System.Text.RegularExpressions;
using STPLoader.Implementation.Model.Entity;

namespace STPLoader.Implementation.Parser
{
    /// <summary>
    /// 
    /// </summary>
	public static class ParseHelper
	{
		static readonly IDictionary<string, Type> EntityTypes = new Dictionary<string, Type>()
        {
            {"CARTESIAN_POINT", typeof(CartesianPoint)},
            {"DIRECTION", typeof(DirectionPoint)},
            {"VERTEX_POINT", typeof(VertexPoint)},
            {"VECTOR", typeof(VectorPoint)},
            {"AXIS2_PLACEMENT_3D", typeof(Axis2Placement3D)},
            {"ORIENTED_EDGE", typeof(OrientedEdge)},
            {"FACE_BOUND", typeof(FaceBound)},
            {"CLOSED_SHELL", typeof(ClosedShell)},
            {"ADVANCED_FACE", typeof(AdvancedFace)},
            {"B_SPLINE_CURVE_WITH_KNOTS", typeof(BSplineCurveWithKnots)},
            {"CIRCLE", typeof(Circle)},
            {"CONICAL_SURFACE", typeof(ConicalSurface)},
            {"CYLINDRICAL_SURFACE", typeof(CylindricalSurface)},
            {"TOROIDAL_SURFACE", typeof(ToroidalSurface)},
            {"EDGE_CURVE", typeof(EdgeCurve)},
            {"EDGE_LOOP", typeof(EdgeLoop)},
            {"FACE_OUTER_BOUND", typeof(FaceOuterBound)},
            {"LINE", typeof(Line)},
            {"PLANE", typeof(Plane)}
        };

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public static Stream FindSection(Stream stream, string start, string end)
        {
            stream.Position = 0;
			MemoryStream ms = new MemoryStream();
			StreamWriter sw = new StreamWriter(ms);
			StreamReader reader = new StreamReader(stream);
			bool inSection = false;

			while (reader.ReadLine() is { } line)
			{
				if (line.Equals(start))
				{
					inSection = true;
					continue;
				}
				if (line.Equals(end))
				{
					inSection = false;
					continue;
				}
				if (inSection)
				{
					sw.WriteLine(line);
				}
			}
			sw.Flush();

			return ms;
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="lineStart"></param>
        /// <returns></returns>
	    public static IList<string> ParseHeaderLine(Stream stream, string lineStart)
        {
            stream.Position = 0;
			StreamReader reader = new StreamReader(stream);
			while (reader.ReadLine() is { } line)
	        {
	            if (line.StartsWith(lineStart))
	            {
	                string? listString = line.Substring(lineStart.Length, line.Length - lineStart.Length - 1);
	                return ParseList(listString);
	            }
	        }

            throw new ParsingException("Can't find "+ lineStart);
	    }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="listString"></param>
        /// <returns></returns>
	    public static IList<string> ParseList(string listString)
        {
            return ParseList<string>(listString);
        }

        public static IList<T> ParseList<T>(string listString)
        {
            // remove parenthesis
            string? inner = listString.Remove(listString.Length - 1).Substring(1);
            return Regex.Split(inner, @",(?![^\(]*\))").Select(x => (T)Convert.ChangeType(x, typeof(T), CultureInfo.InvariantCulture)).ToList();
        }

        /// <summary>
        /// Parse ISO 8601 date string
        /// </summary>
        /// <param name="dateString"></param>
        /// <returns></returns>
	    public static DateTime ParseDate(string dateString)
	    {
            DateTime d;
            DateTime.TryParseExact(dateString, "s", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out d);
            
            return d;
	    }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStream"></param>
        /// <returns></returns>
	    public static IEnumerable<string> ParseBody(Stream dataStream)
	    {
            dataStream.Position = 0;
            StreamReader reader = new StreamReader(dataStream);
            IList<string> lines = new List<string>();
            while (reader.ReadLine() is { } line)
            {
                lines.Add(line);
            }

	        return lines;
	    }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
	    public static Entity ParseBodyLine(string line)
        {
            string[]? splitted = line.Split('=');
            // remove = from id
            long id = ParseId(splitted[0]);

            string? rightPart = splitted[1].Remove(splitted[1].IndexOf(';')).Trim();
            int positionOfList = rightPart.IndexOf('(');
            string? type = rightPart.Substring(0, positionOfList);
            IList<string> list = ParseList(rightPart.Substring(positionOfList));

            return CreateEntity(type, id, list);
	    }

        static Entity CreateEntity(string type, long id, IList<string> list)
        {
            if (SpecificEntity(type))
            {
                Entity? entity = (Entity) Activator.CreateInstance(EntityTypes[type]);
                entity.Id = id;
                entity.Type = type;
                entity.Data = list;
                entity.Init();
                return entity;
            }
            return CreateEntity<Entity>(type, id, list);
        }

        static T CreateEntity<T>(string type, long id, IList<string> list) where T : Entity, new()
        {
            T entity = new T { Id = id, Type = type, Data = list };
            entity.Init();
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
	    public static long ParseId(string id)
	    {
		    return id is "$" or "*" ? 0 : long.Parse(id.Substring(1));
	    }

        public static bool SpecificEntity(string entityName)
        {
            return EntityTypes.ContainsKey(entityName);
        }

        public static string ParseString(string data)
        {
            return data.Trim('\'');
        }

        public static bool ParseBool(string data)
        {
            return data == ".T.";
        }

        public static T Parse<T>(string data)
        {
            Type type = typeof (T);
            if (type == typeof (bool))
            {
                return (T)Convert.ChangeType(ParseBool(data), typeof(T), CultureInfo.InvariantCulture);
            }

            if (type == typeof (string))
            {
	            return (T)Convert.ChangeType(ParseString(data), typeof(T), CultureInfo.InvariantCulture);
            }
            return (T)Convert.ChangeType(data, typeof(T), CultureInfo.InvariantCulture);
        }
	}

}

