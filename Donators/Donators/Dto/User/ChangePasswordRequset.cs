using System.Text.RegularExpressions;

namespace Donators.Contracts.User
{
    public record ChangePasswordRequset(string CurrentPassword ,string NewPassword);
}
