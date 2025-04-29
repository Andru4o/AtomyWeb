namespace AtomyWeb.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<string>> GetReviewsAsync();
    }
}
