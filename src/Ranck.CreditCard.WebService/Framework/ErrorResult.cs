using Ranck.CreditCard.WebService.Framework.Enum;

namespace Ranck.CreditCard.WebService.Framework
{
    public class ErrorResult
    {
        public ErrorResult()
        {
        }

        public ErrorResult(ErrorType type, string message)
        {
            Type = type;
            Message = message;
        }

        public ErrorResult(ErrorType type)
        {
            Type = type;
            Message = type.ToString();
        }

        public ErrorType Type { get; set; }
        public string Message { get; set; }
    }
}
