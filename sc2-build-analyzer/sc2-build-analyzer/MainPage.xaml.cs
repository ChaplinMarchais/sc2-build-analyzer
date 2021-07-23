using System;
using System.Reflection;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using sc2_build_analyzer.core.Uwp;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

#nullable enable
namespace sc2_build_analyzer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private const string _viewTypePrefix = @"NavigationView.Views";

        private NavigationViewItem _lastViewItem;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (!(args.InvokedItemContainer is NavigationViewItem item) || item == _lastViewItem)
                return;

            var selectedItemTag = item.Tag?.ToString() ?? "StngsView";
            if (!TryNavigateToView(selectedItemTag)) return;

            _lastViewItem = item;
        }

        private bool TryNavigateToView(string selectedItemTag) 
            => ResolveViewFromTag(selectedItemTag, out var view) 
                    && ContentFrame.Navigate(view, null, new EntranceNavigationTransitionInfo());

        private bool ResolveViewFromTag(string viewTag, out Type? viewType)
        {
            viewType = Assembly.GetExecutingAssembly()?.GetType($"{_viewTypePrefix}.{viewTag}");

            return viewType != null;
        }
        
        private void NavigationView_OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
                ContentFrame.GoBack();
        }

        private void ContentFrame_OnNavigationFailed(object sender, NavigationFailedEventArgs e)
            => throw new NavigationException(e.Exception.Message, e.SourcePageType.Name);
    }
}
