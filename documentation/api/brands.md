# Brands

You can manage brands as part of the inventory management of the API. From here it will be describe the API end points on managing brands.

As part of the role managements. Only users with the **UserType** `Admin` can Create, Edit, or Update a Brand. For now, there is no option to delete a brand.



## Endpoints

### Get brand by ID
Any user can search brand by ID with **Get** request using the following end point:
```
brand/<brand id>
```

### Get all brands
Any user can get all brands with **Get** request by using the following endpoint:
```
brand/
```
`NOTE: it will return a list of available all brands`

`TODO: Pagination with Getallbrands`

### Add a new brand 
Only users with **UserType** `Admin` can create a brand. Endpoint:
```
brand/
```
- Adding new Brand rules:
    - Brand name must be unique
    - Must Have a `Name`
    - Must Have a `Description`
    - Must either be **enabled** or **disabled** on creation

Users must send a **Post** `json` request with the body [BrandDto](../../src/cabzcommerce.cshared/DTOs/Product/BrandDto.cs) model.

### Updating an existing Brand
Only Users with **UserType** `Admin` can update a brand.
Endpoint:
```
brand/<brand id>
```

- Updating existhing Brand rules:
    - Brand name must be unique
    - Must Have a `Name`
    - Must Have a `Description`
    - Must either be **enabled** or **disabled** on creation

Users must do a **Put** `json` request with the body [BrandDto](../../src/cabzcommerce.cshared/DTOs/Product/BrandDto.cs) model.

