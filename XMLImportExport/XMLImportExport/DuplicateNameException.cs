using System;

namespace XMLImportExport
{
    public class DuplicateNameException : FormatException
    {
        public DuplicateNameException(string message, Exception innerException) : base(message, innerException) { }
    }
}
