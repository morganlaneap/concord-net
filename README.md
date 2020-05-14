# Concord - .NET Contract Testing Made Easy

Concord is an ambitious contract testing library that aims to simplify writing contract tests for developers.

### How does it work?

Concord works by spinning up a test server which runs your code and validates the expected responses with what actually gets returned. It supports dependency injection so it is a breeze to inject mocks into the test server in order to construct the test in the way needed. Concord will take a JSON file that contains the contract definitions and then run them against the test server.

A contract definition is simply a request with an expected response. Below is an example:

```json
{
  "provider": "api",
  "consumer": "website",
  "contracts": [
    {
      "name": "Loads data successfully",
      "scenario": "Data exists in the database",
      "request": {
        "url": "/data",
        "method": "GET"
      },
      "response": {
        "statusCode": 200,
        "body": [
          {
            "id": "ABC",
            "color": "GREEN"
          },
          {
            "id": "DEF",
            "color": "ORANGE"
          }
        ]
      }
    },
    {
      "name": "Loads data successfully",
      "scenario": "Data exists in the database",
      "request": {
        "url": "/data-test",
        "method": "GET"
      },
      "response": {
        "statusCode": 404
      }
    }
  ]
}
```

The `provider` is the API or service that will provide the responses. The `consumer` is the API or service that will use those responses. A `scenario` allows you to specify data that you expect would be there, such as a product or category existing in an ECommerce store.

### Writing Concord tests

Once you have you contract definitions, writing the Concord tests is easy. Spin up a new test project (for these examples we will be using NUnit) and add a new test class. Make sure that you are referencing the ConcordNet package.

In your test class, you need to add a single test to verify the contract definitions, such as:

```csharp
[Test]
public void VerifyContracts()
{
    var concordHost = new ConcordHost();
    concordHost.RegisterTestServer<Startup>();
    concordHost.AddContractDefinition("<path to your contract definition>");
    concordHost.VerifyContractDefinitions();
}
```

If you wish to configure your dependency injection (for injecting mocks) you can do so by creating a method such as:

```csharp
private void ConfigureDependencyInjection(IServiceCollection services)
{
    ...
}
```

Then pass it as a parameter to `RegisterTestServer`.

Once all this is done, just run `dotnet test` and see the result of the test.

## TODO:

Concord is in VERY early development so there is a lot to do. Here is a (non-definite) list:

- Scenario setup and management
- MUCH better logging outputs
- Better way of writing contract definitions (JS library maybe?)
- Header validation
