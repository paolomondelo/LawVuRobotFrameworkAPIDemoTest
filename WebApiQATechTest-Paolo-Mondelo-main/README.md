# LawVu QA Tech Test 

## Description

The LawVu QA Tech Test is a simple WebApi written in .NET5 and C#9.

## Running the Solution

To run the solution you will need the dotnet CLI which can be installed by issuing the following command in a powershell terminal:

`dotnet-install.ps1 -Channel 5.0 -Runtime aspnetcore`

More information can be found here: https://docs.microsoft.com/en-us/dotnet/core/install/windows?tabs=net50

To run the solution browse to the root directory in a terminal and issue the following command:

`dotnet run --project WebApi`

Once the project is running, the swagger file can be accessed at the endpoints below. The following endpoints are used for
- Lawyer - Create - Creates a Lawyer
- Lawyer - Get - Returns a Lawyer
- LegalMatter - Create - Creates  Legal Matter
- LegalMatter - Get a LegalMatter (Individual and pagination)
- LegalMatter - Assign Lawyer (PATCH) - Assigned a lawyer to a legal matter

The structure of each object at the respective endpoints can be seen from the swagger file.

## The Assignment:

Once the application is running, the Api endpoint / Swagger page can be accessed at the following endpoints

https://localhost:5001/swagger/index.html

http://localhost:5000/swagger/index.html

### What needs to be done?

1. Setup Tests for a GET, POST and PATCH request.

2. Any testing framework / tool can be used that you prefer

3. Ensure all the levels of tests are covered

4. Create a Pull Request on this repo, and submit any script / test files created during the process. 

5. You will then be invited to demo and run through all the various testing procedures that was setup. 



