using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Splat;
using System;
using System.Reactive.Linq;
using System.Reactive.Disposables;

namespace ReactiveFlickr.Test
{
    [TestClass]
    public class FlickrSearchViewModelTests
    {
        [TestClass]
        public class When_No_Search_Text_Specified
        {
            [TestMethod]
            public void Search_CanExecute_Is_False()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>());

                Assert.IsFalse(vm.Search.CanExecute(null));
            }

            [TestMethod]
            public void IsLoading_Is_False()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>());

                Assert.IsFalse(vm.IsLoading);
            }

            [TestMethod]
            public void ShowError_Is_False()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>());

                Assert.IsFalse(vm.ShowError);
            }

            [TestMethod]
            public void CanEnterSearchText_CanExecute_Is_True()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>());

                Assert.IsTrue(vm.CanEnterSearchText);
            }
        }

        [TestClass]
        public class When_Search_Text_Specified
        {
            [TestMethod]
            public void Search_CanExecute_Is_True()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>())
                {
                    SearchText = "testing"
                };

                Assert.IsTrue(vm.Search.CanExecute(null));
            }

            [TestMethod]
            public void IsLoading_Is_False()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>())
                {
                    SearchText = "testing"
                };

                Assert.IsFalse(vm.IsLoading);
            }

            [TestMethod]
            public void ShowError_Is_False()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>())
                {
                    SearchText = "testing"
                };

                Assert.IsFalse(vm.ShowError);
            }

            [TestMethod]
            public void CanEnterSearchText_Is_True()
            {
                var vm = new FlickrSearchViewModel(
                    Substitute.For<IImageService>())
                {
                    SearchText = "testing"
                };

                Assert.IsTrue(vm.CanEnterSearchText);
            }
        }

        [TestClass]
        public class When_Searching
        {
            FlickrSearchViewModel subject;
            IImageService service;

            [TestInitialize]
            public void SetUp()
            {
                service = Substitute.For<IImageService>();
                service.GetImages(Arg.Any<string>())
                    .Returns(Observable.Never<SearchResultViewModel>());

                subject = new FlickrSearchViewModel(service);
            }

            [TestMethod]
            public void Search_Executed_With_SearchText()
            {
                subject.SearchText = "cats";
                subject.Search.ExecuteAsync(null)
                    .Subscribe(_ =>
                    {
                        service.Received().GetImages("cats");
                    });
            }

            [TestMethod]
            public void Search_CanExecute_Is_False()
            {
                subject.SearchText = "cats";
                subject.Search.ExecuteAsync(null)
                    .Subscribe(_ =>
                    {
                        Assert.IsFalse(subject.Search.CanExecute(null));
                    });
            }

            [TestMethod]
            public void IsLoading_Is_True()
            {
                subject.SearchText = "cats";
                subject.Search.ExecuteAsync(null)
                    .Subscribe(_ =>
                    {
                        Assert.IsTrue(subject.IsLoading);
                    });
            }

            [TestMethod]
            public void ShowError_Is_False()
            {
                subject.SearchText = "cats";
                subject.Search.ExecuteAsync(null)
                    .Subscribe(_ =>
                    {
                        Assert.IsFalse(subject.ShowError);
                    });
            }

            [TestMethod]
            public void CanEnterSearchText_Is_False()
            {
                subject.SearchText = "cats";
                subject.Search.ExecuteAsync(null)
                    .Subscribe(_ => 
                    {
                        Assert.IsFalse(subject.CanEnterSearchText);
                    });
            }

            [TestMethod]
            public void Images_Is_Empty()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                subject.Search.ExecuteAsync(null)
                    .Subscribe(_ =>
                    {
                        Assert.AreEqual(0, subject.Images.Count);
                    });
            }
        }

        [TestClass]
        public class When_Search_Succeeds
        {
            SearchResultViewModel photo1;
            SearchResultViewModel photo2;
            FlickrSearchViewModel subject;

            [TestInitialize]
            public void SetUp()
            {
                photo1 = new SearchResultViewModel(
                    Substitute.For<IBitmap>(), "photo1");
                photo2 = new SearchResultViewModel(
                    Substitute.For<IBitmap>(), "photo2");

                var service = Substitute.For<IImageService>();
                service.GetImages(Arg.Any<string>())
                    .Returns(new[] { photo1, photo2, }.ToObservable());

                subject = new FlickrSearchViewModel(service)
                {
                    SearchText = "cats"
                };
            }

            [TestMethod]
            public void Images_Contains_Results()
            {
                subject.Search.Execute(null);

                Assert.AreEqual(photo1, subject.Images[0]);
                Assert.AreEqual(photo2, subject.Images[1]);
            }

            [TestMethod]
            public void ShowError_Is_False()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.IsFalse(subject.ShowError);
            }

            [TestMethod]
            public void IsLoading_Is_False()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.IsFalse(subject.IsLoading);
            }

            [TestMethod]
            public void CanEnterSearchText_Is_True()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.IsTrue(subject.CanEnterSearchText);
            }
        }

        [TestClass]
        public class When_Search_Fails
        {
            FlickrSearchViewModel subject;

            [TestInitialize]
            public void SetUp()
            {
                var service = Substitute.For<IImageService>();
                service.GetImages(Arg.Any<string>())
                    .Returns(x => 
                        {
                            return Observable.Throw<SearchResultViewModel>(new Exception());
                        });

                subject = new FlickrSearchViewModel(service);
            }

            [TestMethod]
            public void ShowError_Is_True()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.IsTrue(subject.ShowError);
            }

            [TestMethod]
            public void IsLoading_Is_False()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.IsFalse(subject.IsLoading);
            }

            [TestMethod]
            public void CanEnterSearchText_Is_True()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.IsTrue(subject.CanEnterSearchText);
            }

            [TestMethod]
            public void Images_Is_Empty()
            {
                subject.SearchText = "cats";
                subject.Search.Execute(null);
                Assert.AreEqual(0, subject.Images.Count);
            }
        }
    }
}
