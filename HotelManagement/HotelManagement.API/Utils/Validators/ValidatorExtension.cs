using FluentValidation.Results;

namespace HotelManagement.API.Utils.Validators
{
	public static class ValidatorExtension
	{
		public static string GetErrorMessage(this ValidationResult validationResult)
		{
			string errorMessage = "";

			foreach (var error in validationResult.Errors)
				errorMessage += error.ErrorMessage + "\n";

			return errorMessage;
		}
	}
}