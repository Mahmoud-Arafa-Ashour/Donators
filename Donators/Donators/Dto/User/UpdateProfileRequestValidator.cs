namespace Donators.Contracts.User
{
    public class UpdateProfileRequestValidator : AbstractValidator<UpdateProfileRequest>
    {
        //string Name, string Adress, string PhoneNumber, string ResturnatName
        public UpdateProfileRequestValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty();
            RuleFor(x => x.Adress)
                .NotEmpty();
            RuleFor(x => x.ResturnatName)
                .NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Length(11 , 100)
                .WithMessage("Can not be less than 11 digits");
        }
    }
}
