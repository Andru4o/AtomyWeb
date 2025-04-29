namespace AtomyWeb.Services
{
    public class ProductModel
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string DetailUrl { get; set; }
    }

    public class ReviewModel
    {
        public string Text { get; set; }
        public string Author { get; set; }
        public bool Active { get; set; }
    }

    public class RegistrationModel
    {
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Email { get; set; }
        public string PickupPoint { get; set; }
        public string Phone { get; set; }
        public string PreferredContact { get; set; }
        public bool AgreeToTerms { get; set; }
    }
}
