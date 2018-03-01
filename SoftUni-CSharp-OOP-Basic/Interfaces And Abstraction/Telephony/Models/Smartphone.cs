using System;
using System.Linq;

public class Smartphone : ICallable, IBrowsable
{
    private string phoneNumber;
    private string url;

    public string PhoneNumber
    {
        get => phoneNumber;
        set
        {
            bool isValid = value.Any(c => Char.IsDigit(c));

            if (isValid)
            {
                phoneNumber = value;
            }
            else
            {
                throw new ArgumentException("Invalid number!");
            }
        }
    }

    public string Call()
    {
        return "Calling... ";
    }

    public string Url
    {
        get => url;
        set
        {
            bool isNotValid = value.Any(c => Char.IsDigit(c));

            if (isNotValid)
            {
                throw new ArgumentException("Invalid URL!");
            }

            url = value;
        }
    }

    public string Browse()
    {
        return "Browsing: ";
    }
}