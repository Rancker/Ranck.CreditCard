namespace Ranck.CreditCard.WebService.Framework
{
    public abstract class ServiceResult
    {
        public bool Success { get; set; }
        public List<ErrorResult> Errors { get; set; }
    }

    public class ServiceResult<TResult> : ServiceResult
    {
        public TResult Result { get; set; }

        public ServiceResult(TResult result)
            : this(success: true, result: result, errors: null)
        { }

        public ServiceResult(ErrorResult error)
            : this(success: false, result: default(TResult), errors: new List<ErrorResult>() { error })
        {
        }

        public ServiceResult(List<ErrorResult> errors)
            : this(success: false, result: default(TResult), errors: errors)
        {
        }

        public ServiceResult(bool success, TResult result, List<ErrorResult> errors)
        {
            Success = success;
            Result = result;
            Errors = errors;
        }
    }
}
