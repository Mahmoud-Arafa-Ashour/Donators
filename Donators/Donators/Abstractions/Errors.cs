namespace Donators.Abstractions;

public static class Errors
{
    public class UserErrors
    {
        public static readonly Error InvalidCredentionals =
            new Error("User.InvalidCredentionals", "Invalid Username or password", StatusCodes.Status401Unauthorized);
        public static readonly Error DuplicateEmail =
            new Error("User.DuplicatedEmail", "Another User with the same Email", StatusCodes.Status409Conflict);
        public static readonly Error EmailNotConfirmed =
            new Error("User.EmailNotConfirmed", "EmailNotConfirmed", StatusCodes.Status401Unauthorized);
        public static readonly Error DuplicateConfirmation =
            new Error("User.DuplicateConfirmation", "This email had been confirmed before", StatusCodes.Status409Conflict);
        public static readonly Error NotFound =
            new Error("User.NotFound", "This Id is not valid", StatusCodes.Status404NotFound);
        public static readonly Error InvalidCode =
            new Error("User.InvalidCode", "InvalidCode", StatusCodes.Status400BadRequest);
    }
    public class TokenErrors
    {
        public static readonly Error EmptyToken =
            new Error("NotFound", "Null Refrence", StatusCodes.Status404NotFound);
    }
    public class ItemErrors
    {
        public static readonly Error Emptyitem =
            new Error("Item.NotFound", "No Item with this id", StatusCodes.Status404NotFound);
    }
    public class CategoryErrors
    {
        public static readonly Error EmptyCategory =
            new Error("Category.NotFound", "No Category with this id", StatusCodes.Status404NotFound);
    }
    public class DiscountErrors
    {
        public static readonly Error InvalidPrice =
            new Error("Discount.InvalidPrice", "New Price Must be less than Old Price", StatusCodes.Status409Conflict);
        public static readonly Error InvalidDateRange =
            new Error("Discount.InvalidDateRange", "EndDate must be more than Start Date", StatusCodes.Status409Conflict);
        public static readonly Error ExistingDiscount =
            new Error("Discount.ExistingDiscount", "This item have a discount already", StatusCodes.Status409Conflict);
        public static readonly Error InvalidDiscount =
            new Error("Discount.Invalid", "No Discount Match this data", StatusCodes.Status409Conflict);
    }
    public class OfferErrors
    {
        public static readonly Error EmptyOffer =
            new Error("Offer.NotFound", "No Offer with this id", StatusCodes.Status404NotFound);
        public static readonly Error NotValidOffers =
            new Error("Offer.NotValid", "No Available Offers right now", StatusCodes.Status404NotFound);
    }
    public static class OfferItemErrors
    {
        public static readonly Error EmptyOfferItem =
            new Error("OfferItem.NotFound", "No OfferItem with this id", StatusCodes.Status404NotFound);
    }
}
