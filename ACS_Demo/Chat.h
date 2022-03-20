#pragma once

#include "Chat.g.h"

namespace winrt::ACS_Demo::implementation
{
    struct Chat : ChatT<Chat>
    {
        Chat();

        int32_t MyProperty();
        void MyProperty(int32_t value);

        void ClickHandler(Windows::Foundation::IInspectable const& sender, Windows::UI::Xaml::RoutedEventArgs const& args);
        void btnBack_Click(winrt::Windows::Foundation::IInspectable const& sender,  Windows::UI::Xaml::RoutedEventArgs const& e);
       };
}

namespace winrt::ACS_Demo::factory_implementation
{
    struct Chat : ChatT<Chat, implementation::Chat>
    {
    };
}
