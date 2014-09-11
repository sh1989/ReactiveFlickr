
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ReactiveFlickr.Mobile
{
	public class loading_rotate : View
	{
		public loading_rotate (Context context) :
			base (context)
		{
			Initialize ();
		}

		public loading_rotate (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{
			Initialize ();
		}

		public loading_rotate (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
	}
}

