using Microsoft.AspNetCore.Components;

namespace AtomyWeb.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _nav;

        public ReviewService(HttpClient http, NavigationManager nav)
        {
            _http = http;
            _nav = nav;
        }

        public Task<IEnumerable<string>> GetReviewsAsync() =>
            _http.GetFromJsonAsync<IEnumerable<string>>($"{_nav.BaseUri}data/reviews.json");
    }
}
