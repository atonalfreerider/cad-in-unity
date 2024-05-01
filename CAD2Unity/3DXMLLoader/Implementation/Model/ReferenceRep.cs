﻿using ThreeDXMLLoader.Implementation.Model.ModelInterna;

namespace ThreeDXMLLoader.Implementation.Model
{
    class ReferenceRep
    {
        public string ReferenceType { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Version { get; set; }

        public string Usage { get; set; }
       
        /// <summary>
        /// maps 3dxmls ids to a shell
        /// </summary>
        public Shell Shell{ get; set; }
    }
}
