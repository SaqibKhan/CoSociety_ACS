#include "pch.h"
#include "MainPage.h"
#include "MainPage.g.cpp"
#include"Chat.g.h"
#include <typeinfo> 
#include <winrt/Windows.UI.Xaml.Interop.h>
using namespace winrt;
using namespace Windows::UI::Xaml;

namespace winrt::ACS_Demo::implementation
{
    MainPage::MainPage()
    {
        InitializeComponent();
    }

    int32_t MainPage::MyProperty()
    {
        throw hresult_not_implemented();
    }

    void MainPage::MyProperty(int32_t /* value */)
    {
        throw hresult_not_implemented();
    }

    void MainPage::ClickHandler_TextChat(IInspectable const&, RoutedEventArgs const&)
    {
       this->Frame().Navigate(xaml_typename<Chat>());       
    }

    void MainPage::ClickHandler_Voice(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args)
    {
        
    }
    void MainPage::ClickHandler_Video(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args)
    {
        btnOpenVideo().Content(box_value(L"Video Chat Window"));
    }
}
