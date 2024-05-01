using BasicLoader.Interface;
using STPLoader.Implementation.Model;
using STPLoader.Implementation.Model.Entity;
using STPLoader.Interface;

namespace STPLoader.Implementation.Parser
{
    /// <summary>
    /// 
    /// </summary>
    public class StpParser : IParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public IStpModel Parse(Stream stream)
        {
            return new StpFile { Header = ParseHeader(FindHeader(stream)), Data = ParseData(FindData(stream)) };
        }
   
        public IModel Parse(ILoader loader)
        {
            return Parse(loader.Load());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Stream FindData(Stream stream)
        {
            string start = "DATA;";
            string end = "ENDSEC;";

			return ParseHelper.FindSection(stream, start, end);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        Stream FindHeader(Stream stream)
        {
            string start = "HEADER;";
            string end = "ENDSEC;";

			return ParseHelper.FindSection(stream, start, end);
        }

        IModel IParser.Parse(Stream stream)
        {
            throw new NotImplementedException();
        }

        public CADType CAD => CADType.STP;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStream"></param>
        /// <returns></returns>
        StpData ParseData(Stream dataStream)
        {
            try
            {
                IEnumerable<string> lines = ParseHelper.ParseBody(dataStream);

                IEnumerable<Entity> entities = lines.Select(ParseHelper.ParseBodyLine);
                return new StpData(entities.ToDictionary(entity => entity.Id));
            }
            catch (Exception e)
            {
                throw new ParsingException("Error while parsing file. "+e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="headerStream"></param>
        /// <returns></returns>
        StpHeader ParseHeader(Stream headerStream)
        {
			StpHeader header = new StpHeader ();
            IList<string> descriptionList = ParseHelper.ParseHeaderLine(headerStream, "FILE_DESCRIPTION");
            header.Description = new FileDescription(ParseHelper.ParseList(descriptionList[0]), descriptionList[1]);
            IList<string> nameList = ParseHelper.ParseHeaderLine(headerStream, "FILE_NAME");
            header.Name = new FileName(nameList[0], ParseHelper.ParseDate(nameList[1]), 
                ParseHelper.ParseList(nameList[2]), ParseHelper.ParseList(nameList[3]), nameList[4], nameList[5], nameList[6]);
            IList<string> schemaList = ParseHelper.ParseHeaderLine(headerStream, "FILE_SCHEMA");
            header.Schema = new FileSchema(ParseHelper.ParseList(schemaList[0]));
			return header;
        }

    }

}