﻿using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ReactiveFlickr;
using ReactiveUI;
using Android.Views.Animations;

namespace ReactiveFlickr.Mobile
{
	[Activity (Label = "ReactiveFlickr", MainLauncher = true, Icon = "@drawable/icon", WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
	public class MainActivity : ReactiveActivity<FlickrSearchViewModel>
	{
		public EditText SearchText { get; private set; }
		public Button SearchButton { get; private set; }
		public ListView ImagesList { get; private set; }

		private IMenuItem loadingItem;
		private ImageView loadingView;

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

			var loadingAnimation = AnimationUtils.LoadAnimation(this, Resource.Animation.loading_rotate);
			loadingAnimation.RepeatCount = Animation.Infinite;
			loadingAnimation.RepeatMode = RepeatMode.Restart;

			this.WhenAnyValue (v => v.ViewModel.IsLoading)
				.Subscribe (isLoading => {
					if (loadingItem != null)
					{
						if (isLoading)
						{
							loadingView.StartAnimation(loadingAnimation);
						}
						else
						{
							loadingView.ClearAnimation();
						}

						loadingItem.SetVisible(isLoading);
					}
				});
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			this.MenuInflater.Inflate (Resource.Menu.menu_reactiveflickr, menu);
			return true;
		}

		public override bool OnPrepareOptionsMenu (IMenu menu)
		{
			loadingItem = menu.FindItem (Resource.Id.loading);
			loadingView = (ImageView)loadingItem.ActionView;
			return true;
		}
	}
}