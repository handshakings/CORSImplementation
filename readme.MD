# CORS (Cross-Origin-Resource-Sharing)

Complete Solution at https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-6.0#set-the-allowed-http-methods. You can set various option in CORS policy like
- Set the allowed origins
- Set the allowed HTTP methods
- Set the allowed request headers
- Set the exposed response headers
- Credentials in cross-origin requests
- Set the preflight expiration time

CORS allows restricted resources of a website (By browser same-origin-policy) to cross domains.

## Same-Origin-Policy
By default, all browser follow it. It means that two URLs can share resources only if both have same origin (schema,domain,port).

## CORS
What if you want your website resoureces to share to few other webistes or all websites of having cross origin. For this browsers allow to relax tight security by using CORS.

For this add a Response Header "Access-Control-Allow-Origin" in the response pipeline in server. It contains all domains who can access resource of this server.

## Test Same-Origin-Policy
- Run this API project in browser. It will be deployed in IIS/Kestrel and will open in browser with URL: https://localhost:44344.
- Open devloper tool in browser, go to https://www.techilm.com, run below fetch code in console to access running API project from techilm domain.

    ```javascript
    fetch("https://localhost:44344").then(x => x.text()).then(console.log);
    ```
- Below Error message will be displayed in browser console:
    Access to fetch at 'https://localhost:44344/' from origin 'https://www.google.com' has been blocked by CORS policy: No 'Access-Control-Allow-Origin' header is present on the requested resource. If an opaque response serves your needs, set the request's mode to 'no-cors' to fetch the resource with CORS disabled.

## Implement CORS
### For Simple Query
- Add "Access-Control-Allow-Origin" response header in response pipeline by adding CORS service and CORS middleware as 
    ```c#
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("https://www.techilm.com","https://www.youtube.com");
        });
    });
    app.UseCors();
    ```
- Now again run above fetch query, you will receive response successfully. However, if you run fetch query from other domains like https://www.google.com, it will not work. To allow all cross origins, add * instead of domains URLs like

    ```c#
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("*");
        });
    });
    app.UseCors();
    ```
### For Fency Query
- Any http request with custom header is called fency query. Now we will add request header and run fetch query as
    ```javascript
    fetch("https://localhost:44344",{headers:{"Content-Type":"json/application"}}).then(x => x.text()).then(console.log);
    ```
    Following error message will be displayed:
    Access to fetch at 'https://localhost:44344/' from origin 'https://www.google.com' has been blocked by CORS policy: Request header field content-type is not allowed by Access-Control-Allow-Headers in preflight response.
    Here browser send an option header to ask server, Is this method header allowed? and server reploy with Error Message of Preflight. To overcome this, allow header named Content-Type in server as
    ```c#
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(builder =>
        {
            builder.WithOrigins("https://www.techilm.com", "https://www.youtube.com")
                .WithHeaders("Access-Control-Allow-Headers", "Content-Type");
        });
    });
    ```
    Or you can allow any header by .AllowAnyHeader();
