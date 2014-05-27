using System.Net.Http;
using Microsoft.WindowsAzure.MobileServices;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace devcon14demo
{
    public class NewsItem
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "approved")]
        public bool Approved { get; set; }
    }

    public sealed partial class MainPage : Page
    {
        private MobileServiceCollection<NewsItem, NewsItem> items;
        private IMobileServiceTable<NewsItem> newsTable = App.MobileService.GetTable<NewsItem>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void InsertNewsItem(NewsItem newsItem)
        {
            // This code inserts a new NewsItem into the database. When the operation completes
            // and Mobile Services has assigned an Id, the item is added to the CollectionView
            await newsTable.InsertAsync(newsItem);
            items.Add(newsItem);
        }

        private async void RefreshNewsItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                // This code refreshes the entries in the list view by querying the NewsItems table.
                // The query excludes completed NewsItems
                items = await newsTable.ToCollectionAsync();
                this.ButtonSave.IsEnabled = true;
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
            }
        }

        private async void UpdateCheckedNewsItem(NewsItem item)
        {
            // This code takes a freshly completed NewsItem and updates the database. When the MobileService 
            // responds, the item is removed from the list 
            await newsTable.UpdateAsync(item);
        }

        private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshNewsItems();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            var newsItem = new NewsItem { Title = TextInputTitle.Text, Text = TextInputText.Text };
            InsertNewsItem(newsItem);
        }

        private void CheckBoxComplete_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            NewsItem item = cb.DataContext as NewsItem;
            UpdateCheckedNewsItem(item);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RefreshNewsItems();
        }

        private async void ButtonApproveAll_Click(object sender, RoutedEventArgs e)
        {
            var message = "";

            try
            {
                var result =
                    await App.MobileService.InvokeApiAsync<int>("approveAll", HttpMethod.Post, null);
                RefreshNewsItems();
                return;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            var dialog = new MessageDialog(message);
            dialog.Commands.Add(new UICommand("OK"));
            await dialog.ShowAsync();
        }
    }
}
