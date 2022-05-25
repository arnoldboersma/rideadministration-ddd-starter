namespace RideAdministration.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Street { get; private set; } = String.Empty;
        public string City { get; private set; } = String.Empty;
        public string State { get; private set; } = String.Empty;
        public string Country { get; private set; } = String.Empty;
        public string ZipCode { get; private set; } = String.Empty;
        private Address() { }
        public Address(string street, string city, string state, string country, string zipcode)
        {
            if (string.IsNullOrEmpty(street))
            {
                throw new RideDomainException($"'{nameof(street)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new RideDomainException($"'{nameof(city)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(state))
            {
                throw new RideDomainException($"'{nameof(state)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(country))
            {
                throw new RideDomainException($"'{nameof(country)}' cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(zipcode))
            {
                throw new RideDomainException($"'{nameof(zipcode)}' cannot be null or empty.");
            }

            if (!Regex.Match(zipcode, @"^(?:NL-)?(?:[1-9]\d{3} ?(?:[A-EGHJ-NPRTVWXZ][A-EGHJ-NPRSTVWXZ]|S[BCEGHJ-NPRTVWXZ]))$").Success)
            {
                throw new RideDomainException($"'{nameof(zipcode)}' is not a valid zipcode.");
            }

            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}
