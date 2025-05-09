namespace AtomyWeb.Services
{
  public class ProductModel
  {
    public string Category { get; set; }
    public string ImageUrl { get; set; }
    public string Name { get; set; }
    public string Price { get; set; }
    public string DetailUrl { get; set; }
    public string Details { get; set; }
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
    public string Address { get; set; }
    public string Phone { get; set; }
    public string PreferredContact { get; set; }
    public bool AgreeToTerms { get; set; }
  }

  public class DadataRequest
  {
    public string query { get; set; }
  }

  public class DadataResponse
  {
    public DadataResponseData[] suggestions { get; set; }
  }

  public class DadataResponseData
  {
    public string value { get; set; }
    public string unrestricted_value { get; set; }
  }
}
