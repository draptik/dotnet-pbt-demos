// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable InvokeAsExtensionMethod
namespace PBT_Demos;

public class DomainTests
{
    [Fact]
    public void Format_never_contains_the_age_or_address_of_a_minor()
    {
        ModelGen.Minors
            .Sample(
                p =>
                    p.Format()
                        .Should()
                        .Be($"{p.Name.Value} is a minor"));
    }
    
    [Fact]
    public void Format_always_contains_the_age_and_address_and_name_of_adults()
    {
        ModelGen.Adults
            .Sample(
                p =>
                    p.Format()
                        .Should()
                        .Contain(p.Name.Value)
                        .And
                        .Contain(p.Address.Value)
                        .And
                        .Contain(p.Age.Value.ToString()));
    }


    public static class ModelGen
    {
        private const string AlphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int MinAge = 0;
        private const int AdultAge = 18;
        private const int MaxAge = 150;

        public static readonly Gen<Name> Names =
            Gen.String[AlphaNumeric]
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => new Name(s));

        public static readonly Gen<Address> Addresses =
            Gen.String[AlphaNumeric]
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => new Address(s));

        public static readonly Gen<Age> MinorAges =
            Gen.Int[MinAge, AdultAge - 1]
                .Select(i => new Age(i));

        public static readonly Gen<Age> AdultAges =
            Gen.Int[AdultAge, MaxAge]
                .Select(i => new Age(i));

        public static readonly Gen<Person> Minors =
            Gen
                .Select(
                    Names,
                    Addresses,
                    MinorAges,
                    (name, address, age) =>
                        new Person(name, address, age));

        public static readonly Gen<Person> Adults =
            Gen
                .Select(
                    Names,
                    Addresses,
                    AdultAges,
                    (name, address, age) =>
                        new Person(name, address, age));
    }
}