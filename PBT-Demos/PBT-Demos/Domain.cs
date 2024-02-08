namespace PBT_Demos;

public class Person(Name name, Address address, Age age)
{
    public Name Name { get; } = name;
    public Address Address { get; } = address;
    public Age Age { get; } = age;

    // This is the method we want to test
    public string Format() =>
        Age.IsAdult()
            ? $"{Name.Value} lives at {Address.Value} is is {Age.Value} years old"
            : $"{Name.Value} is a minor";
}

public class Name
{
    public string Value { get; }
    
    public Name(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        Value = value;
    }
}

public class Address
{
    public string Value { get; }
    
    public Address(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));
        Value = value;
    }
}

public class Age
{
    private static readonly int MinAge = 0;
    private static readonly int MaxAge = 150;
    private static readonly int AdultAge = 18;
    
    public int Value { get; }
    
    public Age(int value)
    {
        if (value < MinAge)
        {
            throw new ArgumentException($"Age cannot be smaller than {MinAge}");
        }
        if (value > MaxAge)
        {
            throw new ArgumentException($"Age cannot be greater than {MaxAge}");
        }
        
        Value = value;
    }
    
    public bool IsAdult() => Value >= AdultAge;
}