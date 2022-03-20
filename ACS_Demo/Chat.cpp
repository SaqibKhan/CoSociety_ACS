#include "pch.h"
#include "Chat.h"
#include"MainPage.g.h"
#include <winrt/Windows.UI.Xaml.Interop.h>


#if __has_include("Chat.g.cpp")
#include "Chat.g.cpp"
#endif

using namespace winrt;
using namespace Windows::UI::Xaml;

namespace winrt::ACS_Demo::implementation
{
    Chat::Chat()
    {
        InitializeComponent();
    }

    int32_t Chat::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void Chat::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    void Chat::ClickHandler(IInspectable const&, RoutedEventArgs const&)
    {
        btn_CreateUser().Content(box_value(L"Clicked"));
    }
}


void winrt::ACS_Demo::implementation::Chat::btnBack_Click(winrt::Windows::Foundation::IInspectable const& sender, winrt::Windows::UI::Xaml::RoutedEventArgs const& e)
{
   this->Frame().Navigate(xaml_typename<MainPage>());


  // auto client = new CommunicationIdentityClient(connectionString);
}
