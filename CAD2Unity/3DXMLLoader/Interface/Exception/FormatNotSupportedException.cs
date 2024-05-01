﻿namespace ThreeDXMLLoader.Interface.Exception
{
    /// <summary>
    /// This exception will be thrown, when a 3dxml format is not supported.
    /// </summary>
    class FormatNotSupportedException : System.Exception
    {
        public FormatNotSupportedException(string msg) : base(msg)
        {
            
        }

    }
}
