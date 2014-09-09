using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveFlickr;
using ReactiveUI;

namespace Mobile
{
	[Activity (Label = "Mobile", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ReactiveActivity<FlickrSearchViewModel>
	{
		public EditText SearchText { get; private set; }
		public Button SearchButton { get; private set; }
		public ListView ImagesList { get; private set; }

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var service = new FlickrImageService();
			ViewModel = new FlickrSearchViewModel(service);

			SearchText = FindViewById<EditText> (Resource.Id.searchText);
			SearchButton = FindViewById<Button>(Resource.Id.searchButton);
			ImagesList = FindViewById<ListView> (Resource.Id.imagesList);

			this.Bind(ViewModel, vm => vm.SearchText, v => v.SearchText.Text);
			this.BindCommand(ViewModel, vm => vm.Search, v => v.SearchButton);

			var adapter = new ReactiveListAdapter<SearchResultViewModel>(
				ViewModel.Images,
				(viewModel, parent) => new ImageItemView(viewModel, this, parent));
			ImagesList.Adapter = adapter;
		}
	}
}