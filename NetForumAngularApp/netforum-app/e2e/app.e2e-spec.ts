import { browser, element, by } from 'protractor';

describe('Registration form in Netforum', () => {

  beforeEach(() => {
    browser.ignoreSynchronization = false;
    browser.get('/', 1000000);
  });

  it('Should enable submit button when all fields are filled.', () => {
    const firstNameElement = element(by.id("firstName"));
    const lastNameElement = element(by.id("lastName"));
    const emailElement = element(by.id("email"));
    const phoneNumberElement = element(by.id("phoneNumber"));
    const userNameElement = element(by.id("userName"));
    const passwordElement = element(by.id("password"));

    firstNameElement.sendKeys("Jacinto");
    lastNameElement.sendKeys("Zaitzewsky");
    emailElement.sendKeys("micales@hotmail.se");
    phoneNumberElement.sendKeys("0739660267");
    userNameElement.sendKeys("Jazz");
    passwordElement.sendKeys("Password");

    const registerButton = element(by.id("registerButton"));

    expect(registerButton.isEnabled()).toBe(true);
  });

  it('Should keep submit button disabled because not all fields are filled.', () => {
    const firstNameElement = element(by.id("firstName"));
    const lastNameElement = element(by.id("lastName"));
    const emailElement = element(by.id("email"));
    const phoneNumberElement = element(by.id("phoneNumber"));
    const userNameElement = element(by.id("userName"));
    const passwordElement = element(by.id("password"));

    firstNameElement.sendKeys("Jacinto");
    lastNameElement.sendKeys("Zaitzewsky");
    emailElement.sendKeys("micales@hotmail.se");
    phoneNumberElement.sendKeys("0739660267");
    userNameElement.sendKeys("Jazz");
    passwordElement.sendKeys("");

    const registerButton = element(by.id("registerButton"));

    expect(registerButton.isEnabled()).toBe(false);
  });

  it('Should keep submit button disabled because email field is not valid.', () => {
    const firstNameElement = element(by.id("firstName"));
    const lastNameElement = element(by.id("lastName"));
    const emailElement = element(by.id("email"));
    const phoneNumberElement = element(by.id("phoneNumber"));
    const userNameElement = element(by.id("userName"));
    const passwordElement = element(by.id("password"));
    var invalidEmailAdress = "micaleshotmail.se"

    firstNameElement.sendKeys("Jacinto");
    lastNameElement.sendKeys("Zaitzewsky");
    emailElement.sendKeys(invalidEmailAdress);
    phoneNumberElement.sendKeys("0739660267");
    userNameElement.sendKeys("Jazz");
    passwordElement.sendKeys("Password");

    const registerButton = element(by.id("registerButton"));

    expect(registerButton.isEnabled()).toBe(false);
  });

  it('Should show error message on touched field [FirstName] that is empty.', () => {
    const firstNameElement = element(by.id("firstName"));
    const lastNameElement = element(by.id("lastName"));

    firstNameElement.sendKeys("");
    lastNameElement.sendKeys("");

    const errorMessageElement = element(by.id("errorMessageFirstNameRegistration"));

    var expectedErrorMessage = "First name is required";
    expect(errorMessageElement.getText()).toEqual(expectedErrorMessage);
  });

  it('Should show error message on touched field [LastName] that is empty.', () => {
    const lastNameElement = element(by.id("lastName"));
    const emailElement = element(by.id("email"));

    lastNameElement.sendKeys("");
    emailElement.sendKeys("");

    const errorMessageElement = element(by.id("errorMessageLastNameRegistration"));

    var expectedErrorMessage = "Last name is required";
    expect(errorMessageElement.getText()).toEqual(expectedErrorMessage);
  });

  it('Should show error message on touched field [Email] that is empty.', () => {
    const emailElement = element(by.id("email"));
    const phoneNumberElement = element(by.id("phoneNumber"));

    emailElement.sendKeys("");
    phoneNumberElement.sendKeys("");

    const errorMessageElement = element(by.id("errorMessageEmailRegistration"));

    var expectedErrorMessage = "Email has to be in format: email@adress.com";
    expect(errorMessageElement.getText()).toEqual(expectedErrorMessage);
  });

  it('Should show error message on touched field [PhoneNumber] that is empty.', () => {
    const phoneNumberElement = element(by.id("phoneNumber"));
    const userNameElement = element(by.id("userName"));

    phoneNumberElement.sendKeys("");
    userNameElement.sendKeys("");

    const errorMessageElement = element(by.id("errorMessagePhoneNumberRegistration"));

    var expectedErrorMessage = "Phone number is required";
    expect(errorMessageElement.getText()).toEqual(expectedErrorMessage);
  });

  it('Should show error message on touched field [UserName] that is empty.', () => {
    const userNameElement = element(by.id("userName"));
    const passwordElement = element(by.id("password"));

    userNameElement.sendKeys("");
    passwordElement.sendKeys("");

    const errorMessageElement = element(by.id("errorMessageUsernameRegistration"));

    var expectedErrorMessage = "Username is required";
    expect(errorMessageElement.getText()).toEqual(expectedErrorMessage);
  });

  it('Should show error message on touched field [Password] that is empty.', () => {
    const userNameElement = element(by.id("userName"));
    const passwordElement = element(by.id("password"));

    passwordElement.sendKeys("");
    userNameElement.sendKeys("");

    const errorMessageElement = element(by.id("errorMessagePasswordRegistration"));

    var expectedErrorMessage = "Password is required";
    expect(errorMessageElement.getText()).toEqual(expectedErrorMessage);
  });
});
