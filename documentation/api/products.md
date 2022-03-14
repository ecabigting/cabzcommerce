# Product

You can manage Product as part of the inventory management of the API. From here it will be describe the API end points on managing Product.

As part of the role managements. Only users with the **UserType** `Admin` can Create, Edit, or Update a product. For now, there is no option to delete a product.



## Endpoints

### Get product by ID
Any user can search product by ID with **Get** request using the following end point:
```
product/<product id>
```

### Get all Product
Any user can get all Product with **Get** request by using the following endpoint:
```
product/
```
`NOTE: it will return a list of available all Product`

`TODO: Pagination with GetallProduct`

### Add a new product 
Only users with **UserType** `Admin` can create a product. Endpoint:
```
product/
```
- Adding new product rules:
    - Must have a `Name`
        - Name must be unique
    - Must have a `Description`
    - Must have a `Price`
    - Must have a `ProductImgeDesktopUrl`
    - Must have a `ProductImgMobileUrl`
    - Must atleast have one valid `ProductType`
    - Must enable or disabled `IsEnabled` property
    - Must either be **enabled** or **disabled** on creation

Users must send a **Post** `json` request with the body [productDto](../../src/cabzcommerce.cshared/DTOs/Product/productDto.cs) model.

### Updating an existing product
Only Users with **UserType** `Admin` can update a product.
Endpoint:
```
product/<product id>
```

- Updating existhing product rules:
    - product name must be unique
    - Must Have a `Name`
    - Must Have a `Description`
    - Must either be **enabled** or **disabled** on creation

Users must do a **Put** `json` request with the body [productDto](../../src/cabzcommerce.cshared/DTOs/Product/productDto.cs) model.

