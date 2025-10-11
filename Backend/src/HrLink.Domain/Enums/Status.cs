namespace HrLink.Domain.Enums;

public enum Status : byte
{
    Wait,
    Cancelled,
    Offer,
    Accepted,
    Refusal,
    OfferRejection
}