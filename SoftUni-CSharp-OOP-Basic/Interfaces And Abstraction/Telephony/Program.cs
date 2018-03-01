using System;
using System.Linq;

class Program
{
    static void Main()
    {
        var calls = Console.ReadLine().Split(' ');

        foreach (var number in calls)
        {
            try
            {
                var phone = new Smartphone();
                phone.PhoneNumber = number;
                Console.WriteLine(phone.Call() + number);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        var urls = Console.ReadLine().Split(' ');

        if (urls.Contains(String.Empty))
        {
            Console.WriteLine("Browsing: !");
            return;
        }

        foreach (var url in urls)
            {
                try
                {
                    var phone = new Smartphone();
                    phone.Url = url;
                    Console.WriteLine(phone.Browse() + url + "!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
