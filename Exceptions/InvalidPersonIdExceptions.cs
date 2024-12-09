using System.Diagnostics.Contracts;

namespace Exceptions
{
	public class InvalidPersonIdExceptions : ArgumentException
	{

		public InvalidPersonIdExceptions(){}
		public InvalidPersonIdExceptions(string message) : base(message) { }
		public InvalidPersonIdExceptions(string message , Exception innerException) : base(message) { }

	}
}
