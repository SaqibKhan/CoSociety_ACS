using ABI.Windows.UI.Notifications;

namespace AuthoringDemo;

public sealed class Person
{
    public  string FirstName { get; set; }
    public  string LastName { get; set; }
    
    public Person GetPerson()
    {
        return this;
    }
}