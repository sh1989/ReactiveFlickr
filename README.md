This is the sample code for my "Keep it Responsive! Cross-Platform MVVM with ReactiveUI" talk.

ReactiveFlickr is an application which is built for WPF and Android (using Xamarin MonoDroid). It queries the Flickr photo API based on a user-defined search term. It is built using the MVVM architecture, with ReactiveUI.

# Project Structure
* ReactiveFlickr.csproj - contains model and view model code. This is a PCL and is platform-independent.
- Desktop.csproj - application shell and view for WPF.
- Mobile.csproj - application shell and view for Android, using Xamarin MonoDroid.
- Test.csproj - an MSTest project which tests `ReactiveFlickr.csproj`
