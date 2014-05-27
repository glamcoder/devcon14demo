using System;
using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;


namespace devcon14demo
{
	[Activity (MainLauncher = true, 
	           Icon="@drawable/ic_launcher", Label="@string/app_name",
	           Theme="@style/AppTheme")]
	public class NewsActivity : Activity
	{
		//Mobile Service Client reference
		private MobileServiceClient client;

		//Mobile Service Table used to access data
		private IMobileServiceTable<NewsItem> newsTable;

		//Adapter to sync the items list with the view
		private NewsItemAdapter adapter;

		//EditText containing the "New News" text
        private EditText textNewNewsTitle;
        private EditText textNewNewsText;

		//Progress spinner to use for table operations
		private ProgressBar progressBar;

		const string applicationURL = @"https://devcon14demo.azure-mobile.net/";
		const string applicationKey = @"AyPuLEqReqIBMyUDGtoxNJdTFrciiU16";

		protected override async void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Activity_News);

			progressBar = FindViewById<ProgressBar> (Resource.Id.loadingProgressBar);

			// Initialize the progress bar
			progressBar.Visibility = ViewStates.Gone;

			// Create ProgressFilter to handle busy state
			var progressHandler = new ProgressHandler ();
			progressHandler.BusyStateChange += (busy) => {
				if (progressBar != null) 
					progressBar.Visibility = busy ? ViewStates.Visible : ViewStates.Gone;
			};

			try {
				CurrentPlatform.Init ();

				// Create the Mobile Service Client instance, using the provided
				// Mobile Service URL and key
				client = new MobileServiceClient (
					applicationURL,
					applicationKey, progressHandler);

				// Get the Mobile Service Table instance to use
				newsTable = client.GetTable <NewsItem> ();

				textNewNewsTitle = FindViewById<EditText> (Resource.Id.textNewNewsTitle);
                textNewNewsText = FindViewById<EditText>(Resource.Id.textNewNewsText);

				// Create an adapter to bind the items with the view
				adapter = new NewsItemAdapter (this, Resource.Layout.Row_List_News);
				var listViewNews = FindViewById<ListView> (Resource.Id.listViewNews);
                listViewNews.Adapter = adapter;

				// Load the items from the Mobile Service
				await RefreshItemsFromTableAsync ();

			} catch (Java.Net.MalformedURLException) {
				CreateAndShowDialog (new Exception ("There was an error creating the Mobile Service. Verify the URL"), "Error");
			} catch (Exception e) {
				CreateAndShowDialog (e, "Error");
			}
		}

		//Initializes the activity menu
		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.activity_main, menu);
			return true;
		}

		//Select an option from the menu
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
			if (item.ItemId == Resource.Id.menu_refresh) {
				OnRefreshItemsSelected ();
			}
			return true;
		}

		// Called when the refresh menu opion is selected
		async void OnRefreshItemsSelected ()
		{
			await RefreshItemsFromTableAsync ();
		}

		//Refresh the list with the items in the Mobile Service Table
		async Task RefreshItemsFromTableAsync ()
		{
			try {
				var list = await newsTable.Where (item => item.Approved == true).ToListAsync ();

				adapter.Clear ();

				foreach (NewsItem current in list)
					adapter.Add (current);

			} catch (Exception e) {
				CreateAndShowDialog (e, "Error");
			}
		}

		public async Task CheckItem (NewsItem item)
		{
			if (client == null) {
				return;
			}

			// Set the item as completed and update it in the table
			item.Approved = true;
			try {
				await newsTable.UpdateAsync (item);
				if (item.Approved)
					adapter.Remove (item);

			} catch (Exception e) {
				CreateAndShowDialog (e, "Error");
			}
		}

		[Java.Interop.Export()]
		public async void AddItem (View view)
		{
			if (client == null || string.IsNullOrWhiteSpace (textNewNewsTitle.Text)) {
				return;
			}

			// Create a new item
			var item = new NewsItem {
				Title = textNewNewsTitle.Text,
                Text = textNewNewsText.Text,
				Approved = false
			};

			try {
				// Insert the new item
				await newsTable.InsertAsync (item);

				if (!item.Approved) {
					adapter.Add (item);
				}
			} catch (Exception e) {
				CreateAndShowDialog (e, "Error");
			}

			textNewNewsTitle.Text = "";
		}

		void CreateAndShowDialog (Exception exception, String title)
		{
			CreateAndShowDialog (exception.Message, title);
		}

		void CreateAndShowDialog (string message, string title)
		{
			AlertDialog.Builder builder = new AlertDialog.Builder (this);

			builder.SetMessage (message);
			builder.SetTitle (title);
			builder.Create ().Show ();
		}

		class ProgressHandler : DelegatingHandler
		{
			int busyCount = 0;

			public event Action<bool> BusyStateChange;

			#region implemented abstract members of HttpMessageHandler

			protected override async Task<HttpResponseMessage> SendAsync (HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
			{
				 //assumes always executes on UI thread
				if (busyCount++ == 0 && BusyStateChange != null)
					BusyStateChange (true);

				var response = await base.SendAsync (request, cancellationToken);

				// assumes always executes on UI thread
				if (--busyCount == 0 && BusyStateChange != null)
					BusyStateChange (false);

				return response;
			}

			#endregion

		}
	}
}


