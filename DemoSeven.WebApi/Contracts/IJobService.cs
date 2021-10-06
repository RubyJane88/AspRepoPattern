namespace DemoSeven.WebApi.Contracts
{
    //for Hangfire
    public interface IJobService
    {
        void FireAndForgetJob();
        void RecurringJob();
        void DelayedJob();
        void ContinuationJob();
    }
}