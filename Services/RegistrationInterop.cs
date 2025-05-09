using Microsoft.JSInterop;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AtomyWeb.Services
{
  public record RegistrationDto(
        string LastName,
        string FirstName,
        string MiddleName,
        string BirthDate,
        string Address,
        string Email,
        string Phone
    );
}
