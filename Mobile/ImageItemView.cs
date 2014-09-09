using System;
using ReactiveUI;
using ReactiveFlickr;
using Android.Views;
using Android.Content;
using Android.Widget;
using Splat;

namespace Mobile
{
	public class ImageItemView : ReactiveViewHost<SearchResultViewModel>
	{
		public ImageItemView (SearchResultViewModel viewModel, Context ctx, ViewGroup parent)
			: base(ctx, Resource.Layout.ImageItem, parent)
		{
			ViewModel = viewModel;

			this.OneWayBind(ViewModel, vm => vm.Title, v => v.Title.Text);
			this.WhenAnyValue(vm => vm.ViewModel.Image)
				.Subscribe(x => Image.SetImageDrawable(x.ToNative()));
		}

		public ImageView Image { get; private set; }
		public TextView Title { get; private set; }
	}
}