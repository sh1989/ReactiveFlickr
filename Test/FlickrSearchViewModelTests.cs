using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ReactiveFlickr;

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
                var vm = new FlickrSearchViewModel(Substitute.For<IImageService>());

                Assert.IsFalse(vm.Search.CanExecute(null));
            }

            [TestMethod]
            public void IsLoading_Is_False()
            {
                var vm = new FlickrSearchViewModel(Substitute.For<IImageService>());

                Assert.IsFalse(vm.IsLoading);
            }

            [TestMethod]
            public void ShowError_Is_False()
            {
                var vm = new FlickrSearchViewModel(Substitute.For<IImageService>());

                Assert.IsFalse(vm.ShowError);
            }
        }

        [TestClass]
        public class When_Search_Text_Specified
        {
            [TestMethod]
            public void Search_CanExecute_Is_True()
            {
                var vm = new FlickrSearchViewModel(Substitute.For<IImageService>())
                {
                    SearchText = "testing"
                };

                Assert.IsTrue(vm.Search.CanExecute(null));
            }
        }
    }
}
