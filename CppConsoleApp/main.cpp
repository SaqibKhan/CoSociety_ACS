#include "pch.h"
#include <iostream>
#include <winrt/AuthoringDemo.h>

using namespace winrt;
using namespace Windows::Foundation;
using namespace std;

int main()
{
   /* init_apartment();
    Uri uri(L"http://aka.ms/cppwinrt");
    printf("Hello, %ls!\n", uri.AbsoluteUri().c_str());*/

    init_apartment();

    AuthoringDemo::Example ex;
    ex.SampleProperty(42);
    std::wcout << ex.SampleProperty() << endl;
    wcout << ex.SayHello().c_str() << endl;


    AuthoringDemo::Person person;

    person.FirstName(L"test Firat name ");
    person.LastName(L"Test Last Name");

	auto test = static_cast<AuthoringDemo::Person>(person.GetPerson());
    wcout << test.FirstName().c_str()<<" " << test.LastName().c_str() << endl;

    
}
