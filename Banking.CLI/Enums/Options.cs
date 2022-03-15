namespace Banking.CLI.Enums;

enum GlobalOptions
{
    Logout
}
enum CustomerOptions
{
    Deposit,
    Withdraw,
    Transfer,
    ViewTransactionHistory
}
enum StaffOptions
{
    CreateCustomer,
    EditCustomerDetails,
    CreateCustomerAccount,
    CloseCustomerAccount,
    ViewAccountTransactionHistory,
    AddCurrency,
    EditCurrencyRates,
    EditServiceCharges
}
enum AdministratorOptions
{
    CreateStaff,
    EditStaffDetails,
    RemoveStaff,
    AddBank,
    EditBankDetails,
    SetCurrentInstance
}
enum RootOptions
{
    CreateAdministrator,
    EditAdministratorDetails,
    RemoveAdministrator
}
