namespace WebPresentation.Tests.Models
{
    using WebPresentation.Models;
    using System;
    using NUnit.Framework;
    using NSubstitute;
    using System.Collections.Generic;
    using System.Web.Mvc;

    [TestFixture]
    public class ExternalLoginConfirmationViewModelTests
    {
        private ExternalLoginConfirmationViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ExternalLoginConfirmationViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ExternalLoginConfirmationViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue1429113491";
            _testClass.Email = testValue;
            Assert.That(_testClass.Email, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ExternalLoginListViewModelTests
    {
        private ExternalLoginListViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ExternalLoginListViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ExternalLoginListViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetReturnUrl()
        {
            var testValue = "TestValue443746594";
            _testClass.ReturnUrl = testValue;
            Assert.That(_testClass.ReturnUrl, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class SendCodeViewModelTests
    {
        private SendCodeViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new SendCodeViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new SendCodeViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetSelectedProvider()
        {
            var testValue = "TestValue1729025287";
            _testClass.SelectedProvider = testValue;
            Assert.That(_testClass.SelectedProvider, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetProviders()
        {
            var testValue = Substitute.For<ICollection<SelectListItem>>();
            _testClass.Providers = testValue;
            Assert.That(_testClass.Providers, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetReturnUrl()
        {
            var testValue = "TestValue274727720";
            _testClass.ReturnUrl = testValue;
            Assert.That(_testClass.ReturnUrl, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetRememberMe()
        {
            var testValue = true;
            _testClass.RememberMe = testValue;
            Assert.That(_testClass.RememberMe, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class VerifyCodeViewModelTests
    {
        private VerifyCodeViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new VerifyCodeViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new VerifyCodeViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetProvider()
        {
            var testValue = "TestValue347632621";
            _testClass.Provider = testValue;
            Assert.That(_testClass.Provider, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetCode()
        {
            var testValue = "TestValue524283656";
            _testClass.Code = testValue;
            Assert.That(_testClass.Code, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetReturnUrl()
        {
            var testValue = "TestValue598267457";
            _testClass.ReturnUrl = testValue;
            Assert.That(_testClass.ReturnUrl, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetRememberBrowser()
        {
            var testValue = false;
            _testClass.RememberBrowser = testValue;
            Assert.That(_testClass.RememberBrowser, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetRememberMe()
        {
            var testValue = false;
            _testClass.RememberMe = testValue;
            Assert.That(_testClass.RememberMe, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ForgotViewModelTests
    {
        private ForgotViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ForgotViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ForgotViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue461318499";
            _testClass.Email = testValue;
            Assert.That(_testClass.Email, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class LoginViewModelTests
    {
        private LoginViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new LoginViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new LoginViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue803688252";
            _testClass.Email = testValue;
            Assert.That(_testClass.Email, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetPassword()
        {
            var testValue = "TestValue759409869";
            _testClass.Password = testValue;
            Assert.That(_testClass.Password, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetRememberMe()
        {
            var testValue = false;
            _testClass.RememberMe = testValue;
            Assert.That(_testClass.RememberMe, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class RegisterViewModelTests
    {
        private RegisterViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new RegisterViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new RegisterViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue919225879";
            _testClass.Email = testValue;
            Assert.That(_testClass.Email, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetPassword()
        {
            var testValue = "TestValue1264040081";
            _testClass.Password = testValue;
            Assert.That(_testClass.Password, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetConfirmPassword()
        {
            var testValue = "TestValue893368829";
            _testClass.ConfirmPassword = testValue;
            Assert.That(_testClass.ConfirmPassword, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ResetPasswordViewModelTests
    {
        private ResetPasswordViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ResetPasswordViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ResetPasswordViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue1812975088";
            _testClass.Email = testValue;
            Assert.That(_testClass.Email, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetPassword()
        {
            var testValue = "TestValue109780406";
            _testClass.Password = testValue;
            Assert.That(_testClass.Password, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetConfirmPassword()
        {
            var testValue = "TestValue986635291";
            _testClass.ConfirmPassword = testValue;
            Assert.That(_testClass.ConfirmPassword, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetCode()
        {
            var testValue = "TestValue340235858";
            _testClass.Code = testValue;
            Assert.That(_testClass.Code, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ForgotPasswordViewModelTests
    {
        private ForgotPasswordViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ForgotPasswordViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ForgotPasswordViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetEmail()
        {
            var testValue = "TestValue148186842";
            _testClass.Email = testValue;
            Assert.That(_testClass.Email, Is.EqualTo(testValue));
        }
    }
}