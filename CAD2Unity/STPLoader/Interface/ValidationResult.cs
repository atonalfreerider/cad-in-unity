namespace STPLoader.Interface
{
	/// <summary>
	/// Validation result.
	/// </summary>
	public class ValidationResult
	{
		readonly bool _valid;
		readonly string _message;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="valid"></param>
        /// <param name="message"></param>
	    public ValidationResult (bool valid, string message = "")
		{
			_valid = valid;
			_message = message;
		}

	    public override string ToString()
	    {
	        return $"<ValidationResult({_valid}, {_message})>";
	    }
	}

}

