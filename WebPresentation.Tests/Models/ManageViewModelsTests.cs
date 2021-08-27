namespace WebPresentation.Tests.Models
{
    using WebPresentation.Models;
    using System;
    using NUnit.Framework;
    using NSubstitute;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System.Web.Mvc;

    [TestFixture]
    public class IndexViewModelTests
    {
        private IndexViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new IndexViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new IndexViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetHasPassword()
        {
            var testValue = true;
            _testClass.HasPassword = testValue;
            Assert.That(_testClass.HasPassword, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetLogins()
        {
            var testValue = new[] { new UserLoginInfo("TestValue1015299662", "TestValue2098337045"), new UserLoginInfo("TestValue90295677", "TestValue595116281"), new UserLoginInfo("TestValue526912711", "TestValue936764596") };
            _testClass.Logins = testValue;
            Assert.That(_testClass.Logins, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetPhoneNumber()
        {
            var testValue = "TestValue936881375";
            _testClass.PhoneNumber = testValue;
            Assert.That(_testClass.PhoneNumber, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetTwoFactor()
        {
            var testValue = true;
            _testClass.TwoFactor = testValue;
            Assert.That(_testClass.TwoFactor, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetBrowserRemembered()
        {
            var testValue = true;
            _testClass.BrowserRemembered = testValue;
            Assert.That(_testClass.BrowserRemembered, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ManageLoginsViewModelTests
    {
        private ManageLoginsViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ManageLoginsViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ManageLoginsViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetCurrentLogins()
        {
            var testValue = new[] { new UserLoginInfo("TestValue32742144", "TestValue1513876399"), new UserLoginInfo("TestValue276055492", "TestValue409654487"), new UserLoginInfo("TestValue1269303420", "TestValue521183939") };
            _testClass.CurrentLogins = testValue;
            Assert.That(_testClass.CurrentLogins, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetOtherLogins()
        {
            var testValue = new[] { new AuthenticationDescription { AuthenticationType = "TestValue1448126574", Caption = "TestValue1562546983" }, new AuthenticationDescription { AuthenticationType = "TestValue1891659885", Caption = "TestValue925889693" }, new AuthenticationDescription { AuthenticationType = "TestValue613363593", Caption = "TestValue1628056563" } };
            _testClass.OtherLogins = testValue;
            Assert.That(_testClass.OtherLogins, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class FactorViewModelTests
    {
        private FactorViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new FactorViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new FactorViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetPurpose()
        {
            var testValue = "TestValue2109129231";
            _testClass.Purpose = testValue;
            Assert.That(_testClass.Purpose, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class SetPasswordViewModelTests
    {
        private SetPasswordViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new SetPasswordViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new SetPasswordViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetNewPassword()
        {
            var testValue = "TestValue1859519445";
            _testClass.NewPassword = testValue;
            Assert.That(_testClass.NewPassword, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetConfirmPassword()
        {
            var testValue = "TestValue1625939794";
            _testClass.ConfirmPassword = testValue;
            Assert.That(_testClass.ConfirmPassword, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ChangePasswordViewModelTests
    {
        private ChangePasswordViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ChangePasswordViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ChangePasswordViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetOldPassword()
        {
            var testValue = "TestValue1456745302";
            _testClass.OldPassword = testValue;
            Assert.That(_testClass.OldPassword, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetNewPassword()
        {
            var testValue = "TestValue1279797975";
            _testClass.NewPassword = testValue;
            Assert.That(_testClass.NewPassword, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetConfirmPassword()
        {
            var testValue = "TestValue1096134702";
            _testClass.ConfirmPassword = testValue;
            Assert.That(_testClass.ConfirmPassword, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class AddPhoneNumberViewModelTests
    {
        private AddPhoneNumberViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new AddPhoneNumberViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new AddPhoneNumberViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetNumber()
        {
            var testValue = "TestValue1352384470";
            _testClass.Number = testValue;
            Assert.That(_testClass.Number, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class VerifyPhoneNumberViewModelTests
    {
        private VerifyPhoneNumberViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new VerifyPhoneNumberViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new VerifyPhoneNumberViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetCode()
        {
            var testValue = "TestValue1314410687";
            _testClass.Code = testValue;
            Assert.That(_testClass.Code, Is.EqualTo(testValue));
        }

        [Test]
        public void CanSetAndGetPhoneNumber()
        {
            var testValue = "TestValue972287880";
            _testClass.PhoneNumber = testValue;
            Assert.That(_testClass.PhoneNumber, Is.EqualTo(testValue));
        }
    }

    [TestFixture]
    public class ConfigureTwoFactorViewModelTests
    {
        private ConfigureTwoFactorViewModel _testClass;

        [SetUp]
        public void SetUp()
        {
            _testClass = new ConfigureTwoFactorViewModel();
        }

        [Test]
        public void CanConstruct()
        {
            var instance = new ConfigureTwoFactorViewModel();
            Assert.That(instance, Is.Not.Null);
        }

        [Test]
        public void CanSetAndGetSelectedProvider()
        {
            var testValue = "TestValue1541066776";
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
    }
}