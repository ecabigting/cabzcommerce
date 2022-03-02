# cabzcommerce.api

The main api for the **cabzcommerce** app.

## Getting started

 - Setting up the API from [here](../api/setup.md)

## Understading the structure of the API

The `API` is is a `RESTful` API build using the with Repository pattern, MVC and Domain-Driven Design. For starters, you can checkout the __Controllers__ folder from [here](/src/cabzcommerce.api/). This is where you will access the api from different end-points under each controller. Let's take for example the `Brand` controller. This controller handles all request related to brands, like **Adding**, **Updating**, **Searching**, or **Listing** all brands. 

> NOTE: THE API Communicates via HTTP and JSON format.

 The **Brand** controller has its own repository. This is injected on _startup_. The actual code can be found at **line:64** from [here](/src/cabzcommerce.api/Program.cs). All endpoints of the controller are using async/await endpoints. Which are drilled down to the repository performing the request to the database.

> Read more about [Brands](/documentation/api/brands.md).

